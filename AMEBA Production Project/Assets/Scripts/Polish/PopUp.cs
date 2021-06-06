using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] private float popUpTime;
    [SerializeField] private float delayPopUpTime;

    public Vector3 scaleDestination;

    public bool animateOnStart = false;

    private void OnEnable()
    {
        gameObject.LeanScale(Vector3.zero, 0f).setIgnoreTimeScale(true);
        gameObject.LeanScale(scaleDestination, popUpTime).setDelay(delayPopUpTime).setIgnoreTimeScale(true);
    }

    private void Start()
    {
        if (animateOnStart)
        {
            gameObject.LeanScale(Vector3.zero, 0f).setIgnoreTimeScale(true);
            gameObject.LeanScale(scaleDestination, popUpTime).setDelay(delayPopUpTime).setIgnoreTimeScale(true);
        }
    }
}
