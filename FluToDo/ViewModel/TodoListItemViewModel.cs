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
            this.serverConnectionManager.GetTodoItem();
            this.serverConnectionManager.OnGetTaskCompleted += this.OnGetTaskCompletedAsync;
        }

        public void DeleteItem(TodoItem todoItem)
        {
            this.serverConnectionManager.OnDeleteTaskCompleted += this.OnDeleteItemCompleted;
            this.serverConnectionManager.DeleteTodoItem(todoItem);
        }
        #endregion

        #region Private Methods
        private async void OnGetTaskCompletedAsync(object sender, IEnumerable<TodoItem> todoItems)
        {
            if (todoItems != null)
            {
                this.OnGetData(this, todoItems);
            }
            else
            {
                this.OnGetData(this, todoItems);
            }

        }

        void OnDeleteItemCompleted(object sender, bool result)
        {
            this.OnDeleteData(this, result);
        }
        #endregion
    }
}
