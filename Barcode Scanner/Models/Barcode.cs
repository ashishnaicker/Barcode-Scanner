using SQLite;

namespace Barcode_Scanner.Models
{
    [Table("barcodes")]
    public class Barcode
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public string Format { get; set; }

        public string Value { get; set; }

        public string DateScanned { get; set; }
    }
}
