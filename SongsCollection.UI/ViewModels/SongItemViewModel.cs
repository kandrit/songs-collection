using System.ComponentModel;
using SongsCollection.Core.Models;

namespace SongsCollection.UI.ViewModels;

public class SongItemViewModel : INotifyPropertyChanged
{
    public readonly Song Song;

    public string Title
    {
        get => this.Song.Title;
        set
        {
            this.Song.Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string Artist
    {
        get => this.Song.Artist ?? string.Empty;
        set
        {
            this.Song.Artist = value;
            OnPropertyChanged(nameof(Artist));
        }
    }

    public SongItemViewModel(Song song)
    {
        this.Song = song;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        if (this.PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
