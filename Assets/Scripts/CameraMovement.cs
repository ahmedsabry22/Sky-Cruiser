using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraForwardFactor, cameraUpFactor;
    public float cameraFollowTime;
    public float cameraBias;

    private GameObject aircraft;
    private Vector3 currentVelocity;

    private void Awake()
    {
    }
    private void Start()
    {
        aircraft = FindObjectOfType<Aircraft>().gameObject;
        StartCoroutine(FollowAircraft());
    }

    private IEnumerator FollowAircraft()
    {
        while (true)
        {
            Vector3 desiredPosition = (aircraft.transform.position - aircraft.transform.forward * cameraForwardFactor) + (aircraft.transform.up * cameraUpFactor);
            transform.position = Vector3.SmoothDamp(transform.position, aircraft.transform.position * cameraBias + desiredPosition * (1 - cameraBias), ref currentVelocity, cameraFollowTime);

            transform.LookAt(aircraft.transform.position + aircraft.transform.forward * cameraForwardFactor);

            yield return null;
        }
    }
}
