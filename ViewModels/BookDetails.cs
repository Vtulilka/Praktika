using Shop1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop1.ViewModels
{
    public class BookDetails
    {
        public System.Guid Book_id { get; set; }
        public string Book_name { get; set; }
        public string Book_age_categ { get; set; }
        public int Book_count { get; set; }
        public int Book_price { get; set; }
        public string Genres  { get; set; }
        public string Authors { get; set; }
        public string Pub_house { get; set; }
    }
}