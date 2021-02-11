using Ono.MVP.CustomRP;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MusicPlayerModel : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm;
    
    /// <summary>
    /// 音楽再生モードかどうか
    /// 購読機能のみ外部に公開
    /// </summary>
    public IReadOnlyReactiveProperty<MusicPlayMode> MusicPlayModeRP => _musicPlayModeRP;
    private readonly MusicPlayModeReactiveProperty _musicPlayModeRP = new MusicPlayModeReactiveProperty();
    
    /// <summary>
    /// 再生時間
    /// 購読機能のみ外部に公開
    /// </summary>
    public IReadOnlyReactiveProperty<float> MusicPlayTimeRP => _musicPlayTimeRP;
    private readonly FloatReactiveProperty _musicPlayTimeRP = new FloatReactiveProperty();

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => _musicPlayModeRP.Value == MusicPlayMode.Play)
            .Subscribe(_ =>
            {
                _musicPlayTimeRP.Value = _bgm.time / _bgm.clip.length;
            })
            .AddTo(this);
    }

    /// <summary>
    /// 再生し再生モードに切り替え
    /// </summary>
    public void PlayMusic()
    {
        _bgm.Play();
        _musicPlayModeRP.Value = MusicPlayMode.Play;
    }
    
    /// <summary>
    /// 停止し停止モードに切り替え
    /// </summary>
    public void StopMusic()
    {
        _bgm.Pause();
        _musicPlayModeRP.Value = MusicPlayMode.Stop;
    }

    /// <summary>
    /// 再生時間を変更し再生
    /// </summary>
    /// <param name="playTimeValue">再生したい箇所の時間</param>
    public void ChangePlayTime(float playTimeValue)
    {
        _bgm.time = playTimeValue;
        PlayMusic();
    }
}
