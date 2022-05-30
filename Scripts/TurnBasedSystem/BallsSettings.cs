using System.Collections;
using UnityEngine;

public class BallsSettings : MonoBehaviour
{
   
    public static BallsSettings Settings { get; private set; }

   
    public  float RaycastTargetRadius { get { return _raycast_target_radius; } private set { _raycast_target_radius = value; } }
    public float BounceForce { get { return _bounce_force; } private set { _bounce_force = value; } }
    public float RigidBodyDrag { get { return _rigid_body_drag; } private set { _rigid_body_drag = value; } }
    [SerializeField]
    private float _bounce_force, _raycast_target_radius,_rigid_body_drag, _freaquency_balls_stop_check, _magnitude_balls_stop;
    private Rigidbody[] _balls;
    private TurnSystem _turn_system;

    private void Awake()
    {
        if(Settings==null)
        {
            Settings = this;
           // DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);

    }
    private void Start()
    {
        _balls = FindObjectsOfType<Rigidbody>();
        _turn_system = FindObjectOfType<TurnSystem>();
    }
    public void RigidBodysIsKinematic(bool State)
    {
        _balls = FindObjectsOfType<Rigidbody>();
        for (int i = 0; i < _balls.Length; i++)
        {
            _balls[i].isKinematic = State;
        }
    }
    public void CheckBallsStop()
    {
        StartCoroutine(_coroutine());

    }
    private IEnumerator _coroutine()
    {
        bool stop = false;
        while (!stop)
        {
            _balls = FindObjectsOfType<Rigidbody>();
            stop = true;
            for (int i = 0; i < _balls.Length; i++)
            {
                if (_balls[i].velocity.magnitude > _magnitude_balls_stop)
                {
                    stop = false;

                }

            }
            yield return new WaitForSeconds(_freaquency_balls_stop_check);
        }

        BallsSettings.Settings.RigidBodysIsKinematic(true);
        _turn_system.RoundEnd();
    }

}
