using SongsCollection.Core.Models;

namespace SongsCollection.Core;

public class SongsManager
{
    private readonly ISongsDAO songsDAO;

    public SongsManager(ISongsDAO songsDAO)
    {
        this.songsDAO = songsDAO;
    }

    public Song AddSong(Song song)
    {
        if (song.Id != 0)
            throw new InvalidOperationException();
        return this.songsDAO.AddSong(song);
    }

    public Song? GetSongById(int id) => this.songsDAO.GetSongById(id);

    public void UpdateSong(Song song) => this.songsDAO.UpdateSong(song);

    public bool DeleteSong(Song song) => this.songsDAO.DeleteSong(song);

    public IEnumerable<Song> GetAllSongs() => this.songsDAO.GetAll();
}
