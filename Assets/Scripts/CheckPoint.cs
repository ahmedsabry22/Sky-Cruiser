using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int CheckPointIndexInMission;

    private Target target;

    private bool collided = false;
    public bool Done
    {
        get { return (Done); }
        set
        {
            if (value)
            {
                CheckPointController.Instance.OnPointChecked(target);
            }
        }
    }

    private void Awake()
    {
        target = GetComponent<Target>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Aircraft")
        {
            print(CheckPointIndexInMission + "  " + CheckPointController.Instance.TargetPointIndex);
            if (CheckPointIndexInMission == CheckPointController.Instance.TargetPointIndex)
            {
                if (!collided)
                {
                    collided = true;
                    Done = true;
                    Destroy(gameObject);
                }
            }

            else
                Popup.Instance.Show("missed a check point");
        }
    }
}