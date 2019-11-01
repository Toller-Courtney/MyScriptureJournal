using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyScriptureJournal.Models
{
    public class Scripture
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name="Chapter & Verse")]
        public string Chapter { get; set; }
        [Display(Name="Entry Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateAdded { get; set; }

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Book { get; set; }
        public string Notes { get; set; }
    }
}
