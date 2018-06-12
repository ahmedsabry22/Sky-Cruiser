using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftWheels : MonoBehaviour
{
    public GameObject Wheels;
    [HideInInspector] public int NumberOfWheelsOnLand = 0;

    private new Rigidbody rigidbody;

    private bool wheelsActive;
    private bool airEngineActive = true, wheelEngineActive = false;

    public bool AircraftLanded
    {
        get
        {
            if (NumberOfWheelsOnLand > 0)
            {
                if (airEngineActive)
                {
                    airEngineActive = false;
                    wheelEngineActive = true;
                    SwitchEngines();
                    EnableRigidbody(true);
                }
                return (true);
            }
            else
            {
                if (wheelEngineActive)
                {
                    airEngineActive = true;
                    wheelEngineActive = false;
                    SwitchEngines();
                    EnableRigidbody(false);
                }
                return (false);
            }
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (AircraftLanded) { }

        if (InputController.ButtonClicked("Wheels"))
        {
            ActivateWheels();
        }
    }

    private void ActivateWheels()
    {
        if (AircraftLanded)
        {
            return;
        }
        else
        {
            if (wheelsActive)
            {
                Wheels.SetActive(false);
            }
            else
            {
                Wheels.SetActive(true);
            }

            wheelsActive = !wheelsActive;
        }
    }

    private void SwitchEngines()
    {
        GetComponent<AircraftWheelEngine>().enabled = wheelEngineActive;
        GetComponent<Aircraft>().enabled = airEngineActive;
    }

    private void EnableRigidbody(bool state)
    {
        if (state)
        {
            rigidbody.useGravity = true;
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}