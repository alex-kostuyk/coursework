
using UnityEngine;


public class GameActivity : MonoBehaviour
{

    [SerializeField]
    private int _each_side_balls_count = 7;
    private BallType _player_1_type, _player_2_type;
    private int _total_scored_full_balls, _total_scored_strip_balls, _round_scored_full_balls, _round_scored_strip_balls;
    private bool _scored_white_ball = false, _scored_black_ball = false, _full_ball_pushed = false, _strip_ball_pushed = false, _black_ball_pushed = false, _ball_dropped_out = false;
    private TurnSystem _turn_system;
    private GameUI _gameUI;

    private void Start()
    {
        _turn_system = FindObjectOfType<TurnSystem>();
        _gameUI = FindObjectOfType<GameUI>();
    }

    public void SetPlayersTypes(BallType FirstBallType)
    {
        if (_total_scored_full_balls == 0 && _total_scored_strip_balls == 0)
        {
            if ((_turn_system.GetWhoseTurn() == Turn.Player1) == (FirstBallType == BallType.Strip))
            {
                _player_1_type = BallType.Strip;
                _player_2_type = BallType.Full;
            }
            else
            {
                _player_1_type = BallType.Full;
                _player_2_type = BallType.Strip;
            }
            _gameUI.SetTypesText(_player_1_type, _player_2_type);

        }
    }
    public void ScoredFullBall()
    {
        _total_scored_full_balls++;
        _round_scored_full_balls++;
        _gameUI.SetScoresText();
    }
    public void ScoredStripBall()
    {
        _total_scored_strip_balls++;
        _round_scored_strip_balls++;
        _gameUI.SetScoresText();
    }
    public void ScoredWhiteBall()
    {
        _scored_white_ball = true;
    }
    public void ScoredBlackBall()
    {
         
        _scored_black_ball = true;
    }
    public void FullBallPushed()
    {
        _full_ball_pushed = true;
    }
    public void StripeBallPushed()
    {
        _strip_ball_pushed = true;
    }
    public void BlackBallPushed()
    {
        _black_ball_pushed = true;
    }
    public int GetEachSideBallsCount()
    {
        return _each_side_balls_count;
    }
    public void ClearRoundData()
    {
        _scored_white_ball = false;
        _scored_black_ball = false;
        _full_ball_pushed = false;
        _strip_ball_pushed = false;
        _ball_dropped_out = false;
        _black_ball_pushed = false;
        _round_scored_strip_balls = 0;
        _round_scored_full_balls = 0;
    }
    public bool GetWhiteBallScored()
    {
        return _scored_white_ball;
    }
    public bool GetBlackBallScored()
    {
        return _scored_black_ball;
    }
    public bool GetBallDropped_Out()
    {
        return _ball_dropped_out;
    }
    public bool RightBallScoredCheck(Turn _turn)
    {
       
        return (GetPlayerType(_turn) == BallType.Strip && _round_scored_strip_balls != 0 || GetPlayerType(_turn) == BallType.Full && _round_scored_full_balls != 0);
    }
    public bool RightBallPushedCheck(Turn _turn)
    {
         return((_strip_ball_pushed && GetPlayerType(_turn) == BallType.Strip) 
            || (_full_ball_pushed && GetPlayerType(_turn) == BallType.Full)
             || (_strip_ball_pushed || _full_ball_pushed) && _player_1_type == BallType.Null 
              || (GetScoredNumber(_turn) == _each_side_balls_count) && _black_ball_pushed);
    }
    public int GetScoredNumber(Turn _turn)
    {

        if (GetPlayerType(_turn) == BallType.Full)
            return _total_scored_full_balls;
        else
            return _total_scored_strip_balls;

    }
    public BallType GetPlayerType(Turn _turn)
    {
        BallType _player_type = _player_2_type;
        if (_turn == Turn.Player1)
            _player_type = _player_1_type;

        return _player_type;

    }
}
