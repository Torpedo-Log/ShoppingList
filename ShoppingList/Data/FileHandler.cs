using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public static class FileHandler
    {
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "shopping_list.json");

        public static List<Product> LoadProducts()
        {
            if (!File.Exists(FilePath)) return new List<Product>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public static void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products ?? new List<Product>());
            File.WriteAllText(FilePath, json);
        }

    }
}
