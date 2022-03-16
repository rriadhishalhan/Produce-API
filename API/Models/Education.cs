using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string GPA { get; set; }

        [ForeignKey("University")]
        public int University_Id { get; set; }

        //RELATION
        public University University { get; set; }
        public ICollection<Profiling> Profiling { get; set; }

    }
}
