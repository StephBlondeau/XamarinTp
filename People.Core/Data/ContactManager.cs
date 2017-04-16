using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace People.Models
{
    public static class ContactManager
    {
        public static IEnumerable<Contact> Load(IContactLoader loader)
        {
            loader = DependencyService.Get<IContactLoader>();
            if (loader == null)
                throw new Exception("Missing contact loader from platform.");

            return loader.Load();
        }

        public static void Save(IContactLoader loader, IEnumerable<Contact> contacts)
        {
            if (loader == null)
                throw new Exception("Missing contact loader from platform.");

            loader.Save(contacts);
        }
    }
}
