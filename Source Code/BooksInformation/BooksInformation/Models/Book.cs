using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksInformation.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(100)]
        public string authoName { get; set; }
    }

    public class BookCreation
    {
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(100)]
        public string authoName { get; set; }
    }
}

