using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Transform Origin;

        private bool _isHit = false;
        public float Threshold = 20f; // In Degrees!
        private CellScript _hitCell;
        private GroupScript _hitGroup;
        private float _lastBeginAngle;

        public float RotationUnit = 60;
        public GUISkin DefaultSkin;
        public Texture2D MenuTexture;

        public GUILocationHelper Location = new GUILocationHelper();

        private Vector2 _fingerPositionFirst;

        public void Start()
        {
            Location.PointLocation = GUILocationHelper.Point.Center;
            Location.UpdateLocation();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                    RaycastHit hitInfo;
                    Physics.Raycast(ray, out hitInfo);

                    if (hitInfo.collider)
                    {
                        _isHit = true;
                        var angle = CalculateAngle();
                        _lastBeginAngle = angle;
                        _hitCell = hitInfo.collider.gameObject.GetComponent<CellScript>();
                        _hitGroup = hitInfo.collider.gameObject.transform.parent.GetComponent<GroupScript>();

                        _fingerPositionFirst = hitInfo.point;
                    }
                }
                else if (Input.touches[0].phase != TouchPhase.Ended)
                {
                    //if (_isHit)
                    //{
                    //    var angle = CalculateAngle();
                    //    _lastGroup.Rotate(angle - _lastRotation);
                    //    _lastRotation = angle;
                    //}
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        _isHit = false;

                        var endAngle = CalculateAngle();
                        var sign = _lastBeginAngle - endAngle > 0 ? -1 : 1;


                        var absMovement = Mathf.Abs(_lastBeginAngle - endAngle);
                        var fingerPositionLast = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                        if (absMovement > Threshold)
                        {
                            Debug.Log(string.Format("Start Angle {0}, End Angle {1}", _lastBeginAngle, endAngle));
                            //_hitObjec.transform.parent.transform.Rotate(Vector3.up, sign*RotationUnit);

                            _hitGroup.Rotate(sign*60, _hitCell.Index);

                            //if (sign == 1)
                            //    _hitGroup.MoveRight();
                            //else
                            //    _hitGroup.MoveLeft();
                        }
                        else
                        {
                        }

                        Debug.Log(((Vector2) fingerPositionLast - _fingerPositionFirst).ToString());
                    }
                }
            }
            else
            {
                _isHit = false;
            }
        }

        public float CalculateAngle()
        {
            var wPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            var deltaX = Origin.transform.position.z - wPoint.z;
            var deltaY = wPoint.x - Origin.transform.position.x;

            return Mathf.Atan2(deltaX, deltaY)*Mathf.Rad2Deg;
        }

        public void OnGUI()
        {
            Vector2 ratio = Location.GuiOffset;
            Matrix4x4 guiMatrix = Matrix4x4.identity;
            guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
            GUI.matrix = guiMatrix;


            if (GUI.Button(new Rect(15, 15, 64, 64), MenuTexture, DefaultSkin.button))
            {
                Application.LoadLevel(0);
            }
        }
    }
}