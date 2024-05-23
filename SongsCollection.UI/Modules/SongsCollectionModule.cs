using Autofac;
using SongsCollection.Core;
using SongsCollection.UI.ViewModels;

namespace SongsCollection.UI;

public class SongsCollectionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MainWindow>().AsSelf();
        builder.RegisterType<SongsEditorViewModel>().AsSelf();
        builder.RegisterType<SongsManager>().AsSelf();
        builder.RegisterType<SongsMemoryStorage>().As<ISongsDAO>();
    }
}
