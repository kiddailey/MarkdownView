﻿using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Markdown.Samples.ViewModels
{
    public class SettingsCardViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public bool IsOpened { get; set; }
        public ICommand ToggleCommand { get; set; }
        public ObservableCollection<SettingsItemViewModel> Items { get; set; } = new ObservableCollection<SettingsItemViewModel>();

        public SettingsCardViewModel(string name)
        {
            Name = name;
            ToggleCommand = new Command(() =>
            {
                IsOpened = !IsOpened;
            });
        }

    }
}
