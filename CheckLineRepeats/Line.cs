namespace CheckLineRepeats
{
    internal class Line
    {
        public string Text { get; set; }
        public int Count { get; set; }

        public Line()
        {
            Text = string.Empty;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
