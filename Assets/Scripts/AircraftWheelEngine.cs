using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftWheelEngine : MonoBehaviour
{
    public WheelCollider[] wheels;

    public float brakeForce;

    public float initialSpeed;
    public float currentSpeed;
    public float verticalRotationSpeed, horzontalRotationSpeed;

    private float vertical, horizontal;
    private float speedAxis;
    private new Rigidbody rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = GetComponent<Aircraft>().currentSpeed;

        wheels[1].motorTorque = currentSpeed;
        wheels[2].motorTorque = currentSpeed;
    }
    private void Update()
    {
        vertical = InputController.GetAxis("Vertical");
        horizontal = InputController.GetAxis("Horizontal");
        speedAxis = InputController.GetAxis("Speed");
        speedAxis = Mathf.Clamp(speedAxis, 0, 1);

        Motor();
        Rotate();

        if (speedAxis > 0.2f && wheels[1].motorTorque > 1000)
        {
            TakeOff();
        }

        if (InputController.ButtonClicking("Brake"))
        {
            Brake();
        }

    }

    private void Brake()
    {
        foreach (WheelCollider wheel in wheels)
        {
            wheel.brakeTorque = brakeForce;
        }
    }

    private void Rotate()
    {
        wheels[0].steerAngle += -horizontal * 60 * Time.deltaTime;
        wheels[0].steerAngle = Mathf.Clamp(wheels[0].steerAngle, -60, 60);
    }

    private void TakeOff()
    {
        Quaternion rot = Quaternion.Euler(new Vector3(vertical * -45, 0, 0) * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * rot);
    }

    private void Motor()
    {
        currentSpeed += initialSpeed / 2 * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed / 3, initialSpeed) * speedAxis;

        wheels[1].brakeTorque = 0;
        wheels[2].brakeTorque = 0;

        wheels[0].motorTorque = currentSpeed * 100;
        wheels[1].motorTorque = currentSpeed * 100;
        wheels[2].motorTorque = currentSpeed * 100;
    }
}