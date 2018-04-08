using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int MissionNumber;
    public int CheckPointIndexInMission;

    public bool Done
    {
        get { return (Done); }
        set
        {
            if (value)
            {
                CheckPointController.Instance.OnPointChecked();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Aircraft")
        {
            print("collided with aircraft");
            if (CheckPointIndexInMission == CheckPointController.Instance.TargetPointIndex)
                Done = true;
            else
                Debug.LogWarning("missed a check point");
        }
    }
}