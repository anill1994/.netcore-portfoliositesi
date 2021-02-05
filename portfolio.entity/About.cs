using System;
using System.Collections.Generic;

namespace portfolio.entity
{
    public class About
    {
        public int AboutId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string Job { get; set; }
    }
}