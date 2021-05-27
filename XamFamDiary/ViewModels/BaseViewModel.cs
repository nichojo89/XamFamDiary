using System;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;

namespace XamFamDiary.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel
    {
        /// <summary>
        /// Wires up Page element events with polymorphic overrides
        /// </summary>
        /// <param name="page"></param>
        public void WireEvents(Page page)
        {
            page.Appearing += ViewIsAppearing;
            page.Disappearing += ViewIsDisappearing;
        }

        /// <summary>
        /// Called when the view is initialized
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// Called when view is appearing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ViewIsAppearing(object sender, EventArgs e) { }

        /// <summary>
        /// Called when view is disappearing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ViewIsDisappearing(object sender, EventArgs e) { }
    }
}