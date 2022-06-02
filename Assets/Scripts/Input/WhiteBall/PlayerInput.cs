using System.Collections;
using UnityEngine;

public class PlayerInput :MonoBehaviour, IInput
{
    [SerializeField]
    private float _scroll_sensitivity = 1;
    [SerializeField]
    private GameObject _trail_renderer;
    private Rotate _rotate;
    private Push _push;
    private Move _move;
    private TrajectoryProjection _trajectory_projection;
    private GameUI _game_UI;
    private Cue _cue;
    private bool _move_ball = true, _left_mouse_button_down = false;
    private Movement _movement;
    private IMovement _i_movement;
    protected RaycastHit _hit;

    private void Start()
    {
        _rotate = FindObjectOfType<Rotate>();
        _move = FindObjectOfType<Move>();
        _cue = FindObjectOfType<Cue>();
        _push = FindObjectOfType<Push>();
        _game_UI = FindObjectOfType<GameUI>();
        _trajectory_projection = FindObjectOfType<TrajectoryProjection>();
        _movement = gameObject.AddComponent(typeof(Movement)) as Movement;


    }
   
    public void SetActiveInput(bool state)
    {
        gameObject.SetActive(state);
        _trajectory_projection.gameObject.SetActive(state);
    }
    private void Update()
    {
        _trajectory_projection.DrawLine(true);
        if (Input.GetMouseButtonDown(0))
        {
            _rotate.ResetXZAngle();

            if (CheckMouseOnBall() && _move_ball)
                _i_movement = _move;
            else
                _i_movement = _rotate;

            BallsSettings.Settings.RigidBodysIsKinematic(true);
            _left_mouse_button_down = true;
            _trail_renderer.SetActive(false);
            StartCoroutine(_coroutine());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            _left_mouse_button_down = false;
            _trail_renderer.SetActive(true);
            BallsSettings.Settings.RigidBodysIsKinematic(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_push.StrengthValue != 0 && !_left_mouse_button_down)
            {
                _rotate.ResetXZAngle();
                _push.Strike();
                _cue.SetActive(false);

            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (_push.StrengthValue != 0 && !_left_mouse_button_down)
            {    
                _push.StrengthValue = 0;
                _game_UI.SetStrenghtBarValue(_push.StrengthValue);
                BallsSettings.Settings.CheckBallsStop();
                _move_ball = false;
                _move.SnapZAxis(false);
                _game_UI.MoveIconIsActive(false);
                SetActiveInput(false);
            }
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            _push.StrengthValue += Input.mouseScrollDelta.y * _scroll_sensitivity;
            _game_UI.SetStrenghtBarValue(_push.StrengthValue);
            _cue.SetCuePosition();
        }

        

    }

    public void MoveBallAllowed()
    {
        _move_ball = true;
        _game_UI.MoveIconIsActive(true);
    }


    public bool CheckMouseOnBall()
    {
        int layer_mask = LayerMask.NameToLayer("WhiteBall");
        _move.gameObject.layer = layer_mask;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {
            _move.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            return true;

        }
        return false;
    }
    private IEnumerator _coroutine()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _hit = hit;
                _movement.ToPoint(_i_movement, hit);
            }
            yield return null;

        }
    }

}
