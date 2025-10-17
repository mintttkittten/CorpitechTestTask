using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using TestTask.Services;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using TestTask.ViewModels;
using TestTask.Views;
using System.Globalization;
using System.Threading;

namespace TestTask;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            culture.NumberFormat.CurrencySymbol = "BYN";
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            DisableAvaloniaDataAnnotationValidation();

            var mainWindow = new MainWindow();
            var fileService = new FileService(mainWindow);
            mainWindow.DataContext = new WorkerListViewModel(fileService);

            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
