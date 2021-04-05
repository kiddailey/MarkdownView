using System;
using System.Collections.Generic;
using System.Text;

namespace Markdown.Samples.ViewModels
{
    public abstract class SettingsItemViewModel : BaseViewModel
    {
        protected readonly SamplePageViewModel vm;
        public string Label { get; set; }
        public bool IsVisible { get; set; } = true;
        public SettingsItemViewModel(SamplePageViewModel vm, string label)
        {
            this.vm = vm;
            Label = label;
        }
    }

    public class SwitchSettingsItemViewModel : SettingsItemViewModel
    {
        public SwitchSettingsItemViewModel(SamplePageViewModel vm, string label) : base(vm, label)
        {
        }

        public bool IsToggled { get; set; }

        public Action<bool> Action { get; set; }

        public void OnIsToggledChanged()
        {
            Action?.Invoke(IsToggled);
            vm.RaisePropertyChanged(nameof(SamplePageViewModel.Theme));
        }
    }

    public class EntrySettingsItemViewModel : SettingsItemViewModel
    {
        public EntrySettingsItemViewModel(SamplePageViewModel vm, string label, string value) : base(vm, label)
        {
            Value = value;
        }

        public string Value { get; set; }

        public Action<string> Action { get; set; }

        public void OnValueChanged()
        {
            Action?.Invoke(Value);
            vm.RaisePropertyChanged(nameof(SamplePageViewModel.Theme));
        }
    }
    
    public class EditorSettingsItemViewModel : SettingsItemViewModel
    {
        public EditorSettingsItemViewModel(SamplePageViewModel vm, string label, string value) : base(vm, label)
        {
            Value = value;
        }

        public string Value { get; set; }

        public Action<string> Action { get; set; }

        public void OnValueChanged()
        {
            Action?.Invoke(Value);
            vm?.RaisePropertyChanged(nameof(SamplePageViewModel.Theme));
        }
    }

    public class StepperSettingsItemViewModel : SettingsItemViewModel
    {
        public StepperSettingsItemViewModel(SamplePageViewModel vm, string label, int defaultValue) : base(vm, label)
        {
            Value = defaultValue;
        }

        public int Value { get; set; }

        public Action<int> Action { get; set; }

        public void OnValueChanged()
        {
            Action?.Invoke(Value);
            vm.RaisePropertyChanged(nameof(SamplePageViewModel.Theme));
        }
    }

    public class PickerSettingsViewModel : SettingsItemViewModel
    {
        public PickerSettingsViewModel(SamplePageViewModel vm, string label, List<object> items, object selectedItem) : base(vm, label)
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
            vm?.RaisePropertyChanged(nameof(SamplePageViewModel.Theme));
        }
    }
}
