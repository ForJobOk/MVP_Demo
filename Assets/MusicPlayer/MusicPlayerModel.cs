using System.Collections;
using System.Collections.Generic;
using Ono.MVP.CustomRP;
using UniRx;
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
        _bgm.Stop();
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
