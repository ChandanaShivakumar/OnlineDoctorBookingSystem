﻿using System.ComponentModel.DataAnnotations;

namespace Sprint.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
