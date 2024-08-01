using System.Collections.ObjectModel;
using Markdown.Samples.Utils;
using Xam.Forms.Markdown;

namespace Markdown.Samples.ViewModels
{
    public class SamplePageViewModel : BaseViewModel
    {
        public string embeded = "# An exhibit of Markdown\n\n![Unsplash](https://unsplash.it/200/300)This note demonstrates some of what [Markdown][1] is capable of doing.\n\n*Note: Feel free to play with this page. Unlike regular notes, this doesn't automatically save itself.*\n\n## Basic formatting\n\nParagraphs can be written like so. A paragraph is the basic block of Markdown. A paragraph is what text will turn into when there is no reason it should become anything else.\n\nParagraphs must be separated by a blank line. Basic formatting of *italics* and **bold** is supported. This *can be **nested** like* so.\n\n## Lists\n\n### Ordered list\n\n1. Item 1\n 2. A second item\n3. Number 3\n4. Ⅳ\n\n*Note: the fourth item uses the Unicode character for [Roman numeral four][2].*\n\n### Unordered list\n\n* An item\n * Another item\n  * Yet another item\n* And there's more...\n\n## Paragraph modifiers\n\n### Code block\n\n    Code blocks are very useful for developers and other people who look at code or other things that are written in plain text. As you can see, it uses a fixed-width font.\n\nYou can also make `inline code` to add code into other things.\n\n### Quote\n\n> Here is a quote. What this is should be self explanatory. Quotes are automatically indented when they are used.\n\n## Headings\n\nThere are six levels of headings. They correspond with the six levels of HTML headings. You've probably noticed them already in the page. Each level down uses one more hash character.\n\n### Headings *can* also contain **formatting**\n\n### They can even contain `inline code`\n\nOf course, demonstrating what headings look like messes up the structure of the page.\n\nI don't recommend using more than three or four levels of headings here, because, when you're smallest heading isn't too small, and you're largest heading isn't too big, and you want each size up to look noticeably larger and more important, there there are only so many sizes that you can use.\n\n## URLs\n\nURLs can be made in a handful of ways:\n\n* A named link to [MarkItDown][3]. The easiest way to do these is to select what you want to make a link and hit `Ctrl+L`.\n* Another named link to [MarkItDown](http://www.markitdown.net/)\n* You can also use autolinks like <http://foo.bar.baz> or <http://foo.bar.baz/test?q=hello&id=22&boolean>. Mails are also supported <MAILTO:FOO@BAR.BAZ> or <foo@bar.baz>\n\n**YouTube previews** [YouTube](https://www.youtube.com/watch?v=qgIYv8fG7Qk) <https://www.youtube.com/watch?v=_N-IREy7C9s&t=7936s> https://www.youtube.com/watch?v=91E_lYSUmg8&t=929s https://www.youtube-nocookie.com/embed/21UW2-u1KNo\n\n## Horizontal rule\n\nA horizontal rule is a line that goes across the middle of the page.\n\n---\n\nIt's sometimes handy for breaking things up.\n\n## Images\n\nMarkdown can also contain images. I'll need to add something here sometime.\n\n## Emojis \n\nYou can use ASCI emojis like :) or :ok_hand:. This feature is disabled by default, set UseEmojiAndSmileyExtension=true for enabling it.\n\n## Finally\n\nThere's actually a lot more to Markdown than this. See the official [introduction][4] and [syntax][5] for more information. However, be aware that this is not using the official implementation, and this might work subtly differently in some of the little things.\n\n  [1]: http://daringfireball.net/projects/markdown/\n  [2]: http://www.fileformat.info/info/unicode/char/2163/index.htm\n  [3]: http://www.markitdown.net/\n  [4]: http://daringfireball.net/projects/markdown/basics\n  [5]: http://daringfireball.net/projects/markdown/syntax";

        public MarkdownTheme Theme => GetTheme();
        public string Markdown { get; set; }

        readonly List<object> listStyleTypeItems = new List<object>
        {
            ListStyleType.Square,
            ListStyleType.Circle,
            ListStyleType.None,
            ListStyleType.Symbol,
            ListStyleType.Decimal
        };

        readonly List<object> fontAttributeItems = new List<object>
        {
            FontAttributes.None,
            FontAttributes.Bold,
            FontAttributes.Italic,
        };

        readonly List<object> textDecorationItems = new List<object>
        {
            TextDecorations.None,
            TextDecorations.Underline,
            TextDecorations.Strikethrough,
        };

        readonly List<object> textAlignmentItems = new List<object>
        {
            TextAlignment.Start,
            TextAlignment.Center,
            TextAlignment.End,
        };

