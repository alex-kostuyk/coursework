
using UnityEngine;

public class ArtificialIntelligenceInput :MonoBehaviour, IInput
{
    [SerializeField]
    private Push _push;
    [SerializeField]
    private TrajectoryProjection _trajectory_projection;
    [SerializeField]
    private GameActivity _game_activity;
    [SerializeField]
    private GameUI _game_UI;
    [SerializeField]
    private Cue _cue;
    private Quaternion _hit_angle;
    [SerializeField]
    private Transform _white_ball,_picking;
    private float _min_strength=4, _max_strength=12;
    private void Start()
    {
        _game_UI = FindObjectOfType<GameUI>();
        _cue = FindObjectOfType<Cue>();
        gameObject.SetActive(false);
    }
   
    public  void SetActiveInput(bool State)
    {
       if(State)
        {
            gameObject.SetActive(true);
            _trajectory_projection.gameObject.SetActive(true);
            _cue.SetActive(true);
            _round_initialize();
            _choose_rotation_angle();
            _choose_push_strenght(); 
            Invoke("_action", 2.5f);
        }
    }
 
    private void _round_initialize()
    {
       
        _picking.position = _white_ball.position;
        _picking.rotation = Quaternion.identity;
        _hit_angle = Quaternion.identity;
      
    }
    public void MoveBallAllowed()
    {
      
    }
    private void _choose_rotation_angle()
    {
        _picking.position = _white_ball.position;
          _picking.rotation = Quaternion.identity;
        bool _look_at_correct_ball;
        BallType _trajectory_hitting_type;
        for (float i=0;i<=360;i+=0.3f)
        {   

            _picking.rotation = Quaternion.Euler(0, i, 0);
            _trajectory_hitting_type = _trajectory_projection.GetTrajectoryHittingBallType();
            _look_at_correct_ball = _trajectory_hitting_type == _game_activity.GetPlayerType(Turn.Player2)||_game_activity.GetScoredNumber(Turn.Player2)>=7&& _trajectory_hitting_type == BallType.Black|| _game_activity.GetPlayerType(Turn.Player2)==BallType.Null;
            if (_look_at_correct_ball)
            {
                _hit_angle = _picking.rotation;
            }
            if (_trajectory_projection.CheckTrajectoryHittingHole(_picking) && _look_at_correct_ball)
            {
                _hit_angle = _picking.rotation;
                break;
            }  
        }
     
        _white_ball.rotation = _hit_angle;
        for(int i=0;i<5;i++)
        _trajectory_projection.DrawLine(true);


    }
    private void _choose_push_strenght()
    {
        _push.StrengthValue = Random.Range(_min_strength,_max_strength);
        _game_UI.SetStrenghtBarValue(_push.StrengthValue);

    }
    private void _action()
    {
        _trajectory_projection.gameObject.SetActive(false);
        _push.Strike();
        _cue.SetActive(false);
        Invoke("wait", 2.5f);
     
    }
    private void wait()
    {
        _push.StrengthValue = _push.MinPushStrenght;
        _game_UI.SetStrenghtBarValue(_push.MinPushStrenght);
        BallsSettings.Settings.CheckBallsStop();
        gameObject.SetActive(false);

    }

}
