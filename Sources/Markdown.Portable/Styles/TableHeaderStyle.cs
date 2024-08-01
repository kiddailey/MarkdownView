using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiMarkdown.Styles.TableStyle;

namespace MauiMarkdown.Styles
{
    public class TableHeaderStyle : TableCellStyle
    {
        public FontAttributes FontAttributes { get; set; } = FontAttributes.Bold;
        public int FontSize { get; set; } = 12;

        public TableHeaderStyle()
        {
            BackgroundColor = Color.FromArgb("#30000000");
        }
    }
}
