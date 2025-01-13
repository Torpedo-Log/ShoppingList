using ShoppingList.Models;
using ShoppingList.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.Views
{
    public partial class ProductView : ContentView
    {
        public ObservableCollection<Product> ParentCollection { get; set;}

        // Constructor to initialize the view with the ParentCollection (ObservableCollection of products)
        public ProductView()
        {
            InitializeComponent();
        }

        // Constructor that accepts the ObservableCollection<Product> and sets the BindingContext to this view
        public ProductView(ObservableCollection<Product> products) : this()
        {
            ParentCollection = products;
            BindingContext = this;  // Set the BindingContext to the view itself
        }

        private void OnDecreaseQuantity(object sender, EventArgs e)
        {
            DebugBindingContext();  // Check BindingContext during the event
            if (BindingContext is Product product && product.Quantity > 0)
            {
                product.Quantity--;
                Debug.WriteLine($"Decreasing quantity for product: {product.Name}, current quantity: {product.Quantity}");
                
                Debug.WriteLine(ParentCollection);
                SaveChanges();  // Save changes after modifying quantity
                Debug.WriteLine("Decreased quantity.");
            }
            else
            {
                Debug.WriteLine("BindingContext is not a Product or is null.");
            }
        }

        private void OnIncreaseQuantity(object sender, EventArgs e)
        {
            DebugBindingContext();  // Check BindingContext during the event
            if (BindingContext is Product product)
            {
                Debug.WriteLine($"Increasing quantity for product: {product.Name}, current quantity: {product.Quantity}");
                product.Quantity++;
                SaveChanges();  // Save changes after modifying quantity
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
                var productName = product.Name.Trim();  // Trim any leading/trailing whitespace
                Debug.WriteLine($"Attempting to delete product: '{productName}'");

                // Load the product list from the file
                var productList = FileHandler.LoadProducts();
                Debug.WriteLine("Products in file: " + string.Join(", ", productList.Select(p => p.Name)));

                // Find the product by name (case-insensitive comparison)
                var itemToRemove = productList.FirstOrDefault(p =>
                    string.Equals(p.Name?.Trim(), productName, StringComparison.OrdinalIgnoreCase));

                if (itemToRemove != null)
                {
                    productList.Remove(itemToRemove);
                    FileHandler.SaveProducts(productList);  // Save changes to the file
                    ParentCollection?.Remove(product);  // Remove the product from the collection
                    Debug.WriteLine($"Product '{productName}' deleted successfully.");
                }
                else
                {
                    Debug.WriteLine($"Product '{productName}' not found in the file.");
                }

                SaveChanges2();  // Save changes after deletion
            }
            else
            {
                Debug.WriteLine("Sender is not a Button or BindingContext is not a Product.");
            }
        }

        private void SaveChanges()
        {
            // Check if ParentCollection is null before saving
            if (ParentCollection != null)
            {
                Debug.WriteLine("Saving changes to file...");
                FileHandler.SaveProducts(ParentCollection.ToList());  // Save the updated collection
            }
            else
            {
                Debug.WriteLine("ParentCollection is null. Changes not saved.");
            }
        }
        private void SaveChanges2()
        {
            // Check if ParentCollection is null before saving
            
            
                Debug.WriteLine("Saving changes to file...");
                FileHandler.SaveProducts(ParentCollection.ToList());  // Save the updated collection
            
        }

        private void DebugBindingContext()
        {
            // Log the BindingContext type to check if it's correct
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
