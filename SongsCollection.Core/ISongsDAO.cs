using SongsCollection.Core.Models;

namespace SongsCollection.Core;

public interface ISongsDAO
{
    public Song AddSong(Song song);
    public Song? GetSongById(int id);
    public void UpdateSong(Song song);
    public bool DeleteSong(Song song);
    public IEnumerable<Song> GetAll();
}
