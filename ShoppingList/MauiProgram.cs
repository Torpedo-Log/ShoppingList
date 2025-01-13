using Microsoft.Maui.LifecycleEvents;
using ShoppingList.Data;
using System.Diagnostics;

namespace ShoppingList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureLifecycleEvents(events =>
                {
                    events.AddWindows(windows => windows
                        .OnVisibilityChanged((window, args) =>
                        {
                            if (!args.Visible)
                                SaveData();
                        }));
                });

            return builder.Build();
        }

        private static void SaveData()
        {
            Debug.WriteLine("App is going into background. Saving products...");
            var products = FileHandler.LoadProducts();
            FileHandler.SaveProducts(products);
            Debug.WriteLine("Products saved successfully.");
        }
    }
}
