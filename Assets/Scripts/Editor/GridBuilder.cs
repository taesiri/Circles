using System;
using UnityEditor;
using UnityEngine;
using Random = System.Random;
using Color = UnityEngine.Color;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof (Generator))]
    public class GridBuilder : UnityEditor.Editor
    {
        private Random _randomGenerator = new Random(DateTime.Now.Millisecond);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label("Way point Type");

            if (GUILayout.Button("Generate Grid"))
            {
                GenerateGrid();
            }
            if (GUILayout.Button("Generate Grid 2"))
            {
                GenerateGrid2();
            }
            if (GUILayout.Button("Clear Childs"))
            {
                ClearChilds();
            }
            if (GUI.changed)
            {
                HandleChanges();
            }
        }

        private void ClearChilds()
        {
            var currentObject = (Generator) target;
            var killList = new Transform[currentObject.transform.childCount];
            for (int i = 0; i < killList.Length; i++)
            {
                killList[i] = currentObject.transform.GetChild(i);
            }

            for (int i = 0; i < killList.Length; i++)
            {
                DestroyImmediate(killList[i].gameObject);
            }
        }

        private void HandleChanges()
        {
            //throw new System.NotImplementedException();
        }


        public void GenerateGrid()
        {
            var currentObject = (Generator) target;


            GroupScript prev = null;
            for (int j = 1; j < currentObject.Radius; j += currentObject.RIncerement)
            {
                var group = new GameObject(string.Format("Circle Gorup {0}", j.ToString()));
                var groupSCript = group.AddComponent<GroupScript>();

                if (prev != null)
                {
                    groupSCript.PrevGroup = prev;
                    prev.NextGroup = groupSCript;
                }


                group.transform.parent = currentObject.transform;
                var rs = group.AddComponent<RotationScript>();

                rs.RotationSpeed = (_randomGenerator.Next(0, 100) - 50)/10f;

                for (int i = 0; i < j*currentObject.NumberOfCircles; i++)
                {
                    currentObject.Aangle = 360/(float) (j*currentObject.NumberOfCircles);

                    var objects = new GameObject[j*currentObject.NumberOfCircles];

                    objects[i] = (GameObject) Instantiate(currentObject.CirclePrefab, currentObject.CenterTransform.position + Vector3.right*j, Quaternion.identity);
                    objects[i].name = string.Format("GenSphere {0}:{1}", j, i);
                    objects[i].transform.RotateAround(currentObject.CenterTransform.position, Vector3.up, i*currentObject.Aangle);
                    objects[i].transform.parent = group.transform;
                    objects[i].AddComponent<CircleScript>();
                    var cfull = objects[i].AddComponent<Colorful>();
                    cfull.MatColor = GeneratoeColor();
                }

                prev = groupSCript;
            }
        }

        public void GenerateGrid2()
        {
            var currentObject = (Generator) target;


            for (int j = 1; j < currentObject.Radius; j += currentObject.RIncerement)
            {
                var group = new GameObject(string.Format("Circle Gorup {0}", j.ToString()));
                group.AddComponent<GroupScript>();

                group.transform.parent = currentObject.transform;
                var rs = group.AddComponent<RotationScript>();

                rs.RotationSpeed = (_randomGenerator.Next(0, 100) - 50)/10f;

                for (int i = 0; i < currentObject.NumberOfCircles; i++)
                {
                    currentObject.Aangle = 360/(float) (currentObject.NumberOfCircles);

                    var objects = new GameObject[currentObject.NumberOfCircles];

                    objects[i] = (GameObject) Instantiate(currentObject.CirclePrefab, currentObject.CenterTransform.position + Vector3.right*j, Quaternion.identity);
                    objects[i].name = string.Format("GenSphere {0}:{1}", j, i);
                    objects[i].transform.RotateAround(currentObject.CenterTransform.position, Vector3.up, i*currentObject.Aangle);
                    objects[i].transform.parent = group.transform;
                    objects[i].AddComponent<CircleScript>();
                    var cfull = objects[i].AddComponent<Colorful>();
                    cfull.MatColor = GeneratoeColor();
                }
            }
        }

        private Color GeneratoeColor()
        {
            //float r = _randomGenerator.Next(1, 10)/100f;
            //float g = _randomGenerator.Next(1, 15)/100f;
            //float b = _randomGenerator.Next(1, 10)/100f;

            float val = _randomGenerator.Next(1, 100)/100f;
            return new Color(val, val, val, 1.0f);
        }
    }
}