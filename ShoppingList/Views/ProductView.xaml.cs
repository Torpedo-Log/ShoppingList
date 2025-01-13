using ShoppingList.Models;
using ShoppingList.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.Views
{
    public partial class ProductView : ContentView
    {
        public ObservableCollection<Product> ParentCollection { get; set;}

        public ProductView()
        {
            InitializeComponent();
        }

        public ProductView(ObservableCollection<Product> products) : this()
        {
            ParentCollection = products;
            BindingContext = this;  
        }

        private void OnDecreaseQuantity(object sender, EventArgs e)
        {
            DebugBindingContext();  
            if (BindingContext is Product product && product.Quantity > 0)
            {
                product.Quantity--;
                Debug.WriteLine($"Decreasing quantity for product: {product.Name}, current quantity: {product.Quantity}");
                
                Debug.WriteLine(ParentCollection);
                SaveChanges();  
                Debug.WriteLine("Decreased quantity.");
            }
            else
            {
                Debug.WriteLine("BindingContext is not a Product or is null.");
            }
        }

        private void OnIncreaseQuantity(object sender, EventArgs e)
        {
            DebugBindingContext();  
            if (BindingContext is Product product)
            {
                Debug.WriteLine($"Increasing quantity for product: {product.Name}, current quantity: {product.Quantity}");
                product.Quantity++;
                SaveChanges();  
                Debug.WriteLine("Increased quantity.");
            }
            else
            {
                Debug.WriteLine("BindingContext is not a Product or is null.");
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Product product)
            {
                var productName = product.Name.Trim();  
                Debug.WriteLine($"Attempting to delete product: '{productName}'");

                var productList = FileHandler.LoadProducts();
                Debug.WriteLine("Products in file: " + string.Join(", ", productList.Select(p => p.Name)));

                var itemToRemove = productList.FirstOrDefault(p =>
                    string.Equals(p.Name?.Trim(), productName, StringComparison.OrdinalIgnoreCase));

                if (itemToRemove != null)
                {
                    productList.Remove(itemToRemove);
                    FileHandler.SaveProducts(productList);  
                    ParentCollection?.Remove(product);  
                    Debug.WriteLine($"Product '{productName}' deleted successfully.");
                }
                else
                {
                    Debug.WriteLine($"Product '{productName}' not found in the file.");
                }

                SaveChanges2();  
            }
            else
            {
                Debug.WriteLine("Sender is not a Button or BindingContext is not a Product.");
            }
        }

        private void SaveChanges()
        {
            
            if (ParentCollection != null)
            {
                Debug.WriteLine("Saving changes to file...");
                FileHandler.SaveProducts(ParentCollection.ToList());  
            }
            else
            {
                Debug.WriteLine("ParentCollection is null. Changes not saved.");
            }
        }
        private void SaveChanges2()
        {
                Debug.WriteLine("Saving changes to file...");
                FileHandler.SaveProducts(ParentCollection.ToList()); 
            
        }

        private void DebugBindingContext()
        {
            
            if (BindingContext == null)
            {
                Debug.WriteLine("BindingContext is null.");
            }
            else
            {
                Debug.WriteLine($"BindingContext is of type: {BindingContext.GetType()}");
            }
        }
    }
}
