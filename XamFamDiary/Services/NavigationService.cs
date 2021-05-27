using System;
using System.Linq;
using Xamarin.Forms;
using System.Reflection;
using XamFamDiary.Views;
using System.Globalization;
using System.Threading.Tasks;
using XamFamDiary.Interfaces;
using XamFamDiary.ViewModels;
using System.Collections.Generic;

namespace XamFamDiary.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _mappings;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            _mappings.Add(typeof(DiaryViewModel), typeof(DiaryView));
        }

        /// <summary>
        /// Navigates to view accepts parameters
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        /// <summary>
        /// Navigates to view
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        /// <summary>
        /// Sets view as MainPage or pushes view onto stack
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType, parameter);
            var navigationPage = Application.Current.MainPage as NavigationView;

            if (navigationPage != null)
            {
                if (Application.Current.MainPage.Navigation.NavigationStack.Last().GetType() != page.GetType())
                    await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new NavigationView(page)
                {
                    BarTextColor = Color.White
                };
            }
            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        /// <summary>
        /// Locates view based on biew model
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns>Page type</returns>
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);

            return viewType;
        }

        /// <summary>
        /// Gets page based on ViewModel type
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="parameter"></param>
        /// <returns>Page</returns>
        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}