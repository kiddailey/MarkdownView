using Markdown.Samples.ViewModels;

namespace Markdown.Samples.Selectors
{
    public class SettingsItemSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var list = (StackLayout)container;

            if (item is SwitchSettingsItemViewModel)
            {
                return (DataTemplate)list.Resources["Switch"];
            }

            if (item is EntrySettingsItemViewModel)
            {
                return (DataTemplate)list.Resources["Entry"];
            }

            if (item is StepperSettingsItemViewModel)
            {
                return (DataTemplate)list.Resources["Stepper"];
            }
            
            if (item is PickerSettingsViewModel)
            {
                return (DataTemplate)list.Resources["Picker"];
            }
            
            if (item is EditorSettingsItemViewModel)
            {
                return (DataTemplate)list.Resources["Editor"];
            }

            return null;
        }
    }
}
