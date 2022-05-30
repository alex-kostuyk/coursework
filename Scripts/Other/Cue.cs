
using UnityEngine;

public class Cue : MonoBehaviour
{
   
    [SerializeField]
    private Transform _min,_max,_global, _static,_cue_mesh;
    [SerializeField]
    private float _smooth_speed=10;
    private Transform _white_ball, _target;
    private Push _push;

    void Start()
    {
        _white_ball = FindObjectOfType<Move>().transform;
        _push = FindObjectOfType<Push>();
        _target = _white_ball;
    }

  
    public void SetCuePosition()
    {
        _cue_mesh.position = Vector3.Lerp(_min.position,_max.position,_push.StrengthValue/_push.MaxPushStrenght);
    }
 
    public void SetActive(bool State)
    {
        if (State)
        {
            _target = _white_ball;
            SetCuePosition();
        }
        else
            _target = _static;
    }

    private void LateUpdate()
    {     
                _global.position = Vector3.Lerp(_global.position, _target.position, _smooth_speed * Time.fixedDeltaTime);
                _global.rotation = Quaternion.Lerp(_global.rotation, _target.rotation, _smooth_speed * Time.fixedDeltaTime);
    }
}
