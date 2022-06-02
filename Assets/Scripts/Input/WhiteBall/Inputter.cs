
using UnityEngine;

public class Inputter : MonoBehaviour
{
   public void StartInput(IInput Input)
    {
         if(Input!=null)
        {
            Input.SetActiveInput(true);
        }
    }

    public void AllowMove(IInput Input)
    {
        if (Input != null)
        {
            Input.MoveBallAllowed();
        }
    }
    public void SetActiveInput(bool State)
    {
        gameObject.SetActive(State);

    }

}
