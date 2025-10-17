using System.Threading.Tasks;

namespace TestTask.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Shows a save file dialog and returns the selected file path.
        /// </summary>
        /// <param name="defaultFileName">The default file name.</param>
        /// <param name="extension">The default file extension (e.g., "xlsx").</param>
        /// <returns>The selected file path, or null if the dialog is cancelled.</returns>
        Task<string?> SaveFileAsync(string defaultFileName, string extension);
    }
}
