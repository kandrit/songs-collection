using SongsCollection.Core.Models;

namespace SongsCollection.Core;

public class SongsMemoryStorage : ISongsDAO
{
    private readonly List<Song> songs = [];
    private int maxId = 0;

    public Song AddSong(Song song)
    {
        lock (songs)
        {
            song.Id = ++maxId;
            songs.Add(CloneSong(song));
            return song;
        }
    }

    public Song? GetSongById(int id)
    {
        var song = default(Song?);
        lock (songs)
        {
            song = songs.FirstOrDefault(s => s.Id == id);
        }
        return song != null ? CloneSong(song) : null;
    }

    public void UpdateSong(Song song)
    {
        lock (songs)
        {
            var songInCollection = songs.First(s => s.Id == song.Id);
            songInCollection.Title = song.Title;
            songInCollection.Artist = song.Artist;
        }
    }

    public bool DeleteSong(Song song)
    {
        lock (songs)
        {
            return songs.RemoveAll(t => t.Id == song.Id) > 0;
        }
    }

    public IEnumerable<Song> GetAll()
    {
        lock (songs)
        {
            return songs.Select(CloneSong);
        }
    }

    private Song CloneSong(Song song) => new(song.Id, song.Title, song.Artist);
}
