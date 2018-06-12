using UnityEngine;

public class Target : MonoBehaviour
{
    public void OnTargetChecked(int index)
    {
        if (index < TargetController.Instance.Targets.Length - 1)
            TargetController.Instance.NextTarget = TargetController.Instance.Targets[index + 1];

        else
            TargetController.Instance.NextTarget = null;
    }
}