using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Aircraft : MonoBehaviour
{
    public AircraftData aircraftData;

    public float initialSpeed;
    public float currentSpeed;
    public float verticalRotationSpeed, horzontalRotationSpeed;

    public GameObject ExplosionParticlePrefab;

    private float vertical, horizontal;
    private float speedAxis;


    private void Awake()
    {
        currentSpeed = initialSpeed;
    }

    private void Update()
    {
        vertical = InputController.GetAxis("Vertical");
        horizontal = InputController.GetAxis("Horizontal");
        speedAxis = InputController.GetAxis("Speed");
        speedAxis = Mathf.Clamp(speedAxis, 0.1f, 1);

        Move();
        Rotate();
    }

    private void Move()
    {
        currentSpeed -= transform.forward.y * initialSpeed * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed / 3, initialSpeed);

        transform.position += transform.forward * (speedAxis * currentSpeed) * Time.deltaTime;
    }
    private void Rotate()
    {
        transform.Rotate(new Vector3(-vertical * verticalRotationSpeed, horizontal * horzontalRotationSpeed, 0) * Time.deltaTime);
    }

    private void Brake()
    {
        currentSpeed -= (initialSpeed / 10) * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed / 10, initialSpeed);
    }

    private void Turbo()
    {
        currentSpeed += (initialSpeed * 20) * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed / 10, initialSpeed * 2.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            if (!GetComponent<AircraftWheels>().AircraftLanded)
            {
                Instantiate(ExplosionParticlePrefab, transform.position, Quaternion.identity);
                Audio.Instance.PlayClip("explosion");
                GameObject losePanel = Instantiate(Resources.Load<GameObject>("Prefabs/LosePanel"), GameObject.FindGameObjectWithTag("UICanvas").transform, false);
                losePanel.transform.Find("play_btn").GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<SceneController>().StartScene(1));
                losePanel.transform.Find("exit_btn").GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<SceneController>().StartScene(0));
                Destroy(gameObject);
            }
        }
    }
}