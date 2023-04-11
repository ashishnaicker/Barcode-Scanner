using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace Barcode_Scanner
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            barcodeView.Options = new BarcodeReaderOptions
            {
                Formats = BarcodeFormats.All,
                AutoRotate = true,
                Multiple = true
            };
        }

        protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        {
            foreach (var barcode in e.Results)
                Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

            var first = e.Results?.FirstOrDefault();
            if (first is not null)
            {
                Dispatcher.Dispatch(async () =>
                {
                    barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                    barcodeGenerator.Format = first.Format;
                    barcodeGenerator.Value = first.Value;

                    await App.BarcodeRepository.AddNewBarcodeAsync(first.Format.ToString(), first.Value, DateTime.Now.ToString());
                    NotificationLabel.Text = App.BarcodeRepository.StatusMessage;
                });
            }
        }

        void SwitchCameraButton_Clicked(object sender, EventArgs e)
        {
            barcodeView.CameraLocation = barcodeView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
        }

        void TorchButton_Clicked(object sender, EventArgs e)
        {
            barcodeView.IsTorchOn = !barcodeView.IsTorchOn;
        }

        private void HistoryToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Views.HistoryPage());
        }

        private void AboutToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Views.AboutPage());
        }
    }
}