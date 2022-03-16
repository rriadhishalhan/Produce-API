﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string JobRole { get; set; }

        //Relation
        public ICollection<AccountRole> AccountRoles { get; set; }

    }
}
