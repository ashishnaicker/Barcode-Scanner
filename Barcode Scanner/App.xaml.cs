using Barcode_Scanner.Data;

namespace Barcode_Scanner
{
    public partial class App : Application
    {
        public static BarcodeRepository BarcodeRepository { get; private set; }

        public App(BarcodeRepository  repository)
        {
            InitializeComponent();

            MainPage = new AppShell();

            BarcodeRepository = repository;
        }
    }
}