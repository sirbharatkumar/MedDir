using System;
using System.Collections.Generic;
using System.Text;

namespace MedDir.Domain.Entities
{
    public class ApiAuthUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
