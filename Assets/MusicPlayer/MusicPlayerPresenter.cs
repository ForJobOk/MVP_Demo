using System;
using Ono.MVP.View;
using UniRx;
using UniRx.Triggers;
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
    /// MonoBehaviorで言うところのStartに当たるライフサイクルイベントとして呼ばれるメソッド
    /// </summary>
    public void Initialize()
    {
        _disposables = new CompositeDisposable();

        //==========================
        // View → Modelへの反映
        //==========================
        
        //再生ボタン押下
        _musicPlayerView.PlayButton.OnClickAsObservable()
            .Subscribe(_ => { _musicPlayerModel.PlayMusic(); }).AddTo(_disposables);

        //停止ボタン押下
        _musicPlayerView.StopButton.OnClickAsObservable()
            .Subscribe(_ => { _musicPlayerModel.StopMusic(); }).AddTo(_disposables);

        //シークバーのドラッグ開始
        _musicPlayerView.SeekBar.OnDragAsObservable()
            .Subscribe(_ => { _musicPlayerModel.StopMusic(); }).AddTo(_disposables);

        //シークバーのドラッグ終了
        _musicPlayerView.SeekBar.OnDropAsObservable()
            .Subscribe(_ => { _musicPlayerModel.ChangePlayTime(_musicPlayerView.SeekBar.value); }).AddTo(_disposables);

        //==========================
        // Model → Viewへの反映
        //==========================
        
        //再生時間をシークバーに反映
        _musicPlayerModel.MusicPlayTimeRP
            .Subscribe(time =>
            {
                _musicPlayerView.SeekBar.value = time;
                var (item1, item2) = _musicPlayerModel.GetMusicTime();
                _musicPlayerView.SetPlayTime($"{item1}/{item2}");
            }).AddTo(_disposables);

        //再生モードに応じてボタンの表示を切り替え
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