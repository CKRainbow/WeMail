using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMail.DAL.DTOs
{
    public class ContactDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string PhoneNumber { get; set; }
    }
}
