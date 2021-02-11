using System;
using Ono.MVP.View;
using UniRx;
using Zenject;

/// <summary>
/// Presenter
/// </summary>
public class MusicPlayerPresenter : IInitializable, IDisposable
{
    private readonly MusicPlayerView _musicPlayerView;
    private readonly MusicPlayerModel _musicPlayerModel;
    private CompositeDisposable _disposables;

    /// <summary>
    /// コンストラクタインジェクション
    /// </summary>
    /// <param name="musicPlayerView">View</param>
    /// <param name="musicPlayerModel">Model</param>
    public MusicPlayerPresenter(MusicPlayerView musicPlayerView, MusicPlayerModel musicPlayerModel)
    {
        _musicPlayerView = musicPlayerView;
        _musicPlayerModel = musicPlayerModel;
    }

    /// <summary>
    /// 初期化時のライフサイクルイベントとして呼ばれるメソッド
    /// </summary>
    public void Initialize()
    {
        _disposables = new CompositeDisposable();

        //View → Modelへの反映
        _musicPlayerView.PlayButton.OnClickAsObservable()
            .DistinctUntilChanged()
            .Subscribe(_ => { _musicPlayerModel.PlayMusic(); }).AddTo(_disposables);

        //View → Modelへの反映
        _musicPlayerView.StopButton.OnClickAsObservable()
            .DistinctUntilChanged()
            .Subscribe(_ => { _musicPlayerModel.StopMusic(); }).AddTo(_disposables);

        //View → Modelへの反映
        _musicPlayerView.SeekBar.OnValueChangedAsObservable()
            .DistinctUntilChanged()
            .Subscribe(value => { _musicPlayerModel.ChangePlayTime(value); }).AddTo(_disposables);

        //Model → Viewへの反映
        _musicPlayerModel.MusicPlayTimeRP
            .Subscribe(time => { _musicPlayerView.SeekBar.value = time; }).AddTo(_disposables);

        //Model → Viewへの反映
        _musicPlayerModel.MusicPlayModeRP
            .Subscribe(mode => { _musicPlayerView.SwitchButton(mode); }).AddTo(_disposables);
    }

    /// <summary>
    /// 破棄する処理
    /// </summary>
    public void Dispose()
    {
        _disposables.Dispose();
    }
}