using UnityEngine;
using Color = UnityEngine.Color;

namespace Assets.Scripts
{
    public class Colorfull : MonoBehaviour
    {
        [SerializeField] public Color MatColor;

        public void Start()
        {
            renderer.material.color = MatColor;
        }
    }
}