        readonly List<object> layoutOptionsItems = new List<object>
        {
            new LayoutOptionsWrapper(LayoutOptions.Start),
            new LayoutOptionsWrapper(LayoutOptions.Center),
            new LayoutOptionsWrapper(LayoutOptions.End),
            new LayoutOptionsWrapper(LayoutOptions.Fill),
            new LayoutOptionsWrapper(LayoutOptions.StartAndExpand),
            new LayoutOptionsWrapper(LayoutOptions.CenterAndExpand),
            new LayoutOptionsWrapper(LayoutOptions.EndAndExpand),
            new LayoutOptionsWrapper(LayoutOptions.FillAndExpand),
        };

        public SettingsCardViewModel SourceCard { get; set; }
        public SettingsCardViewModel BasicSettings { get; set; }
        public SettingsCardViewModel ParagraphSettings { get; set; }
        public SettingsCardViewModel Heading1Settings { get; set; }
        public SettingsCardViewModel Heading2Settings { get; set; }
        public SettingsCardViewModel Heading3Settings { get; set; }
        public SettingsCardViewModel Heading4Settings { get; set; }
        public SettingsCardViewModel Heading5Settings { get; set; }
        public SettingsCardViewModel Heading6Settings { get; set; }
        public SettingsCardViewModel LinkSettings { get; set; }
        public SettingsCardViewModel UnorderedListSettings { get; set; }
        public SettingsCardViewModel OrderedListSettings { get; set; }
        public SettingsCardViewModel CodeSettings { get; set; }
        public SettingsCardViewModel QuoteSettings { get; set; }

        MarkdownTheme themeTemplate;
        bool isDarkTheme;

        public SamplePageViewModel()
        {
            themeTemplate = new LightMarkdownTheme();

            Markdown = embeded;

            InitSource();
            InitSettings();
        }

        void InitSource()
        {
            var sources = new List<object>
            {
                "Embedded",
                "Custom",
            };

            var customSourceItem = new EditorSettingsItemViewModel(null, "Custom source", "")
            {
                IsVisible = false,
                Action = (string value) => Markdown = value,
            };

            var sourceTypeItem = new PickerSettingsViewModel(null, "Source", sources, "Embedded")
            {
                Action = (object value) =>
                {
                    var isCustom = $"{value}" == "Custom";
                    customSourceItem.IsVisible = false;

                    switch ($"{value}")
                    {
                        case "Embedded":
                            Markdown = embeded;
                            break;
                        case "Custom":
                            customSourceItem.IsVisible = true;
                            Markdown = customSourceItem.Value;
                            break;
                    }


                }
            };

            SourceCard = new SettingsCardViewModel("Source")
            {
                Items = new ObservableCollection<SettingsItemViewModel>
                {
                    sourceTypeItem,
                    customSourceItem,
                }
            };
        }

        void InitSettings()
        {
            BasicSettings = new SettingsCardViewModel("Basic settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>
                {
                    new SwitchSettingsItemViewModel(this, "Dark mode")
                    {
                        Action = (bool value) => isDarkTheme = value,
                    },
                    new SwitchSettingsItemViewModel(this, "Use emoji and smiley extension")
                    {
                        Action = (bool value) => themeTemplate.UseEmojiAndSmileyExtension = value,
                    },
                    new StepperSettingsItemViewModel(this, "Vertical spacing", (int)themeTemplate.VerticalSpacing)
                    {
                        Action = (int value) => themeTemplate.VerticalSpacing = value,
                    },
                    new StepperSettingsItemViewModel(this, "Margin", (int)themeTemplate.Margin)
                    {
                        Action = (int value) => themeTemplate.Margin = value,
                    }
                }
            };

            ParagraphSettings = new SettingsCardViewModel("Paragraph settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Paragraph, showBorderSize: false)),
            };

            Heading1Settings = new SettingsCardViewModel("Heading1 settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Heading1)),
            };

            Heading2Settings = new SettingsCardViewModel("Heading2 settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Heading2)),
            };

            Heading3Settings = new SettingsCardViewModel("Heading3 settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Heading3)),
            };

            Heading4Settings = new SettingsCardViewModel("Heading4 settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Heading4)),
            };

            Heading5Settings = new SettingsCardViewModel("Heading5 settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Heading5)),
            };

            Heading6Settings = new SettingsCardViewModel("Heading6 settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Heading6)),
            };

            CodeSettings = new SettingsCardViewModel("Code settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Code, showBorderSize: false)),
            };

            QuoteSettings = new SettingsCardViewModel("Quote settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Quote)),
            };

            LinkSettings = new SettingsCardViewModel("Link settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetMarkdownStyleSettings(themeTemplate.Link, showBorderSize: false))
                {
                    new SwitchSettingsItemViewModel(this, "YouTube preview")
                    {
                        Action = (bool value) => themeTemplate.Link.LoadYouTubePreview = value,
                    },
                    new SwitchSettingsItemViewModel(this, "Use autolinks")
                    {
                        Action = (bool value) => themeTemplate.Link.UseAutolinksExtension = value,
                    },
                    new EntrySettingsItemViewModel(this, "Open link title", themeTemplate.Link.OpenLinkSheetTitle){
                        Action = (string value) => themeTemplate.Link.OpenLinkSheetTitle = value,
                    },
                    new EntrySettingsItemViewModel(this, "Open link cancel", themeTemplate.Link.OpenLinkSheetCancel){
                        Action = (string value) => themeTemplate.Link.OpenLinkSheetCancel = value,
                    },
                }
            };

            UnorderedListSettings = new SettingsCardViewModel("Unordered list settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetListStyleSettings(themeTemplate.UnorderedList)),
            };

            OrderedListSettings = new SettingsCardViewModel("Ordered list settings")
            {
                Items = new ObservableCollection<SettingsItemViewModel>(GetListStyleSettings(themeTemplate.OrderedList)),
            };
        }

