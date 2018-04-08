using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public GameObject[] Points;
    public static CheckPointController Instance;

    public int PreviousPointIndex, TargetPointIndex;

    private int NumberOfPoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        TargetPointIndex = 0;
        NumberOfPoints = Points.Length;
    }

    public void OnPointChecked()
    {
        if (TargetPointIndex == NumberOfPoints - 1)
        {
            print("You won!");
            Destroy(Points[PreviousPointIndex]);
            return;
        }
        PreviousPointIndex = TargetPointIndex;
        TargetPointIndex++;

        Destroy(Points[PreviousPointIndex]);
    }
}