using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;

namespace TestTask.Services
{
    public class FileService : IFileService
    {
        private readonly Window _parentWindow;

        public FileService(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public async Task<string?> SaveFileAsync(string defaultFileName, string extension)
        {
            if (_parentWindow.StorageProvider is not { } storageProvider)
            {
                return null;
            }

            var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Save Excel File",
                SuggestedFileName = defaultFileName,
                DefaultExtension = extension,
                FileTypeChoices = new[]
                {
                    new FilePickerFileType($"Excel Workbook (*.{extension})")
                    {
                        Patterns = new[] { $"*.{extension}" },
                        MimeTypes = new[] { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }
                    }
                }
            });

            return file?.Path.LocalPath;
        }
    }
}
