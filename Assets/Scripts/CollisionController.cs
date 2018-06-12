using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour
{
    public GameObject[] Colliders;
    public static CollisionController Instance;

    private int TargetColliderIndex;

    private int NumberOfColliders;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        TargetColliderIndex = 0;
        NumberOfColliders = Colliders.Length;
    }

    public void OnCollionEvent(CollisionAction collisionAction)
    {
        if (TargetColliderIndex == NumberOfColliders - 1)
        {
            if (FindObjectOfType<AircraftWheels>().AircraftLanded)
            {
                Mission.Instance.OnMissionDone();
            }
            return;
        }
        TargetColliderIndex++;
    }
}