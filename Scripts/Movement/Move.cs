
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour,IMovement
{
    [SerializeField]
    private float  _max_position_x, _max_position_z;
    private float _start_z_position;
    private Rigidbody _rigidbody;
    private bool _snap_z=true;
    private Vector3 _start_position;
   
    private void Start()
    {
    _rigidbody = GetComponent<Rigidbody>();
    _start_z_position = transform.position.z;
    _start_position = transform.position;
       
    }
   public void SnapZAxis(bool State)
    {
        _snap_z = State;
    }
    public void ResetPosition()
    {
        transform.position = _start_position;
    }
  
    public void ToPoint(RaycastHit hit)
    {
        if (hit.collider.GetComponent<BoardMarker>() != null)
        {
            var MovePosition = hit.point;
            MovePosition.y = 0;
            MovePosition.x = Mathf.Clamp(MovePosition.x, -_max_position_x, _max_position_x);
            if (_snap_z)
                MovePosition.z = _start_z_position;
            else
                MovePosition.z = Mathf.Clamp(MovePosition.z, -_max_position_z, _max_position_z);
            _rigidbody.MovePosition(MovePosition);

        }
    }

   
}
