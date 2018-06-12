using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private Rigidbody aircraftRigidbody;
    private AircraftWheels aircraftWheels;
    private WheelCollider wheelCollider;
    private bool gotGrounded = false;

    private void Awake()
    {
        aircraftWheels = FindObjectOfType<AircraftWheels>();
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void Update()
    {
        CheckWheelGrounded();
    }

    private void CheckWheelGrounded()
    {
        if (wheelCollider.isGrounded)
        {
            if (!gotGrounded)
            {
                //aircraftRigidbody.useGravity = true;
                //aircraftRigidbody.constraints = RigidbodyConstraints.None;
                aircraftWheels.NumberOfWheelsOnLand++;
                gotGrounded = true;
            }
        }
        else
        {
            if (gotGrounded)
            {
                //aircraftRigidbody.useGravity = false;
                //aircraftRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                aircraftWheels.NumberOfWheelsOnLand--;
                gotGrounded = false;
            }
        }
    }
}