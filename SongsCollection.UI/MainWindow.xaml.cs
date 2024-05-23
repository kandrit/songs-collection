using System.Windows;
using SongsCollection.UI.ViewModels;

namespace SongsCollection.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(SongsEditorViewModel songsViewModel)
    {
        DataContext = songsViewModel;
        InitializeComponent();
    }
}