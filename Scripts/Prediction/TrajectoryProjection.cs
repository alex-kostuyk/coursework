
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class TrajectoryProjection : MonoBehaviour
{

	[SerializeField]
	private float _max_forward_line_length, _max_bounce_line_length, _sphere_cast_radius;
	[SerializeField]
	private LineRenderer _line_renderer;
	[SerializeField]
	private LayerMask _ignore_me;
	private Ray ray;
	private RaycastHit hit;
	private Transform ball;
	private Vector3  _direction;
	private Vector3 [] points=new Vector3[3];
	private bool _hit_hole=false;
	private BallType _ball_type;
	private void Start()
	{
		ball = FindObjectOfType<Move>().transform;
		_line_renderer.positionCount = points.Length;
	}
    public bool  CheckTrajectoryHittingHole(Transform Start)
    {
		_sphere_cast_trajectory(Start);
		return _hit_hole;
    }
	public BallType GetTrajectoryHittingBallType()
	{
		return _ball_type;
	}

	public void DrawLine(bool State)
	{
		_line_renderer.enabled = State;
		_sphere_cast_trajectory(ball);

		for(int i=0;i<points.Length;i++)
        {
          _line_renderer.SetPosition(i, points[i]);
        }
	
	}
	private void _sphere_cast_trajectory(Transform start)
    {
		ray = new Ray(start.position, start.forward);
		points[0] = start.position;

		if (Physics.SphereCast(ray.origin, _sphere_cast_radius, ray.direction, out hit, _max_forward_line_length, ~_ignore_me))
		{		
			points[1] = hit.point + (hit.normal * _sphere_cast_radius);
			_hit_hole = false;
			_ball_type = BallType.Null;
			 Balls _balls = hit.collider.GetComponent<Balls>();
			if (_balls != null)
			{

				_direction =points[1] - hit.point;
				points[2]= points[1] - (_direction.normalized * _max_bounce_line_length);

				_ball_type = _balls.GetBallType();
				_direction = points[2] - points[1];
				
				ray = new Ray(points[2], -_direction.normalized * _max_forward_line_length);
				if (Physics.SphereCast(ray.origin,_sphere_cast_radius, ray.direction, out hit, _max_forward_line_length))
				{
					if (hit.collider.GetComponent<Holes>() != null)
					{
						
						_hit_hole = true;
					}
				   
				}
				
			}
            else 
			{
				points[2] = points[1];
				
			}


		}
	}
    
}
