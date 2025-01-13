using ShoppingList.Models;
using ShoppingList.Data;
using System.Collections.ObjectModel;

namespace ShoppingList.Views
{
    public partial class AddProductPage : ContentPage
    {
        private ObservableCollection<Product> _products;

        public AddProductPage(ObservableCollection<Product> products)
        {
            InitializeComponent();
            _products = products;
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameEntry.Text) && !string.IsNullOrWhiteSpace(UnitEntry.Text))
            {
                var product = new Product
                {
                    Name = NameEntry.Text,
                    Unit = UnitEntry.Text,
                    Quantity = int.TryParse(QuantityEntry.Text, out var qty) ? qty : 1
                };
                _products.Add(product);
                FileHandler.SaveProducts(_products.ToList());
                Navigation.PopAsync();
            }
        }


    }
}