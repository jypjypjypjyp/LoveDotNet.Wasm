namespace LoveDotNet.Models
{
    public enum ParagraphType
    {
        Text,Image
    }
    public class Paragraph
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public ParagraphType Type { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}