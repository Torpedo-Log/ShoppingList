using ShoppingList.Data;
using ShoppingList.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.Views
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Product> Products { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>(FileHandler.LoadProducts());
            BindingContext = this;

            this.Disappearing += OnAppDisappearing;
        }

        private void OnAddProductClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddProductPage(Products));
            Trace.WriteLine("AddNewProduct is clicked");
        }

        private void OnAppDisappearing(object sender, EventArgs e)
        {
            Debug.WriteLine("App window is disappearing. Saving products...");
            SaveProductsToFile();
        }

        private void SaveProductsToFile()
        {
            FileHandler.SaveProducts(Products.ToList());
            Debug.WriteLine("Products saved successfully.");
        }

    }
}
