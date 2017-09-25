using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluToDo.Models;
using Newtonsoft.Json;

namespace FluToDo.ServerConnections
{
    /// <summary>
    /// Server connection.
    /// </summary>
    public class ServerConnection
    {
        #region Attributes

        private string serverName;

        private int portNumber;

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public IDictionary<string, string> ErrorMessage { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.ServerConnections.ServerConnection"/> class.
        /// </summary>
        /// <param name="serverName">Server name.</param>
        /// <param name="portName">Port name.</param>
        public ServerConnection(string serverName, int portName)
        {
            this.serverName = serverName;
            this.portNumber = portName;
            this.ErrorMessage = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the http client.
        /// </summary>
        /// <returns>The http client.</returns>
        public HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(String.Format("http://{0}:{1}", this.serverName, this.portNumber));

            return httpClient;
        }

        /// <summary>
        /// Gets the data async.
        /// </summary>
        /// <returns>The data async.</returns>
        /// <param name="getString">Get string.</param>
        public async Task<IEnumerable<TodoItem>> GetDataAsync(string getString)
        {
            try
            {
                using (HttpClient httpClient = this.GetHttpClient())
                {
                    HttpResponseMessage eventController = await httpClient.GetAsync(getString); 
                   
                    if (eventController.IsSuccessStatusCode)
                    {
                        string message = await eventController.Content.ReadAsStringAsync();
                        IEnumerable<TodoItem> newObject = JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(message);

                        return newObject;
                    }

                    return default(IEnumerable<TodoItem>);
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Add("GetDataAsync", exception.Message);

                return null;
            }
        }

        /// <summary>
        /// Posts the data async.
        /// </summary>
        /// <returns>The data async.</returns>
        /// <param name="postString">Post string.</param>
        /// <param name="todoItem">Todo item.</param>
        public async Task<bool> PostDataAsync(string postString, TodoItem todoItem)
        {
            try
            {
                using (HttpClient httpClient = this.GetHttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage eventController = await httpClient.PostAsJsonAsync(postString, todoItem);

                    if (eventController.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Add("PostDataAsync", exception.Message);

                return false;
            }
        }

        /// <summary>
        /// Puts the data async.
        /// </summary>
        /// <returns>The data async.</returns>
        /// <param name="postString">Post string.</param>
        /// <param name="todoItem">Todo item.</param>
        public async Task<bool> PutDataAsync(string postString, TodoItem todoItem)
        {
            try
            {
                using (HttpClient httpClient = this.GetHttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage eventController = await httpClient.PutAsJsonAsync(postString, todoItem);

                    if (eventController.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Add("PutDataAsync", exception.Message);

                return false;
            }
        }

        /// <summary>
        /// Deletes the data async.
        /// </summary>
        /// <returns>The data async.</returns>
        /// <param name="deleteString">Delete string.</param>
        public async Task<bool> DeleteDataAsync(string deleteString)
        {
            try
            {
                using (HttpClient httpClient = this.GetHttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    HttpResponseMessage eventController = await httpClient.DeleteAsync(deleteString);

                    if (eventController.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception exception)
            {
                this.ErrorMessage.Add("DeleteDataAsync", exception.Message);

                return false;
            }
        }
        #endregion
    }
}
