
using System.Collections.Generic;
using System.Threading.Tasks;
using FluToDo.Models;
using FluToDo.ServerConnections;
using FluToDo.ViewsItems;
using Xamarin.Forms;

namespace FluToDo
{
    /// <summary>
    /// App.
    /// </summary>
    public partial class App : Application
    {
        #region Attributes

        private ServerConnectionManager serverConnectionManager;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            MainPage.Navigation.PushAsync(new TodoListItemsView());
        }

        #endregion

        #region Protected Methods

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion

    }
}
