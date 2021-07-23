using System;
using System.Collections.Generic;

#nullable disable

namespace Kino.Models.KinoDBModel
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? RegisterDate { get; set; }
        public int? Category { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
