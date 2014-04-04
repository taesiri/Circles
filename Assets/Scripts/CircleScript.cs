using UnityEngine;

namespace Assets.Scripts
{
    public class CircleScript : MonoBehaviour
    {
        private Color _oldColor;
        private bool _isEnable;
        private float _lastTick;
        private float _interval;

        public void SetMatColorForSeconds(Color newColor, float seconds)
        {
            if (!_isEnable)
            {
                _oldColor = renderer.material.color;
                _lastTick = Time.time;
                _isEnable = true;
                _interval = seconds;

                renderer.material.color = newColor;
            }
            else
            {
                _lastTick = Time.time;
            }
        }

        public void Update()
        {
            if (_isEnable)
            {
                if (Time.time - _lastTick > _interval)
                {
                    _isEnable = false;
                    renderer.material.color = _oldColor;
                }
            }
        }
    }
}