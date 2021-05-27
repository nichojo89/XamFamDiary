using System.Threading.Tasks;
using XamFamDiary.ViewModels;

namespace XamFamDiary.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to view
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        /// <summary>
        /// Navigates to view accepts parameters
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
    }
}