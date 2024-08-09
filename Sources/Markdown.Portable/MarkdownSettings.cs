using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Markdig.Syntax;
using MauiMarkdown.Classes;

namespace MauiMarkdown
{

    public class YouTubePreview
    {
        public Func<VideoPreviewDescriptor, string> GenerateLoadImageUrl { get; set; }
        public Func<VideoPreviewDescriptor, ImageSource> CustomLoadImage { get; set; }
        public Func<Image, VideoPreviewDescriptor, View> TransformView { get; set; }
    }

    public class MarkdownSettings : INotifyPropertyChanged
    {
        bool useAutolinksExtension;
        bool useEmojiAndSmileyExtension;
        bool useEmphasisExtrasExtension;
        bool useTablesExtension;
        bool useTaskListsExtension;
        bool loadYoutubePreviews;
        string openLinkSheetCancel = "Cancel";
        string openLinkSheetTitle = "Open Link";

        public string ListSymbol { get; set; } = "*";
        public ListStyleType ListStyle { get; set; } = ListStyleType.Circle;
        public Func<int, ListBlock, ListItemBlock, View> ListBulletCustomCallback { get; set; }
        public Action<List<LinkData>> CustomTapHandler { get; set; }
        public List<string> ExternalProtocols = new List<string> { "http://", "https://", "mailto:", "tel:" };
        public YouTubePreview YouTubePreview { get; set; }

        public MarkdownSettings()
        {

        }

        public MarkdownSettings(MarkdownSettings settings)
        {
            useAutolinksExtension = settings.useAutolinksExtension;
            useEmojiAndSmileyExtension = settings.useEmojiAndSmileyExtension;
            useEmphasisExtrasExtension = settings.useEmphasisExtrasExtension;
            useTablesExtension = settings.useTablesExtension;
            useTaskListsExtension = settings.useTaskListsExtension;
            loadYoutubePreviews = settings.loadYoutubePreviews;
            openLinkSheetCancel = settings.openLinkSheetCancel;
            openLinkSheetTitle = settings.openLinkSheetTitle;

            ListSymbol = settings.ListSymbol;
            ListStyle = settings.ListStyle;
            ListBulletCustomCallback = settings.ListBulletCustomCallback;
            ExternalProtocols = new List<string>(settings.ExternalProtocols);
            YouTubePreview = settings.YouTubePreview;
        }

        public bool UseAutolinksExtension
        {
            get => useAutolinksExtension;
            set
            {
                useAutolinksExtension = value;
                RaisePropertyChanged(nameof(UseAutolinksExtension));
            }
        }

        public bool UseEmojiAndSmileyExtension
        {
            get => useEmojiAndSmileyExtension;
            set
            {
                useEmojiAndSmileyExtension = value;
                RaisePropertyChanged(nameof(UseEmojiAndSmileyExtension));
            }
        }

        public bool UseEmphasisExtrasExtension
        {
            get => useEmphasisExtrasExtension;
            set
            {
                useEmphasisExtrasExtension = value;
                RaisePropertyChanged(nameof(UseEmphasisExtrasExtension));
            }
        }

        public bool UseTablesExtension
        {
            get => useTablesExtension;
            set
            {
                useTablesExtension = value;
                RaisePropertyChanged(nameof(UseTablesExtension));
            }
        }

        public bool UseTaskListsExtension
        {
            get => useTaskListsExtension;
            set
            {
                useTaskListsExtension = value;
                RaisePropertyChanged(nameof(UseTaskListsExtension));
            }
        }

        public bool LoadYoutubePreviews
        {
            get => loadYoutubePreviews;
            set
            {
                loadYoutubePreviews = value;
                RaisePropertyChanged(nameof(LoadYoutubePreviews));
            }
        }

        public string OpenLinkSheetTitle
        {
            get => openLinkSheetTitle;
            set
            {
                openLinkSheetTitle = value;
                RaisePropertyChanged(nameof(OpenLinkSheetTitle));
            }
        }

        public string OpenLinkSheetCancel
        {
            get => openLinkSheetCancel;
            set
            {
                openLinkSheetCancel = value;
                RaisePropertyChanged(nameof(OpenLinkSheetCancel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
