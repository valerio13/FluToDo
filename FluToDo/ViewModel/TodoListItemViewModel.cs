using System;
using System.Collections.Generic;
using FluToDo.Models;
using FluToDo.ServerConnections;

namespace FluToDo.ViewModel
{
    /// <summary>
    /// Todo list item view model.
    /// </summary>
    public class TodoListItemViewModel
    {
        #region Attributes

        private ServerConnectionManager serverConnectionManager;

        /// <summary>
        /// The on get data.
        /// </summary>
        public EventHandler<IEnumerable<TodoItem>> OnGetData;

        /// <summary>
        /// The on delete data.
        /// </summary>
        public EventHandler<bool> OnDeleteData;

        /// <summary>
        /// The state of the on change item.
        /// </summary>
        public EventHandler<bool> OnChangeItemState;

        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.ViewModel.TodoListItemViewModel"/> class.
        /// </summary>
        public TodoListItemViewModel()
        {
            this.serverConnectionManager = new ServerConnectionManager();
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public void GetData()
        {
            this.serverConnectionManager.OnGetTaskCompleted += this.OnGetItemsCompleted;
            this.serverConnectionManager.GetTodoItem();
        }

        public void DeleteItem(TodoItem todoItem)
        {
            this.serverConnectionManager.OnDeleteTaskCompleted += this.OnDeleteItemCompleted;
            this.serverConnectionManager.DeleteTodoItem(todoItem);
        }

        public void ChangeItemState(TodoItem todoItem)
        {
            this.serverConnectionManager.OnUpdateTaskCompleted += OnUpdateItemStateCompleted;
            this.serverConnectionManager.UpdateTodoItem(todoItem);
            
        }

        #endregion

        #region Private Methods
        private async void OnGetItemsCompleted(object sender, IEnumerable<TodoItem> todoItems)
        {
            if (todoItems != null)
            {
                this.OnGetData(this, todoItems);
            }
            else
            {
                this.OnGetData(this, null);
            }
        }

        private void OnDeleteItemCompleted(object sender, bool result)
        {
            this.OnDeleteData(this, result);
        }


        private void OnUpdateItemStateCompleted(object sender, bool result)
        {
            this.OnChangeItemState(this, result);
        }
        #endregion

    }
}
