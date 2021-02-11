using Ono.MVP.View;
using UnityEngine;
using Zenject;

/// <summary>
/// Installer
/// </summary>
public class MusicPresenterInstaller : MonoInstaller
{
    [SerializeField] private GameObject _view, _model;
    
    public override void InstallBindings()
    {
        Container.Bind<MusicPlayerView>().FromComponentOn(_view).AsSingle();
        Container.Bind<MusicPlayerModel>().FromComponentOn(_model).AsSingle();
        Container.Bind(typeof(MusicPlayerPresenter),typeof(IInitializable))
            .To<MusicPlayerPresenter>().AsSingle().NonLazy();
    }
}