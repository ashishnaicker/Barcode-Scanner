using Barcode_Scanner.Data;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace Barcode_Scanner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "barcodes.db3");
            builder.Services.AddSingleton<BarcodeRepository>(s => ActivatorUtilities.CreateInstance<BarcodeRepository>(s, dbPath));

            return builder.Build();
        }
    }
}