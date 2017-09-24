
using System.Collections.Generic;
using System.Threading.Tasks;
using FluToDo.Models;
using FluToDo.ServerConnections;
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

            MainPage = new FluToDoPage();

            this.serverConnectionManager = new ServerConnectionManager();

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

        #region Private Methods

        private void SetEvents(bool subscribe)
        {
            if (subscribe)
            {
                this.serverConnectionManager.OnGetTaskCompleted += this.OnGetTaskCompleted;
                this.serverConnectionManager.OnCreateTaskCompleted += this.OnCreateTaskCompleted;
                this.serverConnectionManager.OnDeleteTaskCompleted += this.OnDeleteTaskCompleted;
                this.serverConnectionManager.OnUpdateTaskCompleted += this.OnUpdateTaskCompleted;
            }
            else
            {
                this.serverConnectionManager.OnGetTaskCompleted -= this.OnGetTaskCompleted;
                this.serverConnectionManager.OnCreateTaskCompleted -= this.OnCreateTaskCompleted;
                this.serverConnectionManager.OnDeleteTaskCompleted -= this.OnDeleteTaskCompleted;
                this.serverConnectionManager.OnUpdateTaskCompleted -= this.OnUpdateTaskCompleted;
            }
        }

        void OnCreateTaskCompleted(object sender, bool e)
        {
            //TODO: do something when create item is complete
        }

        void OnDeleteTaskCompleted(object sender, bool e)
        {
            //TODO: do something when delete item is complete

        }

        void OnUpdateTaskCompleted(object sender, bool e)
        {
            //TODO: do something when update item is complete

        }

        private void OnGetTaskCompleted(object sender, IEnumerable<Models.TodoItem> todoItems)
        {
            //TODO: do something when get item is complete

        }
        #endregion
    }
}
