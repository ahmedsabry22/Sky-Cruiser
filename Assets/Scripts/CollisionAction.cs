using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAction : MonoBehaviour
{
    [Multiline]
    public string Message;              // Message to show to the player when collision happens.
    public int actionIndex;

    private Target target;

    public bool Done
    {
        get { return (Done); }
        set
        {
            if (value)
            {
                CollisionController.Instance.OnCollionEvent(this);
            }
        }
    }

    private void Awake()
    {
        target = GetComponent<Target>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Aircraft")
        {
            target.OnTargetChecked(actionIndex);
            Done = true;
        }
    }
}