using Markdown.Samples.Views;

namespace Markdown.Samples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SamplesPage();
        }
    }
}
