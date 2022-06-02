
using UnityEngine;


public class Movement : MonoBehaviour
{

    public void ToPoint(IMovement Movement,RaycastHit hit)
    {
        if (Movement != null)
        {
            Movement.ToPoint(hit);
        }
    }

}
