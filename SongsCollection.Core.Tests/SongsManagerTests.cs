using Moq;
using SongsCollection.Core.Models;

namespace SongsCollection.Core.Tests;

public class SongsManagerTests
{
    private SongsManager songsManager { get; set; }

    [SetUp]
    public void Setup()
    {
        Mock<ISongsDAO> songsDAO = new Mock<ISongsDAO>();
        songsDAO.Setup(d => d.GetSongById(0)).Returns(() => null);
        songsDAO.Setup(d => d.GetSongById(1)).Returns(() => new Song(1, "Title", "Artist"));
        songsDAO.Setup(d => d.GetAll()).Returns(new List<Song> { new(1, "Title1", "Artist1"), new(2, "Title2", "Artist2") });
        this.songsManager = new SongsManager(songsDAO.Object);
    }

    [Test]
    public void AddSong_NonZeroId_Throws()
    {
        var songToAdd = new Song(5, string.Empty, string.Empty);
        Assert.Throws<InvalidOperationException>(() => songsManager.AddSong(songToAdd));
    }

    [Test]
    public void AddSong_ZeroId_Success()
    {
        var songToAdd = new Song(0, string.Empty, string.Empty);
        songsManager.AddSong(songToAdd);
    }

    [Test]
    public void GetSongById_InvalidId_ReturnsNull()
    {
        var song = songsManager.GetSongById(0);
        Assert.IsNull(song);
    }

    [Test]
    public void GetSongById_ExistingId_ReturnsSong()
    {
        var id = 1;
        var song = songsManager.GetSongById(id);
        Assert.IsNotNull(song);
        Assert.AreEqual(id, song.Id);
    }

    [Test]
    public void GetAllSongs_ReturnsSongs()
    {
        var songs = songsManager.GetAllSongs();
        Assert.That(songs.Count(), Is.EqualTo(2));
    }
}