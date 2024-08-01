using MauiMarkdown.Styles;

namespace MauiMarkdown
{

    public class MarkdownTheme
    {
        public MarkdownTheme()
        {
            Paragraph = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                FontSize = 12,
            };

            Heading1 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                BorderSize = 1,
                FontSize = 26,
            };

            Heading2 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                BorderSize = 1,
                FontSize = 22,
            };

            Heading3 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 20,
            };

            Heading4 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 18,
            };

            Heading5 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 16,
            };

            Heading6 = new MarkdownStyle
            {
                Attributes = FontAttributes.Bold,
                FontSize = 14,
            };

            Link = new LinkStyle
            {
                Attributes = FontAttributes.None,
                FontSize = 12,
            };

            Code = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                FontSize = 12,
            };

            Quote = new MarkdownStyle
            {
                Attributes = FontAttributes.None,
                BorderSize = 4,
                FontSize = 12,
                BackgroundColor = Colors.Gray.MultiplyAlpha(.1f),
            };

            Separator = new MarkdownStyle
            {
                BorderSize = 2,
            };

            OrderedList = new ListStyle
            {
                BulletStyleType = ListStyleType.Decimal,
                BulletVerticalOptions = LayoutOptions.Start,
                ItemVerticalOptions = LayoutOptions.Start,
            };

            UnorderedList = new ListStyle
            {
                BulletStyleType = ListStyleType.Square,
                BulletVerticalOptions = LayoutOptions.Center,
                ItemVerticalOptions = LayoutOptions.Center,
            };

            // Platform specific properties
            // TODO Xamarin.Forms.Device.RuntimePlatform is no longer supported. Use Microsoft.Maui.Devices.DeviceInfo.Platform instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Code.FontFamily = "Courier";
                    break;

                case Device.Android:
                    Code.FontFamily = "monospace";
                    break;
            };

            // MAUI cannot currently do subscript in FormattedStrings
            // So we simulate with making the font smaller
            SubScript = new MarkdownStyle
            {
                FontSize = 6
            };

            // MAUI cannot currently do superscript in FormattedStrings
            // So we simulate with making the font smaller
            SuperScript = new MarkdownStyle
            {
                FontSize = 6
            };

            Marked = new MarkdownStyle
            {
                BackgroundColor = Colors.Yellow
            };

            Inserted = new MarkdownStyle
            {
                TextDecorations = TextDecorations.Underline
            };

            Table = new TableStyle
            {

            };
        }

        public Color BackgroundColor { get; set; }

        public MarkdownStyle Paragraph { get; set; }

        public MarkdownStyle Heading1 { get; set; }

        public MarkdownStyle Heading2 { get; set; }

        public MarkdownStyle Heading3 { get; set; }

        public MarkdownStyle Heading4 { get; set; }

        public MarkdownStyle Heading5 { get; set; }

        public MarkdownStyle Heading6 { get; set; }

        public MarkdownStyle Quote { get; set; }

        public MarkdownStyle Separator { get; set; }

        public LinkStyle Link { get; set; }

        public MarkdownStyle Code { get; set; }

        public ListStyle OrderedList { get; set; }

        public ListStyle UnorderedList { get; set; }

        public float Margin { get; set; } = 10;

        public float VerticalSpacing { get; set; } = 10;

        public bool UseEmojiAndSmileyExtension { get; set; }

        public bool UseEmphasisExtrasExtension {  get; set; }
        public MarkdownStyle SuperScript {  get; set; }
        public MarkdownStyle SubScript {  get; set; }
        public MarkdownStyle Marked {  get; set; }
        public MarkdownStyle Inserted {  get; set; }

        public bool UseTablesExtension {  get; set; }
        public TableStyle Table { get; set; }
    }

    public class LightMarkdownTheme : MarkdownTheme
    {
        public LightMarkdownTheme()
        {
            BackgroundColor = DefaultBackgroundColor;
            Paragraph.ForegroundColor = DefaultTextColor;
            Heading1.ForegroundColor = DefaultTextColor;
            Heading1.BorderColor = DefaultSeparatorColor;
            Heading2.ForegroundColor = DefaultTextColor;
            Heading2.BorderColor = DefaultSeparatorColor;
            Heading3.ForegroundColor = DefaultTextColor;
            Heading4.ForegroundColor = DefaultTextColor;
            Heading5.ForegroundColor = DefaultTextColor;
            Heading6.ForegroundColor = DefaultTextColor;
            Link.ForegroundColor = DefaultAccentColor;
            Code.ForegroundColor = DefaultTextColor;
            Code.BackgroundColor = DefaultCodeBackground;
            Quote.ForegroundColor = DefaultQuoteTextColor;
            Quote.BorderColor = DefaultQuoteBorderColor;
            Separator.BorderColor = DefaultSeparatorColor;
            Marked.BackgroundColor = DefaultMarkedBackgroundColor;
        }

        public static readonly Color DefaultBackgroundColor = Color.FromArgb("#ffffff");

        public static readonly Color DefaultAccentColor = Color.FromArgb("#0366d6");

        public static readonly Color DefaultTextColor = Color.FromArgb("#24292e");

        public static readonly Color DefaultCodeBackground = Color.FromArgb("#f6f8fa");

        public static readonly Color DefaultSeparatorColor = Color.FromArgb("#eaecef");

        public static readonly Color DefaultQuoteTextColor = Color.FromArgb("#6a737d");

        public static readonly Color DefaultQuoteBorderColor = Color.FromArgb("#dfe2e5");

        public static readonly Color DefaultMarkedBackgroundColor = Color.FromArgb("#ffff8f");
    }

    public class DarkMarkdownTheme : MarkdownTheme
    {
        public DarkMarkdownTheme()
        {
            BackgroundColor = DefaultBackgroundColor;
            Paragraph.ForegroundColor = DefaultTextColor;
            Heading1.ForegroundColor = DefaultTextColor;
            Heading1.BorderColor = DefaultSeparatorColor;
            Heading2.ForegroundColor = DefaultTextColor;
            Heading2.BorderColor = DefaultSeparatorColor;
            Heading3.ForegroundColor = DefaultTextColor;
            Heading4.ForegroundColor = DefaultTextColor;
            Heading5.ForegroundColor = DefaultTextColor;
            Heading6.ForegroundColor = DefaultTextColor;
            Link.ForegroundColor = DefaultAccentColor;
            Code.ForegroundColor = DefaultTextColor;
            Code.BackgroundColor = DefaultCodeBackground;
            Quote.ForegroundColor = DefaultQuoteTextColor;
            Quote.BorderColor = DefaultQuoteBorderColor;
            Separator.BorderColor = DefaultSeparatorColor;
            Marked.BackgroundColor = DefaultMarkedBackgroundColor;
        }

        public static readonly Color DefaultBackgroundColor = Color.FromArgb("#2b303b");

        public static readonly Color DefaultAccentColor = Color.FromArgb("#d08770");

        public static readonly Color DefaultTextColor = Color.FromArgb("#eff1f5");

        public static readonly Color DefaultCodeBackground = Color.FromArgb("#4f5b66");

        public static readonly Color DefaultSeparatorColor = Color.FromArgb("#65737e");

        public static readonly Color DefaultQuoteTextColor = Color.FromArgb("#a7adba");

        public static readonly Color DefaultQuoteBorderColor = Color.FromArgb("#a7adba");

        public static readonly Color DefaultMarkedBackgroundColor = Color.FromArgb("#ffff8f");
    }
}
