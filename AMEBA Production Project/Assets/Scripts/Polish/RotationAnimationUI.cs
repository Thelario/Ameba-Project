using UnityEngine;

public class RotationAnimationUI : MonoBehaviour
{
    [SerializeField] private float rotationTime;
    [SerializeField] private float delayedRotationTime;

    private void OnEnable()
    {
        StartRotation();
    }

    private void StartRotation()
    {
        gameObject.LeanRotateZ(180f, rotationTime).setOnComplete(FinnishRotation).setDelay(delayedRotationTime);
    }

    private void FinnishRotation()
    {
        gameObject.LeanRotateZ(360f, rotationTime).setDelay(delayedRotationTime);
    }
}
