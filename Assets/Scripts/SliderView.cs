using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ono.MVP.View
{
    /// <summary>
    /// SliderのViewを担うクラス
    /// </summary>
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _sliderX, _sliderY, _sliderZ;
        [SerializeField] private Text _textX, _textY, _textZ;

        /// <summary>
        /// X軸操作のSlider
        /// 購読機能のみ外部に公開
        /// </summary>
        public IReadOnlyReactiveProperty<float> SliderValueRP_X => _floatReactivePropertyX;
        private readonly FloatReactiveProperty _floatReactivePropertyX = new FloatReactiveProperty();

        /// <summary>
        /// Y軸操作のSlider
        /// 購読機能のみ外部に公開
        /// </summary>
        public IReadOnlyReactiveProperty<float> SliderValueRP_Y => _floatReactivePropertyY;
        private readonly FloatReactiveProperty _floatReactivePropertyY = new FloatReactiveProperty();

        /// <summary>
        /// Z軸操作のSlider
        /// 購読機能のみ外部に公開
        /// </summary>
        public IReadOnlyReactiveProperty<float> SliderValueRP_Z => _floatReactivePropertyZ;
        private readonly FloatReactiveProperty _floatReactivePropertyZ = new FloatReactiveProperty();

        void Start()
        {
            //X軸操作用Sliderの値の変更を監視
            _sliderX.OnValueChangedAsObservable()
                .DistinctUntilChanged()
                .Subscribe(value => { OnValueChange(value, _floatReactivePropertyX, _textX); })
                .AddTo(this);

            //Y軸操作用Sliderの値の変更を監視
            _sliderY.OnValueChangedAsObservable()
                .DistinctUntilChanged()
                .Subscribe(value => { OnValueChange(value, _floatReactivePropertyY, _textY); })
                .AddTo(this);

            //Z軸操作用Sliderの値の変更を監視
            _sliderZ.OnValueChangedAsObservable()
                .DistinctUntilChanged()
                .Subscribe(value => { OnValueChange(value, _floatReactivePropertyZ, _textZ); })
                .AddTo(this);
        }

        /// <summary>
        /// Sliderの値変更時の処理
        /// </summary>
        /// <param name="value">Sliderの値</param>
        /// <param name="floatReactiveProperty">値を更新をしたいRP</param>
        /// <param name="valueText">更新するテキスト</param>
        private void OnValueChange(float value, FloatReactiveProperty floatReactiveProperty, Text valueText)
        {
            //値の整形
            var arrangeValue = Mathf.Floor((value - 0.5f) * 100) / 100 * 360;
            //値の更新
            floatReactiveProperty.Value = arrangeValue;
            //テキストに値を反映
            valueText.text = (arrangeValue).ToString();
        }
    }
}