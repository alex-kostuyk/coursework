
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameUI : UI
{
    [SerializeField]
    private Slider _strenght_bar;
    [SerializeField]
    private Image _bar_fill;
    [SerializeField]
    private Gradient _bar_gradient;
    [SerializeField]
    private Text _player_1_score, _player_1_type, _player_2_score, _player_2_type, _turn,_winner;
    [SerializeField]
    private GameObject _move_icon,_pause_menu,_win_menu,_top_hud;
    private GameActivity _game_activity;
    private Push _push;
    private bool _game_end = false;
    private void Start()
    {
        _game_activity = FindObjectOfType<GameActivity>();
        _push = FindObjectOfType<Push>();
       
        _strenght_bar.maxValue = _push.MaxPushStrenght;
        _strenght_bar.minValue = _push.MinPushStrenght;  
        _strenght_bar.value = 0;
          Time.timeScale = 1;
    }
 
    public void SetStrenghtBarValue(float Value)
    {
        _strenght_bar.value = Value;
        _bar_fill.color = _bar_gradient.Evaluate(_strenght_bar.normalizedValue);
    }
    public void SetTypesText(BallType Player1,BallType Player2)
    {
        _player_1_type.text = "" + Player1;
        _player_2_type.text = "" + Player2;
    }
    public void SetTurnText(Turn TurnType)
    {
        _turn.text = "Turn:  " + TurnType;
    }
   public void SetScoresText()
    {
       _player_1_score.text= _game_activity.GetScoredNumber(Turn.Player1) + "/" + _game_activity.GetEachSideBallsCount();
        _player_2_score.text =  _game_activity.GetScoredNumber(Turn.Player2)+"/"+_game_activity.GetEachSideBallsCount();
    }
    public void MoveIconIsActive(bool State)
    {
        _move_icon.SetActive(State);
    }
    public void Pause(bool State)
    {
        if (!_game_end)
        {
            Time.timeScale = Convert.ToInt32(!State);
            _pause_menu.SetActive(State);
            _top_hud.SetActive(!State);
        }
    }
    public void ActivateWinScreen(Turn Winner)
    {
        _winner.text = Winner + " Win!";
        _top_hud.SetActive(false);
        _win_menu.SetActive(true);
        _game_end = true;
    }



}
