using System;
using System.ComponentModel.DataAnnotations;

namespace razor09.efcore.Models
{
    public class Menu
    {
        public int ID { get; set; }
        [Display(Name="Tên")]
        public string Title { get; set; }

        [Display(Name="Giá")]
        
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Display(Name="Giá")]
        public string Content {set; get;}
    }
}