using Xamarin.Forms;

namespace XamFamDiary.Views
{
    public partial class NavigationView : NavigationPage
    {
        public NavigationView(Page root) : base(root)
        {
            InitializeComponent();
            BarTextColor = Color.White;
        }
    }
}