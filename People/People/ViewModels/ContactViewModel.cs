using People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using XamarinUniversity.Infrastructure;

namespace People.ViewModels
{
    public class ContactViewModel : SimpleViewModel
    {
        readonly Contact contact;

        public string FirstName
        {
            get { return contact.FirstName; }
            set
            {
                if (contact.FirstName != value)
                {
                    contact.FirstName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => Author);
                }
            }
        }

        public string LastName
        {
            get { return contact.LastName; }
            set
            {
                if (contact.LastName != value)
                {
                    contact.LastName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => Author);
                }
            }
        }

        public string Author
        {
            get { return contact.FirstName + " " + contact.LastName; }
        }

        public Boolean Gender
        {
            get { return contact.Gender; }
            set
            {
                if (contact.Gender != value)
                {
                    contact.Gender = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Email
        {
            get { return contact.Email; }
            set
            {
                if (contact.Email != value)
                {
                    contact.Email = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Contact Model
        {
            get { return contact; }
        }

        public ContactViewModel()
            : this(new Contact())
        {
            FirstName = "Stéphanie";
            LastName = "Blondeau";
            Email = "prenom.nom@email.com";
            Gender = true;

        }

        public ContactViewModel(Contact contact)
        {
            this.contact = contact;
        }
    }
}
