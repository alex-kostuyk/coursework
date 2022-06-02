
using UnityEngine;

public class MainMenuUI : UI
{
    [SerializeField]
    private GameObject[] _turn_on,_turn_off;
    [SerializeField]
    private TurnSystem _turn_system;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void StartGameAI(bool State)
    {
        _turn_system.EnemyIsAI = State;
        _start_round();
    }
  
    private void _start_round()
    {
     
        foreach(GameObject on in _turn_on)
        {
            on.SetActive(true);
        }
        foreach (GameObject off in _turn_off)
        {
            off.SetActive(false);
        }
        
    }
}
