using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligenceInput : MonoBehaviour, IInput
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
    private bool _move_ball;
    private Quaternion _hit_angle;
    private Vector3 _move_to_point;
    private float _push_strenght;
    [SerializeField]
    private Transform _white_ball,_picking;
    private void Start()
    {
        _game_UI = FindObjectOfType<GameUI>();
        _cue = FindObjectOfType<Cue>();
    }
    private void OnEnable()
    {
        _round_initialize();
        _choose_move_point();
        _choose_rotation_angle();
        _choose_push_strenght();
        _action();
    }
    public void SetActiveInput(bool State)
    {
        gameObject.SetActive(State);

    }
    private void _round_initialize()
    {
        _move_to_point = _white_ball.position;
        _picking.position = _white_ball.position;
        _picking.rotation = Quaternion.identity;
        _hit_angle = Quaternion.identity;
        _push_strenght = 0;
    }
    public void MoveBallAllowed()
    {
        _move_ball = true;
        
    }
    private void _choose_move_point()
    {
        if(_move_ball)
        {
            //вибыр рандомноъ точки
          
        }
        else
        {
            _move_to_point = _white_ball.position;
        }
      
    }
    private void _choose_rotation_angle()
    {
        for(float i=0;i<=360;i+=0.3f)
        {
            _picking.rotation = Quaternion.Euler(0, i, 0);
            if(_trajectory_projection.CheckTrajectoryHittingHole(_picking)&&(_trajectory_projection.GetTrajectoryHittingBallType()==_game_activity.GetPlayerType(Turn.Player2)|| _game_activity.GetPlayerType(Turn.Player2)==BallType.Null))
            {
                _hit_angle = _picking.rotation;
                Debug.Log("best");
            }
           else if(_trajectory_projection.GetTrajectoryHittingBallType() == _game_activity.GetPlayerType(Turn.Player2))
            {
                _hit_angle = _picking.rotation;
                Debug.Log("last");
            }
        }
        Debug.Log(_hit_angle.eulerAngles);
      
    }
    private void _choose_push_strenght()
    {
        _push.StrengthValue = Random.Range(_push.MinPushStrenght+5,_push.MaxPushStrenght);
    }
    private void _action()
    {
        _white_ball.rotation = _hit_angle;
        _game_UI.SetStrenghtBarValue(_push.StrengthValue);
        _trajectory_projection.DrawLine(true);
        Invoke("wait", 1);
        

    }
    private void wait()
    {
        _cue.SetActive(false);
        _push.Strike();
        _push.StrengthValue = 0; 
        _game_UI.SetStrenghtBarValue(_push.StrengthValue);
        BallsSettings.Settings.CheckBallsStop();
        _move_ball = false;
        SetActiveInput(false);
    }
   
}
