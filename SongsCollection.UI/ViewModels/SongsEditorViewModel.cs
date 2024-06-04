using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SongsCollection.Core;
using SongsCollection.Core.Models;
using SongsCollection.UI.Commands;

namespace SongsCollection.UI.ViewModels;

public class SongsEditorViewModel : INotifyPropertyChanged
{
    private readonly SongsManager songsManager;

    public ObservableCollection<SongItemViewModel> SongsCollection { get; set; } = [];

    private string currentTitle = string.Empty;
    public string CurrentTitle
    {
        get => this.currentTitle;
        set
        {
            this.currentTitle = value;
            OnPropertyChanged(nameof(CurrentTitle));
        }
    }

    private string currentArtist = string.Empty;
    public string CurrentArtist
    {
        get => this.currentArtist;
        set
        {
            this.currentArtist = value;
            OnPropertyChanged(nameof(CurrentArtist));
        }
    }

    private SongItemViewModel? selectedSong;
    public SongItemViewModel? SelectedSong
    {
        get => this.selectedSong;
        set
        {
            this.selectedSong = value;
            OnPropertyChanged(nameof(SelectedSong));
        }
    }

    public ICommand SelectedSongChangedCommand => new Command(selectedSong =>
    {
        var song = selectedSong as SongItemViewModel;
        this.CurrentTitle = song?.Title ?? string.Empty;
        this.CurrentArtist = song?.Artist ?? string.Empty;
        this.selectedSong = song;
    }, o => true);

    public ICommand AddNewCommand => new Command(o =>
    {
        var addedSong = this.songsManager.AddSong(new Song(0, this.CurrentTitle, this.CurrentArtist));
        var addedSongViewModel = new SongItemViewModel(addedSong);
        this.SongsCollection.Add(addedSongViewModel);
        this.SelectedSong = addedSongViewModel;
    }, o => !string.IsNullOrEmpty(this.currentTitle));

    public ICommand UpdateCommand => new Command(o =>
    {
        var songToUpdate = SelectedSong ?? throw new InvalidOperationException();
        songToUpdate.Title = this.CurrentTitle;
        songToUpdate.Artist = this.CurrentArtist;
        this.songsManager.UpdateSong(songToUpdate.Song);
    }, o => this.selectedSong != null && !string.IsNullOrEmpty(this.currentTitle));

    public ICommand DeleteCommand => new Command(o =>
    {
        if (this.SelectedSong is SongItemViewModel currentSongViewModel)
        {
            this.songsManager.DeleteSong(currentSongViewModel.Song);
            this.SongsCollection.Remove(currentSongViewModel);
            this.SelectedSong = null;
        }
    }, o => this.selectedSong != null);

    public SongsEditorViewModel(SongsManager songsManager)
    {
        this.songsManager = songsManager;
        var firstSong = songsManager.AddSong(new Song(0, "Sample song", "Sample artist"));
        var firstSongViewModel = new SongItemViewModel(firstSong);
        SongsCollection.Add(firstSongViewModel);
        var secondSong = songsManager.AddSong(new Song(0, "Other song", "Other artist"));
        var secondSongViewModel = new SongItemViewModel(secondSong);
        SongsCollection.Add(secondSongViewModel);
        this.SelectedSong = firstSongViewModel;
        SelectedSongChangedCommand.Execute(firstSong);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        if (this.PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
