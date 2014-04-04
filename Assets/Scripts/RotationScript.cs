using UnityEngine;

namespace Assets.Scripts
{
    public class RotationScript : MonoBehaviour
    {
        public float RotationSpeed = 10;


        private float _diff = 0;

        private void Start()
        {
        }

        private void Update()
        {
            //transform.Rotate(Vector3.up, RotationSpeed*Time.deltaTime);
        }

        public void Rotate(float angle)
        {
            transform.Rotate(Vector3.up, angle);
        }
    }
}