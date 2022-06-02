
using UnityEngine;

public class UIMoveIcon : MonoBehaviour
{
    private Transform _target;
    private Transform _transform;
    private Camera _camera;

    private void Start()
    {
        _transform = transform;
        _target = FindObjectOfType<Move>().transform;
        _camera = Camera.main;


    }
    private void LateUpdate()
    {
        _transform.position = _camera.WorldToScreenPoint(_target.position);


    }
}
