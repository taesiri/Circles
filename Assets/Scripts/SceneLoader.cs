using UnityEngine;

namespace Assets.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        public GUILocationHelper Location = new GUILocationHelper();

        public void Start()

        {
            Location.PointLocation = GUILocationHelper.Point.Center;
            Location.UpdateLocation();
        }

        public void OnGUI()
        {
            Vector2 ratio = Location.GuiOffset;
            Matrix4x4 guiMatrix = Matrix4x4.identity;
            guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
            GUI.matrix = guiMatrix;

            if (GUI.Button(new Rect(Location.Offset.x - 100, Location.Offset.y - 150, 200, 45), "Scene 1"))
            {
                Application.LoadLevel(1);
            }
            if (GUI.Button(new Rect(Location.Offset.x - 100, Location.Offset.y - 100, 200, 45), "Scene 2"))
            {
                Application.LoadLevel(2);
            }
            if (GUI.Button(new Rect(Location.Offset.x - 100, Location.Offset.y - 50, 200, 45), "Game Board 1"))
            {
                Application.LoadLevel(5);
            }
            if (GUI.Button(new Rect(Location.Offset.x - 100, Location.Offset.y, 200, 45), "Game Board 2"))
            {
                Application.LoadLevel(5);
            }
        }
    }
}