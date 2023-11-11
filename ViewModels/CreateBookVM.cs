using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Shop1.ViewModels
{
    public class CreateBookVM
    {
        public System.Guid Book_id { get; set; }

        [Required]
        [DisplayName("Название книги")]
        [StringLength(100, MinimumLength = 2)]
        public string Book_name { get; set; }
        [Required]
        [DisplayName("Издательство")]
        public string Book_pub_house { get; set; }
        [Required]
        [DisplayName("Возрастное ограничение")]
        [StringLength(4, MinimumLength = 2)]
        public string Book_age_categ { get; set; }
        [Required]
        [DisplayName("Количество книг на складе")]
        public int Book_count { get; set; }
        [Required]
        [DisplayName("Цена")]
        public int Book_price { get; set; }
        public int Author_number { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        public string Last_name { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string First_name { get; set; }
        [Required]
        [DisplayName("Отчество")]
        public string Middle_name { get; set; }
        public int Genre_number { get; set; }
        [Required]
        [DisplayName("Жанр")]
        public string Genre_name { get; set; }
    }
}