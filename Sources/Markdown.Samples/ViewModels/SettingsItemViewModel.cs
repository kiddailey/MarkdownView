namespace Markdown.Samples.ViewModels
{
    public abstract class SettingsItemViewModel : BaseViewModel
    {
        protected readonly SettingsViewModel vm;
        public string Label { get; set; }
        public bool IsVisible { get; set; } = true;

        public SettingsItemViewModel(SettingsViewModel vm, string label)
        {
            this.vm = vm;
            Label = label;
        }
    }

    public class SwitchSettingsItemViewModel : SettingsItemViewModel
    {
        public SwitchSettingsItemViewModel(SettingsViewModel vm, string label) : base(vm, label)
        {
        }

        public bool IsToggled { get; set; }

        public Action<bool> Action { get; set; }

        public void OnIsToggledChanged()
        {
            Action?.Invoke(IsToggled);
            vm.RaisePropertyChanged(nameof(SamplePageViewModel.Settings));
        }
    }

    public class EntrySettingsItemViewModel : SettingsItemViewModel
    {
        public EntrySettingsItemViewModel(SettingsViewModel vm, string label, string value) : base(vm, label)
        {
            Value = value;
        }

        public string Value { get; set; }

        public Action<string> Action { get; set; }

        public void OnValueChanged()
        {
            Action?.Invoke(Value);
            vm.RaisePropertyChanged(nameof(SamplePageViewModel.Settings));
        }
    }
    
    public class EditorSettingsItemViewModel : SettingsItemViewModel
    {
        public EditorSettingsItemViewModel(SettingsViewModel vm, string label, string value) : base(vm, label)
        {
            Value = value;
        }

        public string Value { get; set; }

        public Action<string> Action { get; set; }

        public void OnValueChanged()
        {
            Action?.Invoke(Value);
            vm?.RaisePropertyChanged(nameof(SamplePageViewModel.Settings));
        }
    }

    public class StepperSettingsItemViewModel : SettingsItemViewModel
    {
        public StepperSettingsItemViewModel(SettingsViewModel vm, string label, int defaultValue) : base(vm, label)
        {
            Value = defaultValue;
        }

        public int Value { get; set; }

        public Action<int> Action { get; set; }

        public void OnValueChanged()
        {
            Action?.Invoke(Value);
            vm.RaisePropertyChanged(nameof(SamplePageViewModel.Settings));
        }
    }

    public class PickerSettingsViewModel : SettingsItemViewModel
    {
        public PickerSettingsViewModel(SettingsViewModel vm, string label, List<object> items, object selectedItem) : base(vm, label)
        {
            Items = items;
            SelectedItem = selectedItem;
        }

        public List<object> Items { get; set; }

        public object SelectedItem { get; set; }


        public Action<object> Action { get; set; }

        public void OnSelectedItemChanged()
        {
            Action?.Invoke(SelectedItem);
            vm?.RaisePropertyChanged(nameof(SamplePageViewModel.Settings));
        }
    }
}
