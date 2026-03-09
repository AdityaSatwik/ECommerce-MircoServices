using System;
using Microsoft.Owin.Hosting;

namespace ECommerce.LegacyInventory
{
    /// <summary>
    /// Entry point for the Legacy Inventory self-hosted OWIN service.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main application loop.
        /// </summary>
        static void Main(string[] args)
        {
            string baseAddress = "http://*:9000/";
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Legacy Inventory Service running at {baseAddress}...");
                System.Threading.Thread.Sleep(-1);
            }
        }
    }
}
