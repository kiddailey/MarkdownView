using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMarkdown.Styles
{
    public class TableCellStyle
    {
        public Color BackgroundColor { get; set; } = Color.FromArgb("#10000000");
        public Thickness Padding { get; set; } = new Thickness(10);
        public LayoutOptions HorizontalTextOptions { get; set; } = LayoutOptions.Center;
        public LayoutOptions VerticalTextOptions { get; set; } = LayoutOptions.Center;
    }
}
