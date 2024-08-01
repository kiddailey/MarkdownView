﻿namespace MauiMarkdown
{
    public class LinkStyle : MarkdownStyle
    {
        public string OpenLinkSheetTitle { get; set; } = "Open link";
        public string OpenLinkSheetCancel { get; set; } = "Cancel";
        public Action<List<LinkData>> CustomTapHandler { get; set; }
        public List<string> ExternalProtocols = new List<string> { "http://", "https://", "mailto:", "tel:" };
        public bool LoadYouTubePreview { get; set; }
        public YouTubePreview YouTubePreview { get; set; }
        public bool UseAutolinksExtension { get; set; }
    }

    public class LinkData
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }

    public class YouTubePreview
    {
        public Func<VideoPreviewDescriptor, string> GenerateLoadImageUrl { get; set; }
        public Func<VideoPreviewDescriptor, ImageSource> CustomLoadImage { get; set; }
        public Func<Image, VideoPreviewDescriptor, View> TransformView { get; set; }
    }

}
