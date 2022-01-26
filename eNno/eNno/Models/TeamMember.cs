using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eNno.Models
{
    public class TeamMember
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(25),Required]
        public string Name { get; set; }
        [MaxLength(50), Required]
        public string Surname { get; set; }
        [MaxLength(25), Required]
        public string Position { get; set; }
        [MaxLength(150), Required]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
