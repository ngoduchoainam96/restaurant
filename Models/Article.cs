using System;
using System.ComponentModel.DataAnnotations;

namespace razor08.efcore.Models
{
    public class Article
    {
        public int ID { get; set; }
        [Display(Name="Tên")]
        public string Title { get; set; }

        [Display(Name="Ngày đặt")]
        
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Display(Name="Nội dung")]
        public string Content {set; get;}
    }
}