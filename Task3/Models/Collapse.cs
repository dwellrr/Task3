namespace Task3.Models
{
    public class CollapsibleItem
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class CollapsibleViewModel
    {
        public string Title { get; set; }
        public List<CollapsibleItem> Items { get; set; }
    }
}
