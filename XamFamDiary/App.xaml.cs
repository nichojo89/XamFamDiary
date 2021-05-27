using Xamarin.Forms;
using XamFamDiary.Interfaces;
using XamFamDiary.ViewModels;

namespace XamFamDiary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Registers all dependancy services
            ViewModelLocator.RegisterDependancies();

            //Navigate to Diary view
            Launch();
        }

        /// <summary>
        /// Navigates to correct start up page
        /// </summary>v
        /// <returns></returns>
        private void Launch()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            navigationService.NavigateToAsync<DiaryViewModel>();
        }
    }
}