using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace PetsDairy.Utils
{
    public class AzureStorageHelper
    {
        private static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like
        /// the user hasn't updated this to valid values.
        /// </summary>
        /// <param name="storageConnectionString">Connection string for the storage service or the emulator</param>
        /// <returns>CloudStorageAccount object</returns>
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Logger.Error("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Logger.Error("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                throw;
            }

            return storageAccount;
        }

        /// <summary>
        /// Create a table for the sample application to process messages in.
        /// </summary>
        /// <returns>A CloudTable object</returns>
        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service
            CloudTable table = tableClient.GetTableReference(tableName);
            try
            {
                if (await table.CreateIfNotExistsAsync())
                {
                    Logger.InfoFormat("Created Table named: {0}", tableName);
                }
                else
                {
                    Logger.InfoFormat("Table {0} already exists", tableName);
                }
            }
            catch (StorageException ex)
            {
                Logger.ErrorFormat("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample. Error details:{0}", ex);
                throw;
            }
            return table;
        }
    }
}