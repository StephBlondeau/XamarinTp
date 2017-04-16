using People.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinUniversity.Infrastructure;
using XamarinUniversity.Interfaces;

namespace People.ViewModels
{
    public class MainViewModel : SimpleViewModel
    {
        IDependencyService serviceLocator;
        public ObservableCollection<ContactViewModel> Contacts { get; set; }
        public List<Contact> ListContacts { get; set; }

        ContactViewModel selectedContact;

        public ContactViewModel SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                SetPropertyValue(ref selectedContact, value);
            }
        }

        public IAsyncDelegateCommand AddContact { get; private set; }
        public IAsyncDelegateCommand CallContact { get; private set; }
        public IAsyncDelegateCommand ImageContact { get; private set; }
        public IAsyncDelegateCommand<ContactViewModel> DeleteContact { get; private set; }
        public IAsyncDelegateCommand<ContactViewModel> EditContact { get; private set; }
        public IAsyncDelegateCommand<ContactViewModel> ShowContactDetail { get; private set; }

        public MainViewModel() : this(new XamarinUniversity.Services.DependencyServiceWrapper())
        {
        }

        public MainViewModel(IDependencyService serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            GetContact();
            Contact unContact = new Contact();

                Contacts = new ObservableCollection<ContactViewModel>(ListContacts
                            .Select(c => new ContactViewModel(c)));
    

            SelectedContact = Contacts.FirstOrDefault();

            AddContact = new AsyncDelegateCommand(OnAddContact);
            DeleteContact = new AsyncDelegateCommand<ContactViewModel>(OnDeleteContact);
            EditContact = new AsyncDelegateCommand<ContactViewModel>(OnEditContact);
            ShowContactDetail = new AsyncDelegateCommand<ContactViewModel>(OnShowContactDetails);
        }

        public async Task GetContact()
        {
            ListContacts = await App.ContactRepo.GetAllContactAsync();
            
        }

        private async Task OnShowContactDetails(ContactViewModel qvm)
        {
            SelectedContact = qvm;
            await serviceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Detail);
        }

        private async Task OnAddContact()
        {
            var newContact = new ContactViewModel();
            Contacts.Add(newContact);
            SelectedContact = newContact;

                await App.ContactRepo.AddNewContactAsync(newContact.FirstName);
        }

       private async Task OnEditContact(ContactViewModel contact)
        {
            SelectedContact = contact;
            await serviceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Edit, contact);
        }

        private async Task OnDeleteContact(ContactViewModel contact)
        {
            bool result = await serviceLocator.Get<IMessageVisualizerService>()
                .ShowMessage("Are you sure?",
                    "Are you sure you want to delete this quote from " + contact.Author + "?",
                    "Yes", "No");

            if (result == true)
            {
                int pos = Contacts.IndexOf(contact);
                Contacts.Remove(contact);
                if (SelectedContact == contact)
                {
                    if (pos > Contacts.Count - 1)
                        pos = Contacts.Count - 1;
                    SelectedContact = Contacts[pos];
                }
            }
        }
    }
}
