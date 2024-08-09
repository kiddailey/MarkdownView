using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Markdown.Samples.Utils;
using MauiMarkdown;

namespace Markdown.Samples.ViewModels
{
    public class SamplePageViewModel : BaseViewModel
    {
        public const string embeded = @"
# An exhibit of Markdown

![Unsplash](https://unsplash.it/200/300)This note demonstrates some of what [Markdown][1] is capable of doing.

*Note: Feel free to play with this page. Unlike regular notes, this doesn't automatically save itself.*

## Basic formatting

Paragraphs can be written like so. A paragraph is the basic block of Markdown. A paragraph is what text will turn into when there is no reason it should become anything else.

Paragraphs must be separated by a blank line. Basic formatting of *italics* and **bold** is supported. This *can be **nested** like* so.

Enable the additional empahsis feature to also utilize ~~strikethrough~~, ^superscript^, ~subscript~, ==marked== and ++inserted++ formatting

## Lists

### Ordered list

1. Item 1
   1. A second item
   1. A third item
1. Number 3
   1. An item
1. Ⅳ

*Note: the fourth item uses the Unicode character for [Roman numeral four][2].*

### Unordered list

* An item
  * Another item
    * Yet another item
* And there's more...

### Task List

* [ ] To Do
* [ ] To Do
  * [X] To Do
  * [ ] To Do
* [ ] To Do

*Note: You must enable Task List support for this to render

## Paragraph modifiers

### Code block

```
Code blocks are very useful for developers and other people who look at code or other things that are written in plain text. As you can see, it uses a fixed-width font.
```

    Code blocks can be created using fencing (back ticks) or indenting.

You can also make `inline code` to add code into other things.

### Quote

> Here is a quote. What this is should be self explanatory. Quotes are automatically indented when they are used.

## Headings

There are six levels of headings. They correspond with the six levels of HTML headings. You've probably noticed them already in the page. Each level down uses one more hash character.

### Headings *can* also contain **formatting**

### They can even contain `inline code`

Of course, demonstrating what headings look like messes up the structure of the page.

I don't recommend using more than three or four levels of headings here, because, when you're smallest heading isn't too small, and you're largest heading isn't too big, and you want each size up to look noticeably larger and more important, there there are only so many sizes that you can use.

## Links

Links can be made in a handful of ways:

* Named, Reference: [MarkItDown][3]
* Raw Reference: [3]
* Named, Inline: [MarkItDown](http://www.markitdown.net/)
* Autolink (Angle Brackets):
  <http://foo.bar.baz>
  <http://foo.bar.baz/test?q=hello&id=22&boolean>
* Email links are supported, with or without mailto prefix: <mailto:foo@bar.baz> or <foo@bar.baz>
* When the AutoLinking extension is enabled, regular text links will also be converted to active links: http://foo.bar.baz

### Youtube Links

If enabled, Youtube thumbnail images can be downloaded downloaded and displayed for the link. 

* Named, Inline: [YouTube](https://www.youtube.com/watch?v=qgIYv8fG7Qk)
* AutoLink (Angle Brackets): <https://www.youtube.com/watch?v=_N-IREy7C9s&t=7936s>
* AutoLink (Text Extension): https://www.youtube.com/watch?v=91E_lYSUmg8&t=929s
* AutoLink (Text Extension): https://www.youtube-nocookie.com/embed/21UW2-u1KNo

## Horizontal rule

A horizontal rule is a line that goes across the middle of the page.

---

It's sometimes handy for breaking things up.

## Images

Markdown can also contain images. I'll need to add something here sometime.

## Tables

### Pipe Tables

 Column 1 | Column 2 | Column 3
----------|----------|----------
 Item 1   | X        | 
 Item 2   | X        | X
 Item 3   |          | X

### Grid Tables

+----------+----------+----------+
| Column 1 | Column 2 | Column 3 |
+==========|==========|==========+
| Item 1   | X        |          |
+----------|----------|----------+
| Item 2   | X        | X        |
|          |          | X        |
+----------|----------|----------+

## Abbrevations

*[EOAEA]: Example Of An Extended Abbreviation

When an Markdown abbreviation is defined, the first instance of that abbreviation is displayed with the expanded text in parens. For example, EOAEA is expanded for the first instance but no other instances of EOAEA.  The definition of the abbreviation is removed from the view.  Tooltips are not supported as MAUI does not currently support setting tooltips on Label Span elements.


## Emojis 

You can use ASCI emojis like :) or :heart:. This feature is disabled by default, set UseEmojiAndSmileyExtension=true for enabling it.

## Finally

There's actually a lot more to Markdown than this. See the official [introduction][4] and [syntax][5] for more information. However, be aware that this is not using the official implementation, and this might work subtly differently in some of the little things.

  [1]: http://daringfireball.net/projects/markdown/
  [2]: http://www.fileformat.info/info/unicode/char/2163/index.htm
  [3]: http://www.markitdown.net/
  [4]: http://daringfireball.net/projects/markdown/basics
  [5]: http://daringfireball.net/projects/markdown/syntax
";

        public MarkdownSettings Settings => GetSettings();
        public string Markdown { get; set; }

        bool isEditing;
        bool isRendering;

        public bool IsEditing
        {
            get
            {
                return isEditing;
            }
            set
            {
                isEditing = value;
                IsRendering = !value;
            }
        }

        public bool IsRendering
        {
            get
            {
                return isRendering;
            }
            set
            {
                isRendering = value;
                isEditing = !value;
            }
        }
        public ICommand ResetMarkdownCommand { get; set; }
        public ICommand UpdateMarkdownCommand { get; set; }
        public ICommand RevertMarkdownCommand { get; set; }
        public ICommand EditMarkdownCommand { get; set; }

        MarkdownSettings settingsTemplate;
        public bool IsDarkTheme { get; set; }
        string oldMarkdown;

        public SamplePageViewModel()
        {
            settingsTemplate = new MarkdownSettings();

            if (Application.Current != null)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }

            Markdown = embeded;
            oldMarkdown = embeded;

            IsRendering = true;

            ResetMarkdownCommand = new Command(() => { Markdown = embeded; IsEditing = false; });
            UpdateMarkdownCommand = new Command(() => { oldMarkdown = Markdown; IsEditing = false; });
            RevertMarkdownCommand = new Command(() => { Markdown = oldMarkdown; IsEditing = false; });
            EditMarkdownCommand = new Command(() => { IsEditing = true; });

            WeakReferenceMessenger.Default.Register<MarkdownSettings>(this, (o, m) =>
            {
                if (m.UseTablesExtension != Settings.UseTablesExtension)
                {
                    settingsTemplate.UseTablesExtension = m.UseTablesExtension;
                }
                if (m.UseEmphasisExtrasExtension != Settings.UseEmphasisExtrasExtension)
                {
                    settingsTemplate.UseEmphasisExtrasExtension = m.UseEmphasisExtrasExtension;
                }
                if (m.UseTaskListsExtension != Settings.UseTaskListsExtension)
                {
                    settingsTemplate.UseTaskListsExtension = m.UseTaskListsExtension;
                }
                if (m.UseAutolinksExtension != Settings.UseAutolinksExtension)
                {
                    settingsTemplate.UseAutolinksExtension = m.UseAutolinksExtension;
                }
                if (m.UseEmojiAndSmileyExtension != Settings.UseEmojiAndSmileyExtension)
                {
                    settingsTemplate.UseEmojiAndSmileyExtension = m.UseEmojiAndSmileyExtension;
                }
                if (m.LoadYoutubePreviews != Settings.LoadYoutubePreviews)
                {
                    settingsTemplate.LoadYoutubePreviews = m.LoadYoutubePreviews;
                }
                if (m.OpenLinkSheetCancel != Settings.OpenLinkSheetCancel)
                {
                    settingsTemplate.OpenLinkSheetCancel = m.OpenLinkSheetCancel;
                }
                if (m.OpenLinkSheetTitle != Settings.OpenLinkSheetTitle)
                {
                    settingsTemplate.OpenLinkSheetTitle = m.OpenLinkSheetTitle;
                }
                RaisePropertyChanged(nameof(SamplePageViewModel.Settings));
            });

            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                IsDarkTheme = Application.Current.UserAppTheme == AppTheme.Dark;
            };
        }

        MarkdownSettings GetSettings()
        {
            var settings = new MarkdownSettings(settingsTemplate);

            return settings;
        }
    }
}