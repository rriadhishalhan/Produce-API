using API.Context;
using API.Models;
using API.ViewModel;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var empCount = myContext.Employees.ToList().Count;
            var Year = DateTime.Now.Year;
            var NikFormat = Year + "00" + (empCount+1).ToString();
            var result = 0;

            if (empCount != 0)
            {
                var LastNIK = int.Parse(myContext.Employees.OrderByDescending(e => e.NIK)
                    .Select(e => e.NIK).FirstOrDefault().ToString().Substring(4));
                NikFormat = Year + "00" + (LastNIK+1).ToString();
            }

            var emailEmployee = myContext.Employees.Where(e => e.Email == registerVM.Email).FirstOrDefault();
            var phoneEmployee = myContext.Employees.Where(e => e.Phone == registerVM.PhoneNumber).FirstOrDefault();

            if (emailEmployee != null && phoneEmployee == null)//untuk kondisi email sudah terdaftar
            {
                result = -1;
                return result;
            }
            else if (emailEmployee == null && phoneEmployee != null)//untuk kondisi HP sudah terdaftar
            {
                result = -2;
                return result;
            }
            else if (emailEmployee != null && phoneEmployee != null)//untuk kondisi EMAIL dan HP sudah terdaftar
            {
                result = -3;
                return result;
            }
            else// email dan password belum terdaftar
            {
                Employee emp = new Employee
                {
                    NIK = NikFormat,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Phone = registerVM.PhoneNumber,
                    BirthDate = registerVM.BirthDate,
                    Salary = registerVM.Salary,
                    Email = registerVM.Email,
                    Gender = registerVM.Gender,
                };

                Education edu = new Education
                {
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    University_Id = registerVM.UniversityId
                };

                Profiling prof = new Profiling
                {
                    NIK = NikFormat,
                    Education = edu
                };

                Account acc = new Account
                {
                    NIK = NikFormat,
                    Password = Hashing.HashPassword(registerVM.Password),
                    Employee = emp,
                    Profiling = prof
                };
                AccountRole acr = new AccountRole
                {
                    NIK = NikFormat,
                    Role_Id = registerVM.RoleId
                };

                myContext.Employees.Add(emp);
                myContext.Educations.Add(edu);
                myContext.Profilings.Add(prof);
                myContext.Accounts.Add(acc);
                myContext.AccountRoles.Add(acr);

                result = myContext.SaveChanges();
            }

            
            return result;
        }

        public int Login(LoginVM loginVM)
        {
            var result = 0;
            
            var LoginAccount = myContext.Employees.Join(myContext.Accounts,
                e => e.NIK,
                a => a.NIK,
                (e, a) => new
                {
                    NIK = e.NIK,
                    FirstName = e.FirstName,
                    Email = e.Email,
                    Password = a.Password
                }).SingleOrDefault(d => d.Email == loginVM.Email);

            if (LoginAccount != null)
            {
                if (Hashing.ValidatePassword(loginVM.Password, LoginAccount.Password))
                {
                    result = 1;
                    return result;
                }
                else
                {
                    result = 2;
                    return result;
                }
            }
            else
            {
                result = 3;
                return result;
            }
            
           
        }

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var updated = 0;
            var nikPass = myContext.Employees.Join(myContext.Accounts,
                e => e.NIK,
                j => j.NIK,
                (e, j) => new
                {
                    NIK = e.NIK,
                    FirstName = e.FirstName,
                    Email = e.Email,
                    Password = j.Password
                }).SingleOrDefault(d => d.Email == forgotPasswordVM.Email);

            if (nikPass != null)
            {
                var dataUpdate = new Account()
                {
                    NIK = nikPass.NIK,
                    Password = nikPass.Password,
                    OTP = new Random().Next(111111, 999999),
                    ExpiredToken = DateTime.Now.AddMinutes(5),
                    isUsed = false
                };
                updated = Update(dataUpdate);

                try
                {
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress("Customer Service OTP", "imkfutsalan@gmail.com"));
                    email.To.Add(MailboxAddress.Parse(nikPass.Email));
                    email.Subject = $"Halo {nikPass.FirstName} ini Informasi Kode OTP Kamu";
                    email.Body = new TextPart("Plain") { Text = $"Ini kodemu : {dataUpdate.OTP} (Kode OTP ini hanya berdurasi 5 Menit)" };

                    SmtpClient smtp = new SmtpClient();
                    smtp.Connect("smtp.gmail.com", 465, true);
                    smtp.Authenticate("imkfutsalan@gmail.com", "kevinreyryanfahmisalhan");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    smtp.Dispose();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                updated = -1;
            }      
            
            return updated;
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = 0;

            var dataChangePassword = myContext.Employees.Join(myContext.Accounts,
                e => e.NIK,
                j => j.NIK,
                (e, j) => new
                {
                    NIK = e.NIK,
                    Email = e.Email,
                    Password = j.Password,
                    Otp = j.OTP,
                    IsUsed = j.isUsed,
                    Duration = j.ExpiredToken 
                }).SingleOrDefault(d => d.Email == changePasswordVM.Email);

            if (changePasswordVM.Otp == dataChangePassword.Otp)
            {
                if (dataChangePassword.IsUsed == false)
                {
                    if (DateTime.Now < dataChangePassword.Duration)
                    {
                        if (changePasswordVM.NewPassword == changePasswordVM.ConfirmPassword)
                        {
                            var dataUpdate = new Account()
                            {
                                NIK = dataChangePassword.NIK,
                                Password = Hashing.HashPassword(changePasswordVM.NewPassword),
                                OTP = dataChangePassword.Otp,
                                ExpiredToken = dataChangePassword.Duration,
                                isUsed = true
                            };
                            var updated = Update(dataUpdate);
                            result = updated;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                    else
                    {
                        result = -2;
                    }
                }
                else
                {
                    result = -3;
                }
            }
            else
            {
                result = -4;
            }

            return result;
        }
    
        public class Hashing
        {
            private static string GetRandomSalt()
            {
                return BCrypt.Net.BCrypt.GenerateSalt(12);
            }
            public static string HashPassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
            }

            public static bool ValidatePassword(string password,string correctHash)
            {
                return BCrypt.Net.BCrypt.Verify(password, correctHash);
            }
        }
        

        public AccountRoleVM GetAccountRole(string email)
        {
            var dataRole = (from e in myContext.Employees
                          join a in myContext.Accounts on e.NIK equals a.NIK
                          join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                          join r in myContext.Roles on ar.Role_Id equals r.Id
                          where e.Email == email
                          select r.JobRole).ToArray();

            return new AccountRoleVM
            {
                Email = email,
                Roles = dataRole
            };
        }
    }
}
