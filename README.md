# Markdown *for Xamarin.Forms* 

[![NuGet](https://img.shields.io/nuget/v/Xam.Forms.Markdown.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Forms.Markdown/) [![Donate](https://img.shields.io/badge/donate-Buy%20Me%20a%20Coffe-%235F7FFF)](https://www.buymeacoffee.com/bares43)<br />
You can also support author of original package here [![Donate](https://img.shields.io/badge/donate-paypal-yellow.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=ZJZKXPPGBKKAY&lc=US&item_name=GitHub&item_number=0000001&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHosted)

A native Xamarin.Forms Markdown renderer.

Fork of original repository https://github.com/dotnet-ad/MarkdownView with some improvements.

## Gallery

![Light theme](Documentation/Screenshot.png)

## Introduction

Compared to a majority of solutions, MarkdownView will render every component as **a native Xamarin.Forms view instead of via an HTML backend.** The Markdown is directly translated from a syntax tree to a hierarchy of Xamarin.Forms views, : no HTML is being produced at all (hurray)!

This will produce a more reactive user interface, at the cost of rendering functionalities *(at the moment though!)*.

## Install

Available on [NuGet](https://www.nuget.org/packages/Xam.Forms.Markdown/).

## Quickstart

See [documentation](https://github.com/bares43/MarkdownView/wiki#basic-usage).

## Thanks

* [dotnet-ad/MarkdownView](https://github.com/dotnet-ad/MarkdownView) : original package
* [lunet-io/markdig](https://github.com/lunet-io/markdig) : used for Markdown parsing
* [mono/SkiaSharp](https://github.com/mono/SkiaSharp) : used for SVG rendering

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## License

MIT © [bares43](https://janbares.cz/en/), [Aloïs Deniel](http://aloisdeniel.github.io)
