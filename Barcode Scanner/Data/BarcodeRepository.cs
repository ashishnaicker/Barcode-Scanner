using Barcode_Scanner.Models;
using SQLite;

namespace Barcode_Scanner.Data
{
    public class BarcodeRepository
    {
        string _dbPath;
        SQLiteAsyncConnection _connection;

        public string StatusMessage { get; set; }

        private async Task Init()
        {
            if (_connection != null)
                return;

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Barcode>();
        }

        public BarcodeRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewBarcode(string format, string value, string dateScanned)
        {
            int result = 0;
            try
            {
                await Init();

                await _connection.ExecuteAsync("DELETE FROM barcodes WHERE Value = ?", value);

                result = await _connection.InsertAsync(new Barcode
                {
                    Id = Guid.NewGuid(),
                    Format = format,
                    Value = value,
                    DateScanned = dateScanned
                });

                StatusMessage = string.Format("{0} record(s) added at {1} (Fromat: {2}, Value: {3})", result, dateScanned, format, value);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", result, ex.Message);
            }
        }

        public async Task<List<Barcode>> GetAllBarcodes()
        {
            try
            {
                await Init ();
                return await _connection.Table<Barcode>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Barcode>();
        }
    }
}
