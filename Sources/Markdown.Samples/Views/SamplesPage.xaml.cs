namespace Markdown.Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamplesPage : ContentPage
    {
        public SamplesPage()
        {
            InitializeComponent();
        }

        void SettingsButton_Clicked(object sender, EventArgs e)
        {
            Dispatcher.Dispatch(async () =>
            {
                await scrollView.ScrollToAsync(settings, ScrollToPosition.Start, true);
            });
        }

        void SourceButton_Clicked(object sender, EventArgs e)
        {
            Dispatcher.Dispatch(async () =>
            {
                await scrollView.ScrollToAsync(source, ScrollToPosition.Start, true);
            });
        }
    }
}