using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject[] Points;
    public static CheckPointController Instance;

    private int targetPointIndex;
    public int TargetPointIndex
    {
        get { return (targetPointIndex); }
    }

    private int NumberOfPoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        targetPointIndex = 0;
        NumberOfPoints = Points.Length;
    }

    public void OnPointChecked(Target target)
    {
        if (TargetPointIndex == NumberOfPoints - 1)
        {
            Mission.Instance.OnMissionDone();
        }

        target.OnTargetChecked(TargetPointIndex);
        targetPointIndex++;
    }
}