using System;
using System.Collections.Generic;
using FluToDo.Models;
using FluToDo.ServerConnections;

namespace FluToDo.ViewModel
{
    /// <summary>
    /// Create new item view model.
    /// </summary>
    public class CreateNewItemViewModel
    {
        #region Attributes

        private ServerConnectionManager serverConnectionManager;

        /// <summary>
        /// The on new item created.
        /// </summary>
        public EventHandler<bool> OnNewItemCreated;

        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.ViewModel.CreateNewItemViewModel"/> class.
        /// </summary>
        public CreateNewItemViewModel()
        {
            this.serverConnectionManager = new ServerConnectionManager();
            this.serverConnectionManager.OnCreateTaskCompleted += OnCreateNewItemCompleted;
        }

        /// <summary>
        /// Creates the new item.
        /// </summary>
        /// <param name="newItemName">New item name.</param>
        public void CreateNewItem(string newItemName)
        {
            this.serverConnectionManager.CreateTodoItem(new TodoItem
            {
                Name = newItemName,
                IsComplete = false,
                Key = string.Empty
            });
        }
        #endregion

        #region Private Methods 

        void OnCreateNewItemCompleted(object sender, bool result)
        {
            this.OnNewItemCreated(this, result);
        }

        #endregion
    }
}
