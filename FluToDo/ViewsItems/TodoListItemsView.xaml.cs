using System;
using System.Collections.Generic;
using FluToDo.Models;
using FluToDo.ServerConnections;
using FluToDo.ViewModel;
using Xamarin.Forms;

namespace FluToDo.ViewsItems
{
    /// <summary>
    /// Todo list items view.
    /// </summary>
    public partial class TodoListItemsView : ContentPage
    {
        #region Attributes
        private TodoListItemViewModel todoListItemViewModel;
        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.ViewsItems.TodoListItemsView"/> class.
        /// </summary>
        public TodoListItemsView()
        {
            this.todoListItemViewModel = new TodoListItemViewModel();
            InitializeComponent();
        }

        #endregion

        #region Protected Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.todoListItemViewModel.OnGetData += OnGetTaskCompletedAsync;
            this.todoListItemViewModel.GetData();

            activityIndicator.IsRunning = true;
            activityIndicator.HeightRequest = 50;
        }
        #endregion

        #region Private Methods

        private async void OnGetTaskCompletedAsync(object sender, IEnumerable<Models.TodoItem> todoItems)
        {
            if (todoItems != null)
            {
                listView.ItemsSource = null;
                listView.ItemsSource = todoItems;
            }
            else
            {
                await DisplayAlert("Atención", "Ningún elemento memorizado en la base de datos.", "OK");
                listView.ItemsSource = null;
            }

            activityIndicator.IsRunning = false;
            activityIndicator.HeightRequest = 0;
        }


        //Main Manu Management
        void OnOpenMenuCliked(object sender, EventArgs args)
        {
            //TODO: main menu options

        }

        //Item selected event
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //TODO: manage the item selected event
        }

        #endregion
    }
}
