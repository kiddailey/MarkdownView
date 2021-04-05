using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Markdown.Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamplesPage : ContentPage
    {
        public SamplesPage()
        {
            InitializeComponent();
        }

        private void SettingsButton_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await scrollView.ScrollToAsync(settings, ScrollToPosition.Start, true);
            });
        }

        private void SourceButton_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await scrollView.ScrollToAsync(source, ScrollToPosition.Start, true);
            });
        }
    }
}