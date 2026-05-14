using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary.Models
{
    [Table("books")]
    public class Book
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Название обязательно")]
        [Column("title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Автор обязателен")]
        [Column("author")]
        public string Author { get; set; }
        
        [Column("year")]
        public int? Year { get; set; }
        
        [Column("genre")]
        public string Genre { get; set; }
        
        [Column("publisher")]
        public string Publisher { get; set; }
        
        [Column("isbn")]
        public string ISBN { get; set; }
        
        [Column("content_html")]
        public string ContentHtml { get; set; }
    }
}
