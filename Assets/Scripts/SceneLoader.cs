using UnityEngine;

namespace Assets.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        public void Start()
        {
        }

        public void OnGUI()
        {
            if (GUI.Button(new Rect(Screen.width/2f - 100, Screen.height/2f - 100, 200, 50), "Scene 1"))
            {
                Application.LoadLevel(1);
            }
            if (GUI.Button(new Rect(Screen.width/2f - 100, Screen.height/2f, 200, 50), "Scene 2"))
            {
                Application.LoadLevel(2);
            }
        }
    }
}