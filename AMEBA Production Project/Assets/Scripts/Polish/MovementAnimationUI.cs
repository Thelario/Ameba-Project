using UnityEngine;

public class MovementAnimationUI : MonoBehaviour
{
    [SerializeField] private Vector2 initialPos;
    [SerializeField] private Vector2 secondaryPos;
    [SerializeField] private float secondaryPosTime;
    [SerializeField] private Vector2 finalPos;
    [SerializeField] private float finalPosTime;

    private void OnEnable()
    {
        FirstMove();
    }

    private void FirstMove()
    {
        gameObject.LeanMove(initialPos, 0f);
        gameObject.LeanMove(secondaryPos, secondaryPosTime).setOnComplete(SecondMove);
    }
    
    private void SecondMove()
    {
        gameObject.LeanMove(finalPos, finalPosTime);
    }
}
