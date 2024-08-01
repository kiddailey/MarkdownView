namespace Markdown.Samples.Utils
{
    public class LayoutOptionsWrapper
    {
        public LayoutOptions LayoutOptions { get; }

        public LayoutOptionsWrapper(LayoutOptions layoutOptions)
        {
            LayoutOptions = layoutOptions;
        }
        public override string ToString()
        {
            var name = LayoutOptions.Alignment.ToString();

            if (LayoutOptions.Expands)
            {
                name += "AndExpand";
            }

            return name;
        }

        public override bool Equals(object obj) => LayoutOptions.Equals((obj as LayoutOptionsWrapper).LayoutOptions);
    }
}