        List<SettingsItemViewModel> GetMarkdownStyleSettings(MarkdownStyle style, bool showBorderSize = true)
        {
            return new List<SettingsItemViewModel>
            {
                new StepperSettingsItemViewModel(this, "Font size", (int)style.FontSize)
                {
                    Action = (int value) => style.FontSize = value,
                },
                new StepperSettingsItemViewModel(this, "Line height", (int)style.LineHeight)
                {
                    Action = (int value) => style.LineHeight = value,
                },
                new PickerSettingsViewModel(this, "Font attributes", fontAttributeItems, style.Attributes)
                {
                    Action = (object value) => style.Attributes = (FontAttributes)value,
                },
                new PickerSettingsViewModel(this, "Text decorations", textDecorationItems, style.TextDecorations)
                {
                    Action = (object value) => style.TextDecorations = (TextDecorations)value,
                },
                new PickerSettingsViewModel(this, "Horizontal text alignment", textAlignmentItems, style.HorizontalTextAlignment)
                {
                    Action = (object value) => style.HorizontalTextAlignment = (TextAlignment)value,
                },
                new PickerSettingsViewModel(this, "Vertical text alignment", textAlignmentItems, style.VerticalTextAlignment)
                {
                    Action = (object value) => style.VerticalTextAlignment = (TextAlignment)value,
                },
                new StepperSettingsItemViewModel(this, "Border size", (int)style.BorderSize)
                {
                    Action = (int value) => style.BorderSize = value,
                    IsVisible = showBorderSize,
                },
            };
        }

        List<SettingsItemViewModel> GetListStyleSettings(ListStyle style)
        {
            var items = new List<SettingsItemViewModel>();

            var bulletSymbolItem = new EntrySettingsItemViewModel(this, "Symbol", style.Symbol)
            {
                Action = (string value) => style.Symbol = value,
                IsVisible = false,
            };

            var bulletSizeItem = new StepperSettingsItemViewModel(this, "Bullet size", style.BulletSize)
            {
                Action = (int value) => style.BulletSize = value,
                IsVisible = style.BulletStyleType == ListStyleType.Circle || style.BulletStyleType == ListStyleType.Square,
            };
             
            var bulletFontSizeItem = new StepperSettingsItemViewModel(this, "Bullet font size", (int)(style.BulletFontSize ?? 0))
            {
                Action = (int value) => style.BulletFontSize = value,
                IsVisible = style.BulletStyleType == ListStyleType.Decimal || style.BulletStyleType == ListStyleType.Symbol,
            };

            var bulletLineHeightItem = new StepperSettingsItemViewModel(this, "Bullet line height", (int)(style.BulletLineHeight ?? 0))
            {
                Action = (int value) => style.BulletLineHeight = value,
                IsVisible = style.BulletStyleType == ListStyleType.Decimal || style.BulletStyleType == ListStyleType.Symbol,
            };

            var bulletFontAttributesItem = new PickerSettingsViewModel(this, "Bullet font attributes", fontAttributeItems, style.BulletFontAttributes)
            {
                Action = (object value) => style.BulletFontAttributes = (FontAttributes)value,
                IsVisible = style.BulletStyleType == ListStyleType.Decimal || style.BulletStyleType == ListStyleType.Symbol,
            };

            var bulletTypeItem = new PickerSettingsViewModel(this, "Bullet type", listStyleTypeItems, style.BulletStyleType)
            {
                Action = (object value) =>
                {
                    style.BulletStyleType = (ListStyleType)value;
                    bulletSymbolItem.IsVisible = style.BulletStyleType == ListStyleType.Symbol;

                    var isTextType = style.BulletStyleType == ListStyleType.Decimal || style.BulletStyleType == ListStyleType.Symbol;
                    var isBoxType = style.BulletStyleType == ListStyleType.Circle || style.BulletStyleType == ListStyleType.Square;

                    bulletSizeItem.IsVisible = isBoxType;
                    bulletFontSizeItem.IsVisible = isTextType;
                    bulletLineHeightItem.IsVisible = isTextType;
                    bulletFontAttributesItem.IsVisible = isTextType;
                },
            };

            items.AddRange(new List<SettingsItemViewModel> {
                bulletTypeItem,
                bulletSymbolItem,
                bulletFontSizeItem,
                bulletLineHeightItem,
                bulletFontAttributesItem,
                bulletSizeItem,
                new PickerSettingsViewModel(this, "Bullet vertical options", layoutOptionsItems, new LayoutOptionsWrapper(style.BulletVerticalOptions))
                {
                    Action = (object value) => style.BulletVerticalOptions = ((LayoutOptionsWrapper)value).LayoutOptions,
                },
                new PickerSettingsViewModel(this, "Item vertical options", layoutOptionsItems, new LayoutOptionsWrapper(style.ItemVerticalOptions))
                {
                    Action = (object value) => style.ItemVerticalOptions = ((LayoutOptionsWrapper)value).LayoutOptions,
                },
                new StepperSettingsItemViewModel(this, "Indentation", (int)style.Indentation)
                {
                    Action = (int value) => style.Indentation = value,
                },
                new StepperSettingsItemViewModel(this, "Spacing", (int)(style.Spacing ?? 0))
                {
                    Action = (int value) => style.Spacing = value,
                },
                new StepperSettingsItemViewModel(this, "Items vertical spacing", (int)style.ItemsVerticalSpacing)
                {
                    Action = (int value) => style.ItemsVerticalSpacing = value,
                }
            });

            return items;
        }

