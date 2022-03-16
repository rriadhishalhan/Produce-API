using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_account")]
    public class Account
    {
        [Key]
        [ForeignKey("Employee")]
        public string NIK { get; set; }

        [Required]
        public string Password { get; set; }

        public int OTP { get; set; }
        public DateTime ExpiredToken { get; set; }
        public bool isUsed { get; set; }

        //RELATION
        public Employee Employee { get; set; }
        public Profiling Profiling { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }

    }
}
