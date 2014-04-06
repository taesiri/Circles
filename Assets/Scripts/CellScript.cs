using System.Globalization;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellScript : MonoBehaviour
    {
        public TextMesh Text;

        public CellScript Right;
        public CellScript Left;

        private int _cellValue = 1;

        public int Index = -1;

        public int CellValue
        {
            get { return _cellValue; }
            set
            {
                _cellValue = value;
                Text.text = _cellValue == 0 ? string.Empty : _cellValue.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}