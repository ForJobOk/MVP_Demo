using Ono.MVP.CustomRP;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

/// <summary>
/// Model
/// </summary>
public class MusicPlayerModel : MonoBehaviour
{
    [SerializeField] private AudioSource _bgm;
    
    /// <summary>
    /// 音楽再生モード
    /// </summary>
    public IReadOnlyReactiveProperty<MusicPlayMode> MusicPlayModeRP => _musicPlayModeRP;
    private readonly MusicPlayModeReactiveProperty _musicPlayModeRP = new MusicPlayModeReactiveProperty(MusicPlayMode.Stop);
    
    /// <summary>
    /// 再生時間
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
    /// 再生時間を時分として取得
    /// </summary>
    /// <returns>再生時間、総再生時間</returns>
    public (string, string) GetMusicTime()
    {
        var totalMinute = (int)_bgm.clip.length / 60;
        var totalSecond = (int)_bgm.clip.length % 60;
        var currentMinute = (int)_bgm.time / 60;
        var currentSecond = (int)_bgm.time % 60;
        return ($"{currentMinute}:{currentSecond:00}", $"{totalMinute}:{totalSecond}");
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
    /// <param name="playTimeNormalizedValue">再生箇所の正規化された時間</param>
    public void ChangePlayTime(float playTimeNormalizedValue)
    {
        _bgm.time = playTimeNormalizedValue * _bgm.clip.length;
        PlayMusic();
    }
}
