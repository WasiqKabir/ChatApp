using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BindingModels
{
    public class UserBindingModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
