using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GroupScript : MonoBehaviour
    {
        public GroupScript NextGroup;
        public GroupScript PrevGroup;

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
    }
}