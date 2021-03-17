# Changelog

## Unreleased

## 1.3.0 - 2021-03-17

### Added
- Support for ASCII emojis. (https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/EmojiSpecs.md)
  - This feature is disabled by default. Set `UseEmojiAndSmileyExtension = true` for enabling.
- YouTube preview (added in 1.2.0) parses url from youtube-nocookie.com

## 1.2.0 - 2021-03-14

### Added 
- Support for converting YouTube links to video preview
  - This feature is disabled by defualt. Set `LinkStyle.LoadYouTubePreview = true` for enabling.
  - There are also callbacks `LinkStyle.YouTubePreview.CustomLoadImage` for downloading image and `LinkStyle.YouTubePreview.TransformView` for rendering it
- Support parsing link in plaintext. (https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/AutoLinks.md)
  - This feature is disabled by default. Set `LinkStyle.UseAutolinksExtension = true` for enabling. 

## 1.1.0 - 2021-02-25

### Added 
- Support for autolinks (https://spec.commonmark.org/0.29/#autolinks)
  - Autolinks are rendered the same way as regular links.

## 1.1.0-pre6 - 2021-02-24

### Added 
- `HorizontalTextAlignment` and `VerticalTextAlignment` for Heading, Paragraph and Code
- Added `ExternalProtocols` to `LinkStyle` which defines link protocols which can be opened by default tap handler (default are: http://, https://, mailto:, tel:)

### Fixed
- `CreateSpans` returns `Span[0]` instead of `null` for unknown type, which solves `NullReferenceException`

### Changed
- `LinkStyle.CustomCallback` renamed to `LinkStyle.CustomTapHandler`

## 1.1.0-pre5 - 2021-02-18

### Added
- LinkStyle
  - Changeable texts in action sheet in default tap handler
  - Custom tap handler for links

## 1.1.0-pre4 - 2021-02-17

### Added
- Vertical spacing of items
- Vertical spacing of list items

### Changed
- Rewritten list margins

## 1.1.0-pre3 - 2021-02-16

### Added
- List margin (top/bottom/left/right margin of whole list)

## 1.1.0-pre2 - 2020-10-18

### Added
- List bullets
  - Various bullet types (including custom view)
  - Bullet size, color, font attributes
  - Spacing (space between bullet and content of list item)
  - Align of bullets

### Changed
- Refactored rendering of lists

## 1.1.0-pre1 - 2020-10-14

### Added
- List indentation (indentation of each level of list)

## 1.1.0 - 2020-10-11

### Added
- Fork of original package Xam.Forms.MarkdownView
- Consolidation of dependencies
- Support for `LineHeight`
