namespace SongsCollection.Core.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Artist { get; set; }

    public Song(int id, string title, string? artist)
    {
        this.Id = id;
        this.Title = title;
        this.Artist = artist;
    }

    public override string ToString() => string.IsNullOrWhiteSpace(Artist) ? Title : $"{Artist} - {Title}";
}
