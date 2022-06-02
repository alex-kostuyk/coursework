
using UnityEngine;


public class Rotate : MonoBehaviour,IMovement
{
    [SerializeField]
    private float  _speed = 10;
   private Transform _transform;
 
    private void Start()
    {
        _transform = transform;
    }
   public void ResetXZAngle()
    {
        _transform.rotation = Quaternion.Euler(0, _transform.rotation.eulerAngles.y, 0);
    }
    public void ToPoint(RaycastHit hit)
    {
               var lookPos = hit.point - _transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _speed * Time.deltaTime);
    }
}