        MarkdownTheme GetTheme()
        {
            var theme = isDarkTheme ? (MarkdownTheme)new DarkMarkdownTheme() : new LightMarkdownTheme();

            theme.UseEmojiAndSmileyExtension = themeTemplate.UseEmojiAndSmileyExtension;
            theme.VerticalSpacing = themeTemplate.VerticalSpacing;
            theme.Margin = themeTemplate.Margin;

            CopyMarkdownStyle(themeTemplate.Paragraph, theme.Paragraph);
            CopyMarkdownStyle(themeTemplate.Heading1, theme.Heading1);
            CopyMarkdownStyle(themeTemplate.Heading2, theme.Heading2);
            CopyMarkdownStyle(themeTemplate.Heading3, theme.Heading3);
            CopyMarkdownStyle(themeTemplate.Heading4, theme.Heading4);
            CopyMarkdownStyle(themeTemplate.Heading5, theme.Heading5);
            CopyMarkdownStyle(themeTemplate.Heading6, theme.Heading6);
            CopyMarkdownStyle(themeTemplate.Link, theme.Link);
            CopyMarkdownStyle(themeTemplate.Quote, theme.Quote);
            CopyMarkdownStyle(themeTemplate.Code, theme.Code);

            theme.Link.LoadYouTubePreview = themeTemplate.Link.LoadYouTubePreview;
            theme.Link.UseAutolinksExtension = themeTemplate.Link.UseAutolinksExtension;
            theme.Link.OpenLinkSheetTitle = themeTemplate.Link.OpenLinkSheetTitle;
            theme.Link.OpenLinkSheetCancel = themeTemplate.Link.OpenLinkSheetCancel;

            CopyListStyle(themeTemplate.UnorderedList, theme.UnorderedList);
            CopyListStyle(themeTemplate.OrderedList, theme.OrderedList);

            return theme;
        }

        void CopyMarkdownStyle(MarkdownStyle source, MarkdownStyle target)
        {
            target.Attributes = source.Attributes;
            target.FontSize = source.FontSize;
            target.LineHeight = source.LineHeight;
            target.HorizontalTextAlignment = source.HorizontalTextAlignment;
            target.VerticalTextAlignment = source.VerticalTextAlignment;
            target.BorderSize = source.BorderSize;
            target.TextDecorations = source.TextDecorations;
        }

        void CopyListStyle(ListStyle source, ListStyle target)
        {
            target.BulletStyleType = source.BulletStyleType;
            target.Symbol = source.Symbol;
            target.BulletSize = source.BulletSize;
            target.BulletFontSize = source.BulletFontSize;
            target.BulletFontAttributes = source.BulletFontAttributes;
            target.BulletLineHeight = source.BulletLineHeight;
            target.BulletVerticalOptions = source.BulletVerticalOptions;
            target.ItemVerticalOptions = source.ItemVerticalOptions;
            target.Indentation = source.Indentation;
            target.Spacing = source.Spacing;
            target.ItemsVerticalSpacing = source.ItemsVerticalSpacing;
        }
    }
}