
using UnityEngine;

public class Holes : MonoBehaviour
{
    private GameActivity _game_activity;
    private float _destroy_delay = 1f;
    private void Start()
    {
        _game_activity = FindObjectOfType<GameActivity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Balls>() != null)
        {
            BallType type;
           type= other.GetComponent<Balls>().GetBallType();

            other.transform.position=transform.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
      
            

            if (type==BallType.White)
            {
                _game_activity.ScoredWhiteBall();
               
            }
            else if(type == BallType.Black)
            {
                _game_activity.ScoredBlackBall();
                Destroy(other.gameObject, _destroy_delay);
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            }
            else
            {
                _game_activity.SetPlayersTypes(type);
                if(type == BallType.Strip)
                {
                    _game_activity.ScoredStripBall();
                }
                else
                {
                    _game_activity.ScoredFullBall();
                }
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Destroy(other.gameObject,_destroy_delay);


            }
        }
    }
   
}
