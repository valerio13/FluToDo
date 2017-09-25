using System;
using System.Collections.Generic;
using System.Linq;
using FluToDo.Models;
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

        private IEnumerable<TodoItem> todoItems;

        private TodoItem itemDeleted;

        private TodoItem itemChanged;

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

        private void OnGetTaskCompletedAsync(object sender, IEnumerable<TodoItem> todoItems)
        {
            this.todoItems = todoItems;

            if (todoItems != null)
            {
                listView.ItemsSource = null;
                listView.ItemsSource = this.todoItems;
            }
            else
            {
                DisplayAlert("Attetion", "No element found.", "OK");
                listView.ItemsSource = null;
            }

            activityIndicator.IsRunning = false;
            activityIndicator.HeightRequest = 0;

        }


        private void OnCreateNewItem(object sender, EventArgs args)
        {
            var newItemPage = new CreateNewItem();
            Navigation.PushAsync(newItemPage);
        }


        void OnRefresh(object sender, EventArgs e)
        {
            //TODO: manage the refresh event
        }

        void OnTap(object sender, ItemTappedEventArgs itemArgs)
        {
            TodoItem todoItem = (TodoItem)itemArgs.Item;
            todoItem.IsComplete = !todoItem.IsComplete;
            this.itemChanged = todoItem;

            this.todoListItemViewModel.OnChangeItemState += OnChangeItemState;
            this.todoListItemViewModel.ChangeItemState(todoItem);
        }

        void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            TodoItem todoItem = (TodoItem)menuItem.CommandParameter;

            this.itemDeleted = todoItem;

            this.todoListItemViewModel.OnDeleteData += this.OnDeleteDataResult;
            this.todoListItemViewModel.DeleteItem(todoItem);
        }


        void OnDeleteDataResult(object sender, bool result)
        {
            if (result)
            {
                DisplayAlert("Delete Item", "Item deleted correctly.", "OK");

                this.todoItems = this.todoItems.Where(u => u.Key != this.itemDeleted.Key).ToList();

                listView.ItemsSource = null;
                listView.ItemsSource = this.todoItems;
            }
            else
            {
                DisplayAlert("Delete Item", "Was not possible to deleted the item.", "OK");
            }

            this.itemDeleted = null;
            this.todoListItemViewModel.OnDeleteData -= this.OnDeleteDataResult;
        }

        void OnChangeItemState(object sender, bool result)
        {
            if (result)
            {
                foreach (TodoItem item in this.todoItems)
                {
                    if (item.Key == this.itemChanged.Key)
                    {
                        item.IsComplete = this.itemChanged.IsComplete;
                    }
                }

                listView.ItemsSource = null;
                listView.ItemsSource = this.todoItems;
            }
            else
            {
                DisplayAlert("Change item status", "Was not possible to change the item status.", "OK");
            }

            this.itemChanged = null;
            this.todoListItemViewModel.OnChangeItemState -= OnChangeItemState;

        }

        #endregion
    }
}
