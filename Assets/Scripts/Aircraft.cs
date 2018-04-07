using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Aircraft : MonoBehaviour
{
    public float initialSpeed;
    public float currentSpeed;
    public float verticalRotationSpeed, horzontalRotationSpeed;

    public float cameraForwardFactor, cameraUpFactor;
    public float cameraFollowTime;
    public float cameraBias;

    private new GameObject camera;
    private float vertical, horizontal;
    private Vector3 currentVelocity;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>().gameObject;
        currentSpeed = initialSpeed;
    }
    private void Update()
    {

#if UNITY_EDITOR
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

#elif UNITY_ANDROID
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        //vertical = CrossPlatformInputManager.GetAxis("Vertical");
        //horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
#endif

        Move();
        Rotate();
        MoveCamera();

    }

    private void Move()
    {
        currentSpeed -= transform.forward.y * Time.deltaTime * currentSpeed;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed / 3, initialSpeed * 3);

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(-vertical * verticalRotationSpeed, 0, -horizontal * horzontalRotationSpeed) * Time.deltaTime);
    }

    private void MoveCamera()
    {
        Vector3 desiredPosition = (transform.position - transform.forward * cameraForwardFactor) + (transform.up * cameraUpFactor);
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, camera.transform.position * cameraBias + desiredPosition * (1 - cameraBias), ref currentVelocity, cameraFollowTime);
        camera.transform.LookAt(transform.position + transform.forward * cameraForwardFactor);
    }
}