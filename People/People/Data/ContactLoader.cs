using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Models
{
    public class ContactLoader : IContactLoader
    {
         public IEnumerable<Contact> Load()
        {
            throw new NotImplementedException();
        }


        public void Save(IEnumerable<Contact> contacts)
        {
            throw new NotImplementedException();
        }
    }
}
