using Ono.MVP.CustomRP;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ono.MVP.View
{
    /// <summary>
    /// View
    /// </summary>
    public class MusicPlayerView : MonoBehaviour
    {
        [SerializeField] private Button _playButton, _stopButton;
        [SerializeField] private Slider _seekBar;

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
            //プレイボタン押下
            _playButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _playButton.gameObject.SetActive(false);
                    _stopButton.gameObject.SetActive(true);
                    //値の更新(発火)
                    _musicPlayModeRP.Value = MusicPlayMode.Play;
                }).AddTo(this);
        
            //停止ボタン押下
            _stopButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _playButton.gameObject.SetActive(true);
                    _stopButton.gameObject.SetActive(false);
                    //値の更新(発火)
                    _musicPlayModeRP.Value = MusicPlayMode.Stop;
                }).AddTo(this);

            //シークバー操作
            _seekBar.OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    //値の更新(発火)
                    _musicPlayTimeRP.Value = value;
                }).AddTo(this);
        }
    }
}