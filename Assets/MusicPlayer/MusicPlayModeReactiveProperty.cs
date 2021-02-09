using System;
using UniRx;

namespace Ono.MVP.CustomRP
{
    /// <summary>
    /// 音楽再生に関する状態
    /// </summary>
    public enum MusicPlayMode
    {
        Play,
        Stop
    }
    
    [Serializable]
    public class MusicPlayModeReactiveProperty : ReactiveProperty<MusicPlayMode>
    {
        public MusicPlayModeReactiveProperty (){}
        public MusicPlayModeReactiveProperty (MusicPlayMode initialValue) : base (initialValue) {}
    }
}
