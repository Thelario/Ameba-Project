using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWorldMap : MonoBehaviour
{
    [Header("Fields")]
    public bool unlocked;
    public bool completed;
    public Image circleAroundImage;

    [Header("Colors")]
    public Color lockedColor;
    public Color unlockedColor;
    public Color completedColor;

    [Header("Parameters to pass to the level")]
    public float spawnTime;
    public List<GameObject> enemiesPrefabs = new List<GameObject>();
    public List<GameObject> defensesButtonsPrefabs = new List<GameObject>();
    public Sprite hintImage;

    [Header("Path of Circles")]
    public List<Image> pathCircles = new List<Image>(); // These are the circles before the current button, which means that are the path that takes to this button

    public delegate void OnButtonWorldMapPressed();
    public static OnButtonWorldMapPressed ButtonWorldMapPressed;

    private void OnEnable()
    {
        if (!unlocked)
        {
            circleAroundImage.color = lockedColor;
            SetPathColors(lockedColor);
        }
        else if (!completed)
        {
            circleAroundImage.color = unlockedColor;
            SetPathColors(unlockedColor);
        }
        else
        {
            circleAroundImage.color = completedColor;
            SetPathColors(completedColor);
        }
    }

    private void SetPathColors(Color c)
    {
        if (pathCircles.Count > 0)
        {
            foreach (Image i in pathCircles)
            {
                i.color = c;
            }
        }
    }

    public void ButtonPressed()
    {
        if (unlocked)
        {
            CanvasManager.GetInstance().SwitchCanvas(CanvasType.InGameMenu);
            GameManager.GetInstance().ConfigureGame(enemiesPrefabs, defensesButtonsPrefabs, spawnTime, hintImage, this);
        }
    }
}
