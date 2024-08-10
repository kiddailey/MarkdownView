namespace MauiMarkdown
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using Extensions;
    using Markdig;
    using Markdig.Extensions.Abbreviations;
    using Markdig.Extensions.Tables;
    using Markdig.Extensions.TaskLists;
    using Markdig.Syntax;
    using Markdig.Syntax.Inlines;
    using MauiMarkdown.Classes;

    /*
     * https://spec.commonmark.org (Markdown Spec)
     * https://github.github.com/gfm (Github Flavored Markdown Spec)
     */

    public class MarkdownView : ContentView
    {
        public Action<string> NavigateToLink { get; set; } = async (s) => await Browser.OpenAsync(new Uri(s));

        const string youTubeWatchPattern = @"https?:\/\/www\.youtube\.com\/watch\?v=([^&]+)&?";
        const string youTubePattern = @"https?:\/\/youtu\.be/([^&]+)&?";
        const string youTubeCookiePattern = @"https?:\/\/www\.youtube-nocookie\.com\/embed\/([^&]+)&?";

        static readonly WebClient webClient = new WebClient();

        public string Markdown
        {
            get => (string)GetValue(MarkdownProperty);
            set => SetValue(MarkdownProperty, value);
        }

        public static readonly BindableProperty MarkdownProperty = BindableProperty.Create(nameof(Markdown), typeof(string), typeof(MarkdownView), null, propertyChanged: OnMarkdownChanged);

        public string RelativeUrlHost
        {
            get => (string)GetValue(RelativeUrlHostProperty);
            set => SetValue(RelativeUrlHostProperty, value);
        }

        public static readonly BindableProperty RelativeUrlHostProperty = BindableProperty.Create(nameof(RelativeUrlHost), typeof(string), typeof(MarkdownView), null, propertyChanged: OnMarkdownChanged);

        public MarkdownSettings Settings
        {
            get => (MarkdownSettings)GetValue(SettingsProperty);
            set => SetValue(SettingsProperty, value);
        }

        public static readonly BindableProperty SettingsProperty = BindableProperty.Create(nameof(Settings), typeof(MarkdownSettings), typeof(MarkdownView), new MarkdownSettings(), propertyChanged: OnMarkdownChanged);

        public bool RenderEnabled
        {
            get { return (bool)GetValue(RenderEnabledProperty); }
            set { SetValue(RenderEnabledProperty, value); }
        }

        public static readonly BindableProperty RenderEnabledProperty = BindableProperty.Create(nameof(RenderEnabled), typeof(bool), typeof(bool), true, propertyChanged: OnMarkdownChanged);

        bool isQuoted;

        readonly List<View> queuedViews = new List<View>();

        static void OnMarkdownChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as MarkdownView;
            if (!view.RenderEnabled) return;
            view.RenderMarkdown();
        }

        StackLayout stack;

        List<KeyValuePair<string, string>> links = new List<KeyValuePair<string, string>>();

        void RenderMarkdown()
        {
            expandedAbbreviations.Clear();
            stack = new StackLayout()
            {
                StyleClass = ["MarkdownWrapper"]
            };

            if (!string.IsNullOrEmpty(Markdown))
            {
                var pipeline = new MarkdownPipelineBuilder()
                    .UseAbbreviations();

                if (Settings.UseAutolinksExtension)
                {
                    pipeline = pipeline.UseAutoLinks();
                }

                if (Settings.UseEmphasisExtrasExtension)
                {
                    pipeline = pipeline.UseEmphasisExtras();
                }

                if (Settings.UseTablesExtension)
                {
                    pipeline = pipeline.UseGridTables();
                    pipeline = pipeline.UsePipeTables();
                }

                if (Settings.UseTaskListsExtension)
                {
                    pipeline = pipeline.UseTaskLists();
                }

                if (Settings.UseEmojiAndSmileyExtension)
                {
                    pipeline = pipeline.UseEmojiAndSmiley();
                }

                var parsed = Markdig.Markdown.Parse(Markdown, pipeline.Build());
                Render(parsed.AsEnumerable());
            }

            Content = stack;
        }

        void Render(IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
            {
                Render(block);
            }
        }

        void AttachLinks(View view)
        {
            if (!links.Any())
            {
                return;
            }

            var blockLinks = links.Select(o => new LinkData { Text = o.Key, Link = o.Value }).ToList();
            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        if (Settings.CustomTapHandler != null)
                        {
                            Settings.CustomTapHandler.Invoke(blockLinks);
                        }
                        else
                        {
                            LinkData link = null;
                            if (blockLinks.Count > 1)
                            {
                                var result = await Application.Current.MainPage.DisplayActionSheet(
                                    Settings.OpenLinkSheetTitle,
                                    Settings.OpenLinkSheetCancel,
                                    null,
                                    blockLinks.Select(x => x.Text).ToArray());
                                link = blockLinks.FirstOrDefault(x => x.Text == result);
                            }
                            else
                            {
                                link = blockLinks.FirstOrDefault();
                            }

                            if (link != null)
                                NavigateToLink(link.Link);
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }),
            });

            links = new List<KeyValuePair<string, string>>();
        }

        #region Rendering blocks

        void Render(Block block)
        {
            switch (block)
            {
                case HeadingBlock heading:
                    Render(heading);
                    break;

                case ParagraphBlock paragraph:
                    Render(paragraph);
                    break;

                case QuoteBlock quote:
                    Render(quote);
                    break;

                case CodeBlock code:
                    Render(code);
                    break;

                case ListBlock list:
                    Render(list);
                    break;

                case ThematicBreakBlock thematicBreak:
                    Render(thematicBreak);
                    break;

                case HtmlBlock html:
                    Render(html);
                    break;

                case Table table:
                    Render(table);
                    break;

                default:
                    Debug.WriteLine($"Can't render {block.GetType()} blocks.");
                    break;
            }

            if (!queuedViews.Any())
            {
                return;
            }

            queuedViews.ForEach(v => stack.Children.Add(v));
            queuedViews.Clear();
        }

        void Render(ThematicBreakBlock block)
        {
            stack.Children.Add(new BoxView
            {
                StyleClass = ["MarkdownThematicBreak"]
            });
        }

        int listScope;

        void Render(ListBlock block)
        {
            var listType = block.IsOrdered ? ListStyleType.Decimal : Settings.ListStyle;

            var initialStack = stack;

            stack = new StackLayout()
            {
                StyleClass = ["MarkdownList"],
            };

            listScope++;

            var itemsCount = block.Count();
            for (var i = 0; i < itemsCount; i++)
            {
                var item = block.ElementAt(i);

                if (item is ListItemBlock itemBlock)
                {
                    Render(block, listType, i + 1, itemBlock);
                }
            }

            listScope--;

            initialStack.Children.Add(stack);

            stack = initialStack;

        }

        void Render(ListBlock parent, ListStyleType listType, int index, ListItemBlock block)
        {
            var initialStack = stack;

            stack = new StackLayout()
            {
                StyleClass = [ "MarkdownList" ],
            };

            Render(block.AsEnumerable());
            Grid.SetColumn(stack, 1);

            var horizontalStack = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection {
                    new ColumnDefinition() { Width = GridLength.Auto },
                    new ColumnDefinition() { Width = GridLength.Star },
                },
                ColumnSpacing = 0, //listTheme.Spacing ?? Theme.Margin,
                RowSpacing = 0,
                Margin = 0
                // TODO : Stylize
            };

            if (listType == ListStyleType.None)
            {
                horizontalStack.ColumnSpacing = 0;
            }

            var bullet = GetListBullet(listType, index, parent, block);

            if (bullet != null)
            {
                Grid.SetColumn(bullet, 0);
                horizontalStack.Children.Add(bullet);
            }

            horizontalStack.Children.Add(stack);
            initialStack.Children.Add(horizontalStack);

            stack = initialStack;
        }

        View GetListBullet(ListStyleType listType, int index, ListBlock parent, ListItemBlock block)
        {
            TaskList? thisTaskList = null;

            try
            {
                if (((Markdig.Syntax.LeafBlock)block.FirstOrDefault())?.Inline?.FirstChild is TaskList)
                    thisTaskList = ((Markdig.Syntax.LeafBlock)block.FirstOrDefault())?.Inline?.FirstChild as TaskList;
            }
            catch { }

            if (thisTaskList != null)
            {
                return new Label()
                {
                    Text = thisTaskList.Checked ? "\u2612" : "\u2610",
                    Style = (Style)Application.Current.Resources["MarkdownBulletTask"]
                };
            }

            if (listType == ListStyleType.None)
            {
                return null;
            }

            if (listType == ListStyleType.Custom)
            {
                return Settings.ListBulletCustomCallback?.Invoke(index, parent, block);
            }

            if (listType == ListStyleType.Decimal || listType == ListStyleType.Symbol)
            {
                return new Label
                {
                    Text = listType == ListStyleType.Symbol ? Settings.ListSymbol : $"{index}.",
                    Style = (Style)Application.Current.Resources["MarkdownBulletOrdered"]
                };
            }
            else if (listType == ListStyleType.Square || listType == ListStyleType.Circle)
            {
                var bullet = new Frame
                {
                    Style = listScope % 2 == 0 
                        ? (Style)Application.Current.Resources["MarkdownBulletUnorderedEven"]
                        : (Style)Application.Current.Resources["MarkdownBulletUnorderedOdd"]
                };

                return bullet;
            }

            return null;
        }

        void Render(HeadingBlock block)
        {
            var label = new Label
            {
                FormattedText = CreateFormatted(block.Inline, string.Empty),
                StyleClass = [ "MarkdownHeading" + block.Level.ToString(), isQuoted ? "MarkdownBaseLabelQuoted" : null ],
            };

            AttachLinks(label);

            // TODO : Only render stack if boxview is defined in style?
            var headingStack = new StackLayout();
            headingStack.Children.Add(label);
            headingStack.Children.Add(new BoxView
            {
                StyleClass = ["MarkdownHeading" + block.Level.ToString(), isQuoted ? "MarkdownHeadingBoxViewBaseQuoted" : null ]
            });
            stack.Children.Add(headingStack);
        }

        bool isHeader = false;

        void Render(Table table)
        {
            var initialStack = stack;

            stack = new StackLayout();

            var tableGrid = new Grid
            {
                StyleClass = ["MarkdownTable"],
            };

            for (var i = 0; i < table.ColumnDefinitions.Count; i++)
            {
                tableGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            }

            for (var i = 0; i < table.Count; i++) // extra for header row
            {
                tableGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            var rowCount = 0;
            foreach (var rowObj in table)
            {
                var row = (TableRow)rowObj;

                isHeader = row.IsHeader;

                for (var column = 0; column < row.Count; column++)
                {
                    var cellObj = row[column];
                    var cell = (TableCell)cellObj;

                    stack = new StackLayout();
                    if (isHeader)
                    {
                        var cornerStyle = string.Empty;

                        if (column == 0) cornerStyle = "MarkdownTableHeaderTopLeft";
                        if (column == row.Count - 1) cornerStyle = "MarkdownTableHeaderTopRight";

                        var bv = new BoxView()
                        {
                            StyleClass = [ "MarkdownTableHeader", cornerStyle],
                        };
                        tableGrid.Add(bv, column, rowCount);
                        stack.StyleClass = ["MarkdownTableHeader", cornerStyle];
                    }
                    else
                    {
                        var cornerStyle = string.Empty;
                        if (rowCount == table.Count - 1)
                        {
                            if (column == 0) cornerStyle = "MarkdownTableCellBottomLeft";
                            if (column == row.Count - 1) cornerStyle = "MarkdownTableCellBottomRight";
                        }

                        var bv = new BoxView()
                        {
                            StyleClass = ["MarkdownTableCell", cornerStyle],
                        };
                        tableGrid.Add(bv, column, rowCount);
                        stack.StyleClass = ["MarkdownTableCell", cornerStyle];
                    }
                    Render(cell.AsEnumerable());
                    tableGrid.Add(stack, column, rowCount);
                }

                rowCount++;
            }

            initialStack.Children.Add(tableGrid);
            stack = initialStack;
        }

        void Render(ParagraphBlock block)
        {
            var styleName = isQuoted ? "MarkdownParagraphQuoted" : "MarkdownParagraph";

            if (isHeader)
            {
                styleName = isQuoted ? "MarkdownTableHeaderQuoted" : "MarkdownTableHeader";
            }

            var label = new Label
            {
                FormattedText = CreateFormatted(block.Inline, styleName),
                StyleClass = [styleName],
                // Must set parent Label TextColor for TextDecorations such
                // as strikethrough and underline to display due to bug
                // See https://github.com/dotnet/maui/issues/23488
                // Fix has been done so it won't be an issue forever
                // StyleClass here has default TextColor defined
            };
            AttachLinks(label);
            stack.Children.Add(label);
        }

        void Render(HtmlBlock block)
        {
            // ?
        }

        void Render(QuoteBlock block)
        {
            var initialIsQuoted = isQuoted;
            var initialStack = stack;

            isQuoted = true;
            var grid = new Grid()
            {
                StyleClass = ["MarkdownQuote"],
                ColumnDefinitions = { 
                    new ColumnDefinition(GridLength.Auto), 
                    new ColumnDefinition(GridLength.Star) }
            };

            stack = new StackLayout() { StyleClass = ["MarkdownQuote"] };
            Render(block.AsEnumerable());

            grid.Add(new BoxView() { StyleClass = ["MarkdownQuote"] }, 0);
            grid.Add(stack, 1);

            initialStack.Children.Add(grid);

            isQuoted = initialIsQuoted;
            stack = initialStack;
        }

        void Render(CodeBlock block)
        {
            var language = (block is Markdig.Syntax.FencedCodeBlock && !string.IsNullOrEmpty(((Markdig.Syntax.FencedCodeBlock)block).Info))
                ? "-Language-" + ((Markdig.Syntax.FencedCodeBlock)block).Info.ToLower()
                : string.Empty;

            var label = new Label
            {
                Text = string.Join(Environment.NewLine, block.Lines),
                StyleClass = ["MarkdownCode" + language]
            };
            stack.Children.Add(new Frame()
            {
                Content = label,
                StyleClass = ["MarkdownCode"]
            });
        }

        FormattedString CreateFormatted(ContainerInline inlines, string style)
        {
            var fs = new FormattedString();

            foreach (var inline in inlines)
            {
                var spans = CreateSpans(inline, style);
                foreach (var span in spans)
                {
                    fs.Spans.Add(span);
                }
            }

            return fs;
        }

        Span[] CreateSpans(Inline inline, string style)
        {
            switch (inline)
            {
                case TaskList taskList:
                    // Task list checkboxes are rendered as list bullets in GetListBullet
                    return Array.Empty<Span>();

                case LiteralInline literal:

                    if (literal is Markdig.Extensions.Emoji.EmojiInline)
                    {
                        style = "MarkdownEmoji";
                    }

                    return new[]
                    {
                        new Span
                        {
                            Text = literal.Content.Text.Substring(literal.Content.Start, literal.Content.Length),
                            Style = !string.IsNullOrEmpty(style) ? (Style)Application.Current.Resources[style] : null,
                        }
                    };

                case EmphasisInline emphasis:
                    var childStyle = string.Empty;

                    switch (emphasis.DelimiterChar)
                    {
                        // Bold/italics
                        case '*':
                        case '_':
                            childStyle = emphasis.DelimiterCount == 2 ? "MarkdownBold" : "MarkdownEmphasis";
                            break;

                        // Strikethrough/Subscript
                        case '~':
                            if (emphasis.DelimiterCount == 2)
                            {
                                childStyle = "MarkdownStrikethrough";
                            }
                            else
                            {
                                childStyle = "MarkdownSubscript";
                            }
                            break;

                        // Superscript
                        case '^':
                            childStyle = "MarkdownSuperscript";
                            break;

                        // Marked
                        case '=':
                            if (emphasis.DelimiterCount != 2) break;
                            childStyle = "MarkdownMarked";
                            break;

                        // Inserted
                        case '+':
                            if (emphasis.DelimiterCount != 2) break;
                            childStyle = "MarkdownInserted";
                            break;
                    }
                    return emphasis.SelectMany(x => CreateSpans(x, childStyle)).ToArray();

                case LineBreakInline breakline:
                    return new[] { new Span { Text = "\n" } };

                case LinkInline link:

                    var url = link.Url;

                    if (!Settings.ExternalProtocols.Any(o => url.StartsWith(o)))
                    {
                        url = $"{RelativeUrlHost?.TrimEnd('/')}/{url.TrimStart('/')}";
                    }

                    if (TryLoadYouTubePreview(link.Url, out var youtubePreview))
                    {
                        queuedViews.Add(youtubePreview);
                        return new Span[0];
                    }
                    else if (link.IsImage)
                    {
                        // In MAUI, images expand, so we need a Grid to contain it
                        var grid = new Grid()
                        {
                            RowDefinitions = [new RowDefinition(GridLength.Auto)],
                            ColumnDefinitions = [new ColumnDefinition(GridLength.Auto)],
                            HorizontalOptions = LayoutOptions.Center
                        };

                        var image = new Image
                        {
                            HeightRequest = -1, // Auto height
                            WidthRequest = -1, // Auto width
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            StyleClass = ["MarkdownImage"],
                        };

                        if (link.FirstChild is Markdig.Syntax.Inlines.LiteralInline)
                        {
                            var tooltipText = ((Markdig.Syntax.Inlines.LiteralInline)link.FirstChild).Content.ToString();
                            if (!string.IsNullOrEmpty(tooltipText))
                            {
                                ToolTipProperties.SetText(image, tooltipText);
                            }
                        }

                        if (Path.GetExtension(url) == ".svg")
                        {
                            image.RenderSvg(url);
                        }
                        else
                        {
                            image.Source = url;
                        }

                        grid.Add(image, 0, 0);

                        queuedViews.Add(grid);
                        return new Span[0];
                    }
                    else
                    {
                        var spans = link.SelectMany(x => CreateSpans(x, "MarkdownLink")).ToArray();
                        links.Add(new KeyValuePair<string, string>(string.Join("", spans.Select(x => x.Text)), url));
                        return spans;
                    }

                case AutolinkInline autolink:

                    return RenderAutolink(autolink);

                case CodeInline code:
                    return new[]
                    {
                        new Span()
                        {
                            Text="\u2002",
                            Style = (Style)Application.Current.Resources["MarkdownInlineCode"]
                        },
                        new Span
                        {
                            Text = code.Content,
                            Style = (Style)Application.Current.Resources["MarkdownInlineCode"]
                        },
                        new Span()
                        {
                            Text="\u2002",
                            Style = (Style)Application.Current.Resources["MarkdownInlineCode"]
                        },
                    };
                    
                case AbbreviationInline abbr:

                    var hasBeenExpanded = expandedAbbreviations.Contains(abbr.Abbreviation.Label);

                    var abbrText = hasBeenExpanded
                        ? abbr.Abbreviation.Label
                        : abbr.Abbreviation.Label + " (" + abbr.Abbreviation.Text.ToString() + ")";

                    if (!hasBeenExpanded)
                    {
                        expandedAbbreviations.Add(abbr.Abbreviation.Label);
                    }

                    return new[]
                    {
                        new Span
                        {
                            Text = abbrText,
                            Style = (Style)Application.Current.Resources["MarkdownAbbreviation"]
                        },
                    };

                case ContainerInline container:
                    return container.SelectMany(x => CreateSpans(x, "")).ToArray();
                    //return new[]
                    //{
                    //    new Span
                    //    {
                    //        Text = "abbr",
                    //        Style = (Style)Application.Current.Resources["MarkdownInlineCode"]
                    //    },
                    //};

                default:
                    Debug.WriteLine($"Can't render {inline.GetType()} inlines.");
                    return new Span[0];
            }
        }

        List<string> expandedAbbreviations = new List<string>();

        Span[] RenderAutolink(AutolinkInline autolink)
        {
            var url = autolink.Url;

            if (TryLoadYouTubePreview(url, out var youtubePreview))
            {
                queuedViews.Add(youtubePreview);
                return new Span[0];
            }

            if (autolink.IsEmail && !url.ToLower().StartsWith("mailto:"))
            {
                url = $"mailto:{url}";
            }

            links.Add(new KeyValuePair<string, string>(autolink.Url, url));

            return new[] {
                new Span
                {
                    Text = autolink.Url,
                    Style = (Style)Application.Current.Resources["MarkdownLink"]
                }
            };
        }

        bool TryLoadYouTubePreview(string url, out View imageView)
        {
            imageView = null;

            if (!Settings.LoadYoutubePreviews)
            {
                return false;
            }

            var reg = new List<Regex>
            {
                new Regex(youTubeWatchPattern),
                new Regex(youTubePattern),
                new Regex(youTubeCookiePattern),
            };

            var match = reg.Select(o => o.Match(url)).FirstOrDefault(o => o.Success);

            if (match == null)
            {
                return false;
            }

            var code = match.Groups[1].Value;

            var videoPreviewDescriptor = new VideoPreviewDescriptor
            {
                Code = code,
                VideoUrl = url,
            };

            ImageSource imageSource;

            if (Settings.YouTubePreview?.CustomLoadImage != null)
            {
                imageSource = Settings.YouTubePreview.CustomLoadImage(videoPreviewDescriptor);
            }
            else
            {
                imageSource = DownloadImage(Settings.YouTubePreview?.GenerateLoadImageUrl?.Invoke(videoPreviewDescriptor) 
                    ?? $"https://img.youtube.com/vi/{code}/hqdefault.jpg");
            }

            if (imageSource == null)
            {
                return false;
            }

            // In MAUI, images expand, so we need a Grid to contain it
            imageView = new Grid()
            {
                RowDefinitions = [new RowDefinition(GridLength.Auto)],
                ColumnDefinitions = [new ColumnDefinition(GridLength.Auto)],
                HorizontalOptions = LayoutOptions.Center
            };

            var image = new Image
            {
                Source = imageSource,
                HeightRequest = -1, // Auto height
                WidthRequest = -1, // Auto width
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                StyleClass = [ "MarkdownImage" ],
            };

            ((Grid)imageView).Add(image, 0, 0);

            imageView.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (Settings.CustomTapHandler != null)
                    {
                        Settings.CustomTapHandler.Invoke(new List<LinkData> { new LinkData { Link = url, Text = url } });
                    }
                    else
                    {
                        NavigateToLink(url);
                    }

                })
            });

            if (Settings.YouTubePreview?.TransformView != null)
            {
                imageView = Settings.YouTubePreview.TransformView(image as Image, videoPreviewDescriptor);
            }

            return true;
        }

        ImageSource DownloadImage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            try
            {
                var byteArray = webClient.DownloadData(url);

                if (byteArray == null || !byteArray.Any())
                {
                    return null;
                }

                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        #endregion
    }
}