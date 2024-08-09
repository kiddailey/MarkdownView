using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Markdown.Samples.Utils;
using MauiMarkdown;

namespace Markdown.Samples.ViewModels
{
    public class ChangeSettingMessage : ValueChangedMessage<MarkdownSettings>
    {
        public ChangeSettingMessage(MarkdownSettings settings) : base(settings) { }
    }

    public class SettingsViewModel : BaseViewModel
    {
        public MarkdownSettings Settings { get; set; }
        public SettingsCardViewModel BasicSettings { get; set; }
        public SettingsCardViewModel LinkSettings { get; set; }

        public bool IsDarkTheme { get; set; }

        public SettingsViewModel()
        {
            Settings = new MarkdownSettings();
            InitSettings();
        }

        public class Change
        {
            public string Target;
            public bool Value;
        }

        public class SetEmphasisExtrasMessage : ValueChangedMessage<bool>
        {
            public SetEmphasisExtrasMessage(bool value) : base(value) { }
        }

        void InitSettings()
        {
            BasicSettings = new SettingsCardViewModel("Basic settings")
            {
                IsOpened = true,
                Items = new ObservableCollection<SettingsItemViewModel>
                {
                    new SwitchSettingsItemViewModel(this, "Dark mode")
                    {
                        Action = (bool value) => {
                            Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
                        }
                    },
                    new SwitchSettingsItemViewModel(this, "Use Emphasis Extras extension")
                    {
                        Action = (bool value) => {
                            Settings.UseEmphasisExtrasExtension = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new SwitchSettingsItemViewModel(this, "Use Pipe and Grid Table extensions")
                    {
                        Action = (bool value) => {
                            Settings.UseTablesExtension = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new SwitchSettingsItemViewModel(this, "Use Task Lists extensions")
                    {
                        Action = (bool value) => {
                            Settings.UseTaskListsExtension = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new SwitchSettingsItemViewModel(this, "Use emoji and smiley extension")
                    {
                        Action = (bool value) => {
                            Settings.UseEmojiAndSmileyExtension = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new SwitchSettingsItemViewModel(this, "YouTube preview")
                    {
                        Action = (bool value) => {
                            Settings.LoadYoutubePreviews = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new SwitchSettingsItemViewModel(this, "Use autolink extension")
                    {
                        Action = (bool value) => {
                            Settings.UseAutolinksExtension = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new EntrySettingsItemViewModel(this, "Open link title", Settings.OpenLinkSheetTitle){
                        Action = (string value) => {
                            Settings.OpenLinkSheetTitle = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                    new EntrySettingsItemViewModel(this, "Open link cancel", Settings.OpenLinkSheetCancel){
                        Action = (string value) => {
                            Settings.OpenLinkSheetCancel = value;
                            WeakReferenceMessenger.Default.Send(Settings);
                        },
                    },
                }
            };
        }
    }
}