using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMail.Contact.Models
{
    public class ContactModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
