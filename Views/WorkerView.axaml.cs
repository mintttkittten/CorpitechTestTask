using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TestTask.Views
{
    public partial class WorkerView : UserControl
    {
        public WorkerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
