
using UnityEngine;

public  class UserInput : MonoBehaviour
{
    private CameraSwitchMod _camera_switch_mod;
    private GameUI _game_UI;

    private void Start()
    {
        _camera_switch_mod = FindObjectOfType<CameraSwitchMod>();
        _game_UI = FindObjectOfType<GameUI>();
    }


    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _game_UI.Pause(true);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _camera_switch_mod.SwitchMod();
            }
        }
       
    }
}
