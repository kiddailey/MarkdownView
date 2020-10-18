using System;
using Markdig.Syntax;
using Xamarin.Forms;

namespace Xam.Forms.Markdown
{
    public class ListStyle
    {
        public float Indentation { get; set; } = 10;
        public float? Spacing { get; set; }
        public ListStyleType BulletStyleType { get; set; }
        public int BulletSize { get; set; } = 4;
        public float? BulletFontSize { get; set; }
        public float? BulletLineHeight { get; set; }
        public Color? BulletColor { get; set; }
        public FontAttributes BulletFontAttributes { get; set; } = FontAttributes.None;
        public LayoutOptions BulletVerticalOptions { get; set; }
        public LayoutOptions ItemVerticalOptions { get; set; }
        public string Symbol { get; set; }
        public Func<int, ListBlock, ListItemBlock, View> CustomCallback { get; set; }
    }

    public enum ListStyleType
    {
        Square,
        Circle,
        None,
        Symbol,
        Decimal,
        Custom
    }
}
