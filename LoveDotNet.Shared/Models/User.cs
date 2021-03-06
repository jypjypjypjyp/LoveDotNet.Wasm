﻿using System.Collections.Generic;

namespace LoveDotNet.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avator { get; set; }
        public string Password { get; set; }
        public bool IsEditor { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public bool IsEmpty()
        {
            if (Email == default) return true;
            else return false;
        }
    }
}
