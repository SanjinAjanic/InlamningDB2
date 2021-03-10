using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InlamningDB2.Models
{
    class Model
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
