namespace ECommerce.LegacyInventory.Config
{
    /// <summary>
    /// Singleton configuration class for the legacy inventory system.
    /// </summary>
    public sealed class InventorySingletonConfig
    {
        private static readonly InventorySingletonConfig INSTANCE = new InventorySingletonConfig();
        
        /// <summary>
        /// Gets the configured database connection string.
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Initializes static members of the <see cref="InventorySingletonConfig"/> class.
        /// </summary>
        static InventorySingletonConfig() { }

        private InventorySingletonConfig()
        {
            // Simulate reading from App.config but allow environment override for Docker
            var envConn = System.Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            ConnectionString = !string.IsNullOrEmpty(envConn) 
                ? envConn 
                : "Server=(localdb)\\Local;Database=ECommerce_LegacyInventory;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        /// <summary>
        /// Gets the singleton instance of the configuration.
        /// </summary>
        public static InventorySingletonConfig Instance
        {
            get
            {
                return INSTANCE;
            }
        }
    }
}
