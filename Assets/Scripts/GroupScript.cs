using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GroupScript : MonoBehaviour
    {
        public GroupScript NextGroup;
        public GroupScript PrevGroup;

        public CellScript[] CellChilds;

        public float RotationSpeed = 200;
        private float _remainingRotation;
        private bool _isRotating;
        private float _goalRotation;

        private int _signOfRotation = 1;
        private int _cellIndex;
        private Action<int> _finishAction;

        public void Rotate(float degree, int index)
        {
            if (!_isRotating)
            {
                if (degree < 0)
                {
                    _signOfRotation = -1;
                    degree *= -1;
                    _finishAction = MoveLeft;
                }
                else
                {
                    _signOfRotation = 1;
                    _finishAction = MoveRight;
                }

                _remainingRotation = degree;
                _goalRotation = _signOfRotation*degree + transform.eulerAngles.y;
                _isRotating = true;
                _cellIndex = index;
            }
        }

        public void Update()
        {
            if (_isRotating)
            {
                if (_remainingRotation <= -1)
                {
                    _isRotating = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, _goalRotation, transform.eulerAngles.z);

                    _finishAction(_cellIndex);
                }
                else
                {
                    var deltaR = RotationSpeed*Time.deltaTime;
                    transform.Rotate(Vector3.up, deltaR*_signOfRotation);

                    _remainingRotation -= deltaR;
                }
            }
        }

        public GameObject[] FindGameObjects(float angle)
        {
            return null;
        }

        public GameObject FindGameObject(float angle)
        {
            Debug.Log(string.Format("Search for an Angle : {0}", angle));
            for (int i = 0; i < transform.childCount; i++)
            {
                if (Math.Abs(transform.GetChild(i).rotation.eulerAngles.y - angle) < 5f)
                {
                    return transform.GetChild(i).gameObject;
                }
            }

            return null;
        }

        public void MoveRight(int index)
        {
            var indexer = new RingIndexHelper(CellChilds.Length);
            var indexes = indexer.GenerateFullCycle(index);


           // Debug.Log(index);

            //for (int i = 0; i < indexes.Length-1; i++)
            //{
            //    if (CellChilds[indexes[i]].CellValue == CellChilds[indexes[i + 1]].CellValue)
            //    {
            //        CellChilds[indexes[i + 1]].CellValue *= 2;
            //        CellChilds[indexes[i]].CellValue = 0;
            //        i++;
            //    }
            //    else if (CellChilds[indexes[i]].CellValue != 0)
            //    {
            //        if (CellChilds[indexes[i + 1]].CellValue == 0)
            //        {
            //            CellChilds[indexes[i + 1]].CellValue = CellChilds[index].CellValue;
            //            CellChilds[indexes[i]].CellValue = 0;
            //        }
            //    }
            //}

            if (CellChilds[index].CellValue == CellChilds[indexer.GetNext(index)].CellValue)
            {
                CellChilds[indexer.GetNext(index)].CellValue *= 2;

                CellChilds[index].CellValue = 0;
            }
            else if (CellChilds[index].CellValue != 0)
            {
                if (CellChilds[indexer.GetNext(index)].CellValue == 0)
                {
                    CellChilds[indexer.GetNext(index)].CellValue = CellChilds[index].CellValue;
                    CellChilds[index].CellValue = 0;
                }
            }


            //for (int i = 0; i < CellChilds.Length; i++)
            //{
            //    if (CellChilds[i].CellValue == CellChilds[indexer.GetNext(i)].CellValue)
            //    {
            //        CellChilds[indexer.GetNext(i)].CellValue *= 2;

            //        CellChilds[i].CellValue = 0;

            //        i++;
            //    }
            //    else if (CellChilds[i].CellValue != 0)
            //    {
            //        if (CellChilds[indexer.GetNext(i)].CellValue == 0)
            //        {
            //            CellChilds[indexer.GetNext(i)].CellValue = CellChilds[i].CellValue;
            //            CellChilds[i].CellValue = 0;
            //        }
            //    }
            //}
        }

        public void MoveLeft(int index)
        {
        }
    }
}