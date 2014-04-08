using UnityEngine;

namespace Assets.Scripts
{
    public class Generator : MonoBehaviour
    {
        public GameObject CirclePrefab;
        public float Radius = 50;
        public int NumberOfCircles = 10;
        public Transform CenterTransform;
        public int RIncerement = 1;
        public float Aangle;
        public float CenterOffset = 2.0f;
        public float MiddleOffset = 2.0f;
        public float NodeScales = 1.95f;

        private void Start()
        {
            //if (CirclePrefab)
            //{
            //    for (int j = 1; j < Radius; j += RIncerement)
            //    {
            //        for (int i = 0; i < j*NumberOfCircles; i++)
            //        {
            //            Aangle = 360/(float) (j*NumberOfCircles);

            //            GameObject[] objects = new GameObject[j*NumberOfCircles];

            //            objects[i] = (GameObject) Instantiate(CirclePrefab, CenterTransform.position + Vector3.right*j, Quaternion.identity);
            //            objects[i].transform.RotateAround(CenterTransform.position, Vector3.up, i*Aangle);
            //        }
            //    }
            //}
        }


        private void Update()
        {
        }
    }
}