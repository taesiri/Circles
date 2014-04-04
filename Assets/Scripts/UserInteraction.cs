using UnityEngine;

namespace Assets.Scripts
{
    public class UserInteraction : MonoBehaviour
    {
        private bool _isHit = false;
        public Transform Origin;
        private RotationScript _lastGroup;


        private float _lastRotation;

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
                        var circle = hitInfo.collider.gameObject.GetComponent<CircleScript>();
                        if (circle)
                            circle.SetMatColorForSeconds(Color.red, 10);

                        _isHit = true;
                        _lastGroup = hitInfo.collider.gameObject.transform.parent.GetComponent<RotationScript>();

                        //=============================================================================================

                        var wPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                        var deltaX = Origin.transform.position.z - wPoint.z;
                        var deltaY = wPoint.x - Origin.transform.position.x;

                        var angle = Mathf.Atan2(deltaX, deltaY)*Mathf.Rad2Deg;

                        _lastRotation = angle;
                    }
                }
                else if (Input.touches[0].phase != TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        var wPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                        var deltaX = Origin.transform.position.z - wPoint.z;
                        var deltaY = wPoint.x - Origin.transform.position.x;

                        var angle = Mathf.Atan2(deltaX, deltaY)*Mathf.Rad2Deg;


                        _lastGroup.Rotate(angle - _lastRotation);

                        _lastRotation = angle;
                    }
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (_isHit)
                    {
                        _isHit = false;

                        var wPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                        var deltaX = Origin.transform.position.z - wPoint.z;
                        var deltaY = wPoint.x - Origin.transform.position.x;

                        var angle = Mathf.Atan2(deltaX, deltaY)*Mathf.Rad2Deg;
                        _lastGroup.Rotate(angle - _lastRotation);

                        _lastRotation = angle;
                    }
                }
            }
            else
            {
                _isHit = false;
            }
        }
    }
}

// OLD UPDATE(){

// if (Input.touchCount > 0)
//            {
//                if (Input.touches[0].phase == TouchPhase.Began)
//                {
//                    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

//                    RaycastHit hitInfo;
//                    Physics.Raycast(ray, out hitInfo);


//                    if (hitInfo.collider)
//                    {
//                        hitInfo.collider.gameObject.renderer.material.color = Color.red;

//                        _isHit = true;
//                        Debug.Log(hitInfo.collider.gameObject.transform.parent);
//                        _lastGroup = hitInfo.collider.gameObject.transform.parent.GetComponent<RotationScript>();
//                        Debug.Log(_lastGroup.RotationSpeed);
//                    }
//                }
//                else if (Input.touches[0].phase != TouchPhase.Ended)
//                {
//                    if (_isHit)
//                    {
//                        Debug.Log("Rotatin");
//                        _lastGroup.Rotate(Input.touches[0].deltaPosition.magnitude);
//                    }
//                }
//                else if (Input.touches[0].phase == TouchPhase.Ended)
//                {
//                    _isHit = false;
//                }
//            }
//            else
//            {
//                _isHit = false;
//            }

//}