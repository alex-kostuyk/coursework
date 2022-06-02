using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Balls : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private BallType _type;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if(transform.GetChild(0).gameObject.TryGetComponent(out SphereCollider collider))
        {
            collider.radius=BallsSettings.Settings.RaycastTargetRadius;
        }
        _rigidbody.drag = BallsSettings.Settings.RigidBodyDrag;
        _rigidbody.angularDrag = BallsSettings.Settings.RigidBodyDrag;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Balls>()!=null)
        {
            Vector3 direction = collision.contacts[0].point - transform.position;
   
                   collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * _rigidbody.velocity.magnitude * BallsSettings.Settings.BounceForce, ForceMode.Impulse);
        }
    }
    public BallType GetBallType()
    {
        return _type;
    }
}
