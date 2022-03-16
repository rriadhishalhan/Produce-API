﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //RELATION
        public ICollection<Education> Education { get; set; }
    }
}
