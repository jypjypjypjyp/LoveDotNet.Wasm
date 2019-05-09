using System;
using System.Collections.Generic;

namespace LoveDotNet.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User Anthor { get; set; }

        public virtual ICollection<Paragraph> Paragraphs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
