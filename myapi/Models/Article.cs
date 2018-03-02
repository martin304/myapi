using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myapi.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string FirstCategory { get; set; }
        public string SecondCategory { get; set; }
        public string ThirdCategory { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public bool pubable { get; set; }
        public bool show { get; set; }
        public int puborder { get; set; }
        public int ViewCount { get; set; }
        public DateTime UploadTime { get; set; }
    }
}