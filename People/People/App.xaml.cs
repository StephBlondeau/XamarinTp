using People.Models;
using People.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinUniversity.Interfaces;
using XamarinUniversity.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace People
{

    public partial class App : Application
    {
        public static SwaguinDatabase ContactRepo { get; set; }

        static App()
        {
            // Register dependencies.
            DependencyService.Register<MainViewModel>();
            // Register standard XamU services
            XamUInfrastructure.Init();
        }

        public App(String dbPath)
        {
            InitializeComponent();
            ContactRepo = new SwaguinDatabase(dbPath);
            ContactRepo.AddNewContactAsync("Stephanie");

            MasterDetailPage mdPage = new MainPage();
            MainPage = mdPage;

            // Register pages.
            var navService = DependencyService.Get<INavigationService>() as FormsNavigationPageService;
            navService.RegisterPage(AppPages.Edit, () => new ContactListPage());
           
        }

        protected override void OnSleep()
        {
            base.OnSleep();

            // Persist the quotes back out.
            ContactManager.Save(DependencyService.Get<IContactLoader>(),
                DependencyService.Get<MainViewModel>()
                    .Contacts.Select(qvm => qvm.Model));
        }
    }
}
