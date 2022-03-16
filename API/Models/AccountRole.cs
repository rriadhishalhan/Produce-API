using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_account_role")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public string NIK { get; set; }

        [ForeignKey("Role")]
        public int Role_Id { get; set; }

        //RELATION
        public Account Account { get; set; }
        public Role Role { get; set; }

    }
}
