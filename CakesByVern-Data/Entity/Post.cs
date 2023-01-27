namespace CakesByVern_Data.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
