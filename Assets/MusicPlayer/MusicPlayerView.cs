using Ono.MVP.CustomRP;
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
        [SerializeField] private Text _playTimeText;

        /// <summary>
        /// 再生ボタン
        /// </summary>
        public Button PlayButton => _playButton;

        /// <summary>
        /// 停止ボタン
        /// </summary>
        public Button StopButton => _stopButton;

        /// <summary>
        /// シークバー
        /// </summary>
        public Slider SeekBar => _seekBar;
        
        /// <summary>
        /// 再生時間のテキスト
        /// </summary>
        public Text PlayTimeText => _playTimeText;

        /// <summary>
        /// 再生時間をセット
        /// </summary>
        /// <param name="timeText">表示される時間</param>
        public void SetPlayTime(string timeText)
        {
            _playTimeText.text = timeText;
        }

        /// <summary>
        /// ボタン切り替え
        /// </summary>
        /// <param name="musicPlayMode">音楽再生モード</param>
        public void SwitchButton(MusicPlayMode musicPlayMode)
        {
            switch (musicPlayMode)
            {
                case MusicPlayMode.Play:
                    _playButton.gameObject.SetActive(false);
                    _stopButton.gameObject.SetActive(true);
                    break;
                case MusicPlayMode.Stop:
                    _playButton.gameObject.SetActive(true);
                    _stopButton.gameObject.SetActive(false);
                    break;
            }
        }
    }
}