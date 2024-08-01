using Markdig.Syntax;

namespace MauiMarkdown.Styles
{
    public class TableStyle
    {
        public Thickness Margin { get; set; } = new Thickness(0);
        public double ColumnSpacing { get; set; } = 1;
        public double RowSpacing { get; set; } = 1;

        public int FontSize { get; set; } = 12;

        public CornerRadius CornerRadius { get; set; } = new CornerRadius(0);

        public TableHeaderStyle Header { get; set; } = new TableHeaderStyle();
        public TableCellStyle Cell { get; set; } = new TableCellStyle();
    }
}
