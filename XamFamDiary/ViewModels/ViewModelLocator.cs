using System;
using Autofac;
using Xamarin.Forms;
using System.Reflection;
using System.Globalization;
using XamFamDiary.Services;
using XamFamDiary.Interfaces;

namespace XamFamDiary.ViewModels
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        static ViewModelLocator() { }

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        /// <summary>
        /// Registers denpendancy services
        /// </summary>
        public static void RegisterDependancies()
        {
            var builder = new ContainerBuilder();

            //Utilities
            builder.RegisterType<HttpService>().As<IHttpService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            //ViewModels
            builder.RegisterType<DiaryViewModel>();

            _container = builder.Build();
        }

        /// <summary>
        /// Resolves dependancy by Type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Wires up views & viewmodels base on naming convention & location
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as ContentPage;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            var vm = viewModel as BaseViewModel;
            vm.WireEvents(view);
            view.BindingContext = viewModel;
        }
    }
}