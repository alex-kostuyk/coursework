using System.Collections;

using UnityEngine;

public class CameraSwitchMod : MonoBehaviour
{
    [SerializeField]
    private Transform []_statics_point;
    private Transform _main_camera, _target_point;
    [SerializeField]
    private float _smooth_speed = 10f;
    private IEnumerator change_position;
    private int _index;
    private int _point_index
    {
        get
        {
            return _index;
        }
        set
        {
            if (value >= _statics_point.Length || value < 0)
                _index = 0;
            else
                _index = value;

        }
    }

    private void Start()
    {
        _main_camera = Camera.main.transform;
        _target_point = _statics_point[_point_index];
        change_position = _coroutine(_target_point);
        StartCoroutine(change_position);
    }
    public void SwitchMod()
    {
        StopCoroutine(change_position);
        _point_index++;
        _target_point = _statics_point[_point_index];

        change_position = _coroutine(_target_point);
        StartCoroutine(change_position);

    }
   
    private IEnumerator _coroutine(Transform _target)
    {
        while (true)
        {
            
            if (_target.position!=_main_camera.position)
            {
                _main_camera.position = Vector3.Lerp(_main_camera.position, _target.position, _smooth_speed * Time.fixedDeltaTime);
                _main_camera.rotation = Quaternion.Lerp(_main_camera.rotation, _target.rotation, _smooth_speed * Time.fixedDeltaTime);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(Time.deltaTime);

        }
    }
}
