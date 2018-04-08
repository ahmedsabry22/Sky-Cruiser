using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAction : MonoBehaviour
{
    [Multiline]
    public string Message;              // Message to show to the player when collision happens.
    public int actionIndex;

    public bool Done
    {
        get { return (Done); }
        set
        {
            if (value)
            {
                CollisionController.Instance.OnCollionEvent();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Aircraft")
        {
            Debug.Log(Message);
            Done = true;
        }
    }
}