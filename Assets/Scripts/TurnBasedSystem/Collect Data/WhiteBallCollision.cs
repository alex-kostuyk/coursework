
using UnityEngine;

public class WhiteBallCollision : MonoBehaviour
{
    private GameActivity _game_activity;
    private void Start()
    {
        _game_activity = FindObjectOfType<GameActivity>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Balls>() != null)
        {
                BallType type;
                type = collision.gameObject.GetComponent<Balls>().GetBallType();
                
                if(type==BallType.Full)
                {
                    _game_activity.FullBallPushed();
                }
                else if(type == BallType.Strip)
                {
                    _game_activity.StripeBallPushed();
                }
                else if (type == BallType.Black)
                {
                    _game_activity.BlackBallPushed();
                }
        }
    }
}
