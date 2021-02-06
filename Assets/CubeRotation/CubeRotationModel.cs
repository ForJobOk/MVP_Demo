using UnityEngine;

namespace Ono.MVP.Model
{
    /// <summary>
    /// ビジネスロジックを持つモデルクラス
    /// キューブを回転させる
    /// キューブにアタッチ
    /// </summary>
    public class CubeRotationModel : MonoBehaviour
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        public static CubeRotationModel Instance;
        
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 与えられたパラメータに応じてX軸方向にキューブを回転
        /// </summary>
        /// <param name="x">X軸回転</param>
        public void SetRotationX(float x)
        {
            var rot = Quaternion.AngleAxis(x, Vector3.right);
            transform.rotation =  rot;
        }

        /// <summary>
        /// 与えられたパラメータに応じてY軸方向にキューブを回転
        /// </summary>
        /// <param name="y">X軸回転</param>
        public void SetRotationY(float y)
        {
            var rot = Quaternion.AngleAxis(y, Vector3.up);
            transform.rotation =  rot;
        }

        /// <summary>
        /// 与えられたパラメータに応じてZ軸方向にキューブを回転
        /// </summary>
        /// <param name="z">Z軸回転</param>
        public void SetRotationZ(float z)
        {
            var rot = Quaternion.AngleAxis(z, Vector3.forward);
            transform.rotation =  rot;
        }
    }
}