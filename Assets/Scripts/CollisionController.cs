using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour
{
    public GameObject[] Colliders;
    public static CollisionController Instance;

    public int PreviousColliderIndex, TargetColliderIndex;

    private int NumberOfColliders;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        TargetColliderIndex = 0;
        NumberOfColliders = Colliders.Length;
    }

    public void OnCollionEvent()
    {
        if (TargetColliderIndex == NumberOfColliders - 1)
        {
            print("You won!");
            Destroy(Colliders[PreviousColliderIndex]);
            return;
        }
        PreviousColliderIndex = TargetColliderIndex;
        TargetColliderIndex++;

        Destroy(Colliders[TargetColliderIndex]);
    }
}