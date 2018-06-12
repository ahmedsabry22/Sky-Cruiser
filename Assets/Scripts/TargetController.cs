using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Image arrow;
    [SerializeField] private Text distanceText;

    public static TargetController Instance;

    public Target[] Targets;

    private Target nextTarget;

    private Aircraft aircraft;

    public Target NextTarget
    {
        get
        {
            return (nextTarget);
        }
        set
        {
            nextTarget = value;
        }
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        aircraft = FindObjectOfType<Aircraft>();
        Targets = FindObjectsOfType<Target>();

        Array.Reverse(Targets);

        NextTarget = Targets[0];
    }

    private void Update()
    {
        if (NextTarget != null)
        {
            Vector3 targetDirection = aircraft.transform.InverseTransformPoint(NextTarget.transform.position);
            float a = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

            arrow.transform.localEulerAngles = new Vector3(0, 180, a);

            float magnitude = Vector3.Distance(aircraft.transform.position, NextTarget.transform.position);
            distanceText.text = Convert.ToInt32(magnitude) + "m";
        }
        else
        {
            arrow.gameObject.SetActive(false);
            distanceText.gameObject.SetActive(false);
        }
    }
}