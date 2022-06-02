
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Push : MonoBehaviour
{
 
    private float  _max_push_strenght = 13f, _min_push_strenght=0;
    private float _strength;
    private Rigidbody _rigidbody;
    public float MaxPushStrenght
    {
        get 
        { 
            return _max_push_strenght;
        }
        set 
        {
            if(_max_push_strenght>_min_push_strenght)
            _max_push_strenght = value;
        }
    }
    public float MinPushStrenght
    {
        get
        {
            return _min_push_strenght;
        }
        set
        {
            if (_min_push_strenght>0)
                _min_push_strenght = value;

        }
    }
   
    public float StrengthValue
    {
        get
        {
            return _strength;
        }
        set
        {
            if (value >= _min_push_strenght && value <= _max_push_strenght)
                _strength = value;

        }
    }
    private void Start()
    {
        _rigidbody= GetComponent<Rigidbody>();

    }

   
    public void Strike()
    {   
        _rigidbody.AddForce(transform.forward * _strength, ForceMode.Impulse);
    }

}
