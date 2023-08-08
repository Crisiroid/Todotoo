using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace todotoo.Models
{
    public class Content
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}