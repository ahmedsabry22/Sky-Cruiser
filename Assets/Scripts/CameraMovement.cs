using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform aircraft;
    public float forwardFactor, upFactor;
    public float followTime;
    public float bias;

    private Vector3 currentVelocity;
    void Awake()
    {
        aircraft = FindObjectOfType<Aircraft>().transform;
    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 desiredPosition = (aircraft.position - aircraft.forward * forwardFactor) + (aircraft.up * upFactor);
        transform.position = Vector3.SmoothDamp(transform.position, transform.position * bias + desiredPosition * (1 - bias), ref currentVelocity, followTime);
        transform.LookAt(aircraft.position + transform.forward * forwardFactor);
    }
}
