using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluToDo.Models;

namespace FluToDo.ServerConnections
{
    /// <summary>
    /// Server connection manager.
    /// </summary>
    public class ServerConnectionManager
    {
        #region Attributes
        private readonly string serverName = "192.168.1.2";

        private readonly int connectionPort = 8080;

        private readonly string rootString = "api/todo";

        private readonly string optionalParameter = "?id=";

        private ServerConnection serverConnection;

        /// <summary>
        /// The on get task completed.
        /// </summary>
        public EventHandler<IEnumerable<TodoItem>> OnGetTaskCompleted;

        /// <summary>
        /// The on create task completed.
        /// </summary>
        public EventHandler<bool> OnCreateTaskCompleted;

        /// <summary>
        /// The on update task completed.
        /// </summary>
        public EventHandler<bool> OnUpdateTaskCompleted;

        /// <summary>
        /// The on delete task completed.
        /// </summary>
        public EventHandler<bool> OnDeleteTaskCompleted;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.ServerConnections.ServerConnectionManager"/> class.
        /// </summary>
        public ServerConnectionManager()
        {
            this.serverConnection = new ServerConnection(this.serverName, this.connectionPort);
        }

        /// <summary>
        /// Gets the todo item.
        /// </summary>
        /// <returns>The todo item.</returns>
        /// <param name="id">Identifier.</param>
        public async Task GetTodoItem(string id = null)
        {
            string getString = this.rootString;
            getString += id != null ? optionalParameter + id : string.Empty;

            Task<IEnumerable<TodoItem>> getTodoItemTaskTaks = Task.Run(() => serverConnection.GetDataAsync(getString));

            await getTodoItemTaskTaks;

            if (getTodoItemTaskTaks.Status == TaskStatus.RanToCompletion)
            {
                this.OnGetTaskCompleted(this, getTodoItemTaskTaks.Result);
            }
            else
            {
                this.OnGetTaskCompleted(this, null);
            }
        }

        /// <summary>
        /// Creates the todo item.
        /// </summary>
        /// <returns>The todo item.</returns>
        /// <param name="todoItem">Todo item.</param>
        public async Task CreateTodoItem(TodoItem todoItem)
        {
            Task<bool> postTodoItemTaskTaks = Task.Run(() => serverConnection.PostDataAsync(this.rootString, todoItem));

            await postTodoItemTaskTaks;

            if (postTodoItemTaskTaks.Status == TaskStatus.RanToCompletion)
            {
                this.OnCreateTaskCompleted(this, true);
            }
            else
            {
                this.OnCreateTaskCompleted(this, false);
            }
        }

        /// <summary>
        /// Updates the todo item.
        /// </summary>
        /// <returns>The todo item.</returns>
        /// <param name="todoItem">Todo item.</param>
        public async Task UpdateTodoItem(TodoItem todoItem)
        {
            string uri = this.rootString + this.optionalParameter + todoItem.Key;

            Task<bool> putTodoItemTaskTaks = Task.Run(() => serverConnection.PutDataAsync(uri, todoItem));

            await putTodoItemTaskTaks;

            if (putTodoItemTaskTaks.Status == TaskStatus.RanToCompletion)
            {
                this.OnUpdateTaskCompleted(this, true);
            }
            else
            {
                this.OnUpdateTaskCompleted(this, false);
            }
        }

        /// <summary>
        /// Deletes the todo item.
        /// </summary>
        /// <returns>The todo item.</returns>
        /// <param name="todoItem">Todo item.</param>
        public async Task DeleteTodoItem(TodoItem todoItem)
        {
            string uri = this.rootString + this.optionalParameter + todoItem.Key;

            Task<bool> putTodoItemTaskTaks = Task.Run(() => serverConnection.DeleteDataAsync(uri));

            await putTodoItemTaskTaks;

            if (putTodoItemTaskTaks.Status == TaskStatus.RanToCompletion)
            {
                this.OnDeleteTaskCompleted(this, true);
            }
            else
            {
                this.OnDeleteTaskCompleted(this, false);
            }
        }
        #endregion
    }
}
