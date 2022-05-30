
using UnityEngine;

public class TurnSystem : MonoBehaviour
{  
    private GameActivity _game_activity;
    private Turn _turn,_enemy;
    private Move _move;
    private PlayerInput _player_input;
    private ArtificialIntelligenceInput _ai_input;
    private Inputter _input;
    private GameUI _gameUI;
    private Rotate _rotate;
    private Cue _cue;
    public bool EnemyIsAI;


    private void Start()
    {
        _turn = Turn.Player1;
        if (EnemyIsAI)
            _enemy = Turn.AI;
        else
            _enemy = Turn.Player2;

        _game_activity=FindObjectOfType<GameActivity>();
        _ai_input = FindObjectOfType<ArtificialIntelligenceInput>();
        _player_input = FindObjectOfType<PlayerInput>();
        _move = FindObjectOfType<Move>();
        _rotate = FindObjectOfType<Rotate>();
        _gameUI = FindObjectOfType<GameUI>();
        _gameUI.SetTurnText(_turn);
        _cue = FindObjectOfType<Cue>();
        _input = gameObject.AddComponent(typeof(Inputter))as Inputter; 
    }

    public Turn GetWhoseTurn()
    {
        return _turn;
    }
    public void SwipeTurn()
    {
        if (_turn == Turn.Player1)
        {
            _turn = _enemy;
        }
        else 
        {
            _turn = Turn.Player1;
        }

        _gameUI.SetTurnText(_turn);
    }
    public void ContinueRound()
    {
        _input.StartInput(_get_current_input_type(_turn));
        _rotate.ResetXZAngle();
        _cue.SetCuePosition();
        _cue.SetActive(true);
        BallsSettings.Settings.RigidBodysIsKinematic(false);
    }
    private IInput _get_current_input_type(Turn turn)
    {
            if (EnemyIsAI && turn == Turn.AI)
               return _ai_input;

            return _player_input;
    }
    public void RoundEnd()
    {
        if (_game_activity.GetWhiteBallScored()|| !_game_activity.RightBallPushedCheck(_turn))
        {
            _move.ResetPosition();
            _input.AllowMove(_get_current_input_type(_turn));
        }
        if (_game_activity.GetBlackBallScored())
        {
            FindWinner();
            return;
        }
        if (!_game_activity.RightBallScoredCheck(_turn) || _game_activity.GetWhiteBallScored() || _game_activity.GetBallDropped_Out() || !_game_activity.RightBallPushedCheck(_turn))
        {
            SwipeTurn();
        }

         ContinueRound();

        _game_activity.ClearRoundData();
    }
    public void FindWinner()
    {
        Turn Winner;
        if (_game_activity.GetWhiteBallScored() || (_game_activity.GetEachSideBallsCount() - _game_activity.GetScoredNumber(_turn)) != 0)
        {  
            if (_turn == Turn.Player1)
                Winner = _enemy;
            else
                Winner = Turn.Player1;
        }
        else
        {      
            Winner = _turn;
        }
        _gameUI.ActivateWinScreen(Winner);
    }
  

}
