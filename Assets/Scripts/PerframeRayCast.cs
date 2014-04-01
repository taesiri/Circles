using UnityEngine;

namespace Assets.Scripts
{
    public class PerframeRayCast : MonoBehaviour
    {
        private GameObject _lastHitObject;
        private Vector3? _lastHitPosition;

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);

            if (hitInfo.collider.gameObject)
            {
                _lastHitObject = hitInfo.collider.gameObject;
                _lastHitPosition = hitInfo.point;
            }
            else
            {
                _lastHitObject = null;
                _lastHitPosition = null;
            }
        }

        public GameObject GetLastHitObject()
        {
            return _lastHitObject;
        }

        public Vector3? GetLastHitPosition()
        {
            return _lastHitPosition;
        }
    }
}