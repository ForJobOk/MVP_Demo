using Ono.MVP.Model;
using Ono.MVP.View;
using UniRx;
using UnityEngine;

namespace Ono.MVP.Presenter
{
    /// <summary>
    /// ViewとModelを繋ぐPresenter
    /// </summary>
    public class CubeRotationPresenter : MonoBehaviour
    {
        [SerializeField] private SliderView _sliderView;
        
        void Start()
        {
            var cubeRotationLogic = CubeRotationModel.Instance;

            // ================================
            // Sliderの値の更新を監視
            // ================================
            
            _sliderView.SliderValueRP_X
                .Subscribe(value => { cubeRotationLogic.SetRotationX(value); }).AddTo(this);
            
            _sliderView.SliderValueRP_Y
                .Subscribe(value => { cubeRotationLogic.SetRotationY(value); }).AddTo(this);
            
            _sliderView.SliderValueRP_Z
                .Subscribe(value => { cubeRotationLogic.SetRotationZ(value); }).AddTo(this);
        }
    }
}


