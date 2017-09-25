using System;
using FluToDo.ViewModel;
using Xamarin.Forms;

namespace FluToDo.ViewsItems
{
    /// <summary>
    /// Create new item.
    /// </summary>
    public partial class CreateNewItem : ContentPage
    {
        #region Attributes

        private CreateNewItemViewModel createNewItemViewModel;
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FluToDo.ViewsItems.CreateNewItem"/> class.
        /// </summary>
        public CreateNewItem()
        {
            this.createNewItemViewModel = new CreateNewItemViewModel();

            InitializeComponent();
        }
        #endregion

        #region Private Methods 
        private void OnCreateNewItem(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(nameEntry.Text))
            {
                this.createNewItemViewModel.OnNewItemCreated += this.OnNewItemCreated;

                this.createNewItemViewModel.CreateNewItem(nameEntry.Text);
            }
            else
            {
                DisplayAlert("Item creation", "Enter the item name", "OK");
            }
        }

        private  void OnNewItemCreated(object sender, bool result)
        {
            if(result)
            {
                DisplayAlert("Item creation", "Item created properly", "OK");
            }
            else
            {
                DisplayAlert("Item creation", "Item not created properly", "OK");
            }

            nameEntry.Text = string.Empty;

            Navigation.PopAsync();
        }

        private void OnCancel(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        #endregion
    }
}
