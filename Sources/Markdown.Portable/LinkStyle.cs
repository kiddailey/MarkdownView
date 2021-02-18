using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xam.Forms.Markdown
{
    public class LinkStyle : MarkdownStyle
    {
        public string OpenLinkSheetTitle { get; set; } = "Open link";
        public string OpenLinkSheetCancel { get; set; } = "Cancel";
        public Action<List<LinkData>> CustomTapHandler { get; set; }
    }

    public class LinkData
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }

}
