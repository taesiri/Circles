using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Transform Origin;

        private bool _isHit = false;
        public float Threshold = 20f; // In Degrees!
        public float PosThreshold = 0.9f; // In Degrees!
        private CellScript _hitCell;
        private GroupScript _hitGroup;

        public float RotationUnit = 60;
        public GUISkin DefaultSkin;
        public Texture2D MenuTexture;
        public GUILocationHelper Location = new GUILocationHelper();

        private float _lastBeginAngle;
        private Vector2 _fingerPositionFirst;
        private GameObject _lastHitObject;


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
                        _lastHitObject = hitInfo.collider.gameObject;
                        _fingerPositionFirst = hitInfo.point;
                    }
                }
                else if (Input.touches[0].phase != TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        var worldPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);


                        var startPoint = _hitCell.transform.position;
                        var endPoint = new Vector3(_hitCell.transform.parent.position.x, _hitCell.transform.position.y, _hitCell.transform.parent.position.z);

                        var lastTouchPoint = new Vector3(worldPoint.x, endPoint.y, worldPoint.z);

                        Debug.DrawLine(startPoint, lastTouchPoint, Color.blue);
                        Debug.DrawLine(startPoint, endPoint, Color.red);


                        //var line1 = endPoint - startPoint;
                        //var line2 = lastTouchPoint - startPoint;


                        //Debug.Log(Helper.TruncateAngle(CalculateAngle(line1, line2)));
                    }
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        _isHit = false;

                        var worldPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                        var startPoint = _hitCell.transform.position;
                        var endPoint = new Vector3(_hitCell.transform.parent.position.x, _hitCell.transform.position.y, _hitCell.transform.parent.position.z);

                        var lastTouchPoint = new Vector3(worldPoint.x, endPoint.y, worldPoint.z);

                        Debug.DrawLine(startPoint, lastTouchPoint, Color.blue);
                        Debug.DrawLine(startPoint, endPoint, Color.red);

                        var line1 = endPoint - startPoint;
                        var line2 = lastTouchPoint - startPoint;
                        var diffAngle = Helper.TruncateAngle(CalculateAngle(line1, line2));

                        if (line2.magnitude > PosThreshold)
                        {
                            if (0 < diffAngle && diffAngle < 40.0f)
                            {
                                _hitGroup.MoveDiagonal(_hitCell, MovementType.GoInside);
                            }
                            else if (diffAngle < 359 && diffAngle > 360 - 40.0f)
                            {
                                _hitGroup.MoveDiagonal(_hitCell, MovementType.GoInside);
                            }
                            else if (diffAngle < 180 + 30.0f && diffAngle > 180 - 30.0f)
                            {
                                _hitGroup.MoveDiagonal(_hitCell, MovementType.GoOutSide);
                            }
                            else if (diffAngle < 180 - 30.0f && diffAngle > 40)
                            {
                                _hitGroup.Rotate(1*RotationUnit, _hitCell.Index);
                            }
                            else if (diffAngle < 360 - 40.0f && diffAngle > 180 - 30.0f)
                            {
                                _hitGroup.Rotate(-1*RotationUnit, _hitCell.Index);
                            }
                        }
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

        public float CalculateAngle(Vector3 origin)
        {
            var wPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            var deltaX = origin.z - wPoint.z;
            var deltaY = wPoint.x - origin.x;

            return Mathf.Atan2(deltaX, deltaY)*Mathf.Rad2Deg;
        }

        public float CalculateAngle(Vector3 line1, Vector3 line2)
        {
            var theta1 = Mathf.Atan2(line1.z, line1.x)*Mathf.Rad2Deg;
            var theta2 = Mathf.Atan2(line2.z, line2.x)*Mathf.Rad2Deg;


            return theta2 - theta1;
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