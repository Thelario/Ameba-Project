using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;

public class DefenseButton : MonoBehaviour
{
    public Sprite defenseSprite;
    public bool available;
    public GameObject selectionFrame;
    public GameObject defensePrefab;
    public Slider timerSlider;
    private bool hasBeenUsed = false;
    public float timeAfterBeenUsed = 2f;
    private float timeAfterBeenUsedCounter;

    private Image image;
    private DefenseSelectionManager selectionManager;

    public delegate void OnDefenseButtonSelected(bool activated);
    public static event OnDefenseButtonSelected DefenseButtonSelected;

    private void Awake()
    {
        image = GetComponent<Image>();
        selectionManager = DefenseSelectionManager.GetInstance();
        DefenseSpawnPoint.DefenseSpawnned += RestartCounter;
        DefenseButtonSelected += ConfigureFrame;
        GameManager.LevelExited += DeactivateFrame;
    }

    private void Start()
    {
        timeAfterBeenUsedCounter = timeAfterBeenUsed;
        timerSlider.maxValue = timeAfterBeenUsed;
        timerSlider.minValue = 0f;

        selectionFrame.SetActive(false);

        if (available)
            image.sprite = defenseSprite;
        else
            image.sprite = Assets.i.lockedDefenseSprite;
    }

    private void Update()
    {
        if (hasBeenUsed)
        {
            timeAfterBeenUsedCounter -= Time.deltaTime;
            timerSlider.value = timeAfterBeenUsedCounter;

            if (timeAfterBeenUsedCounter <= 0f)
            {
                timeAfterBeenUsedCounter = timeAfterBeenUsed;
                timerSlider.value = timeAfterBeenUsedCounter;
                timerSlider.gameObject.SetActive(false);
                hasBeenUsed = false;
            }
        }
    }

    public void ChooseDefense()
    {
        if (available)
        {
            selectionFrame.SetActive(true);
            selectionManager.selectedButton = this;
            DefenseButtonSelected(true);
        }
    }

    public void RestartCounter()
    {
        if (available)
        {
            selectionFrame.SetActive(false);
            hasBeenUsed = true;
            timerSlider.gameObject.SetActive(true);
        }
    }

    public void DeactivateFrame()
    {
        selectionFrame.SetActive(false);
    }

    public void ConfigureFrame(bool activated)
    {
        if (available && selectionManager.selectedButton != this)
        {
            selectionFrame.SetActive(!activated);
        }
    }

    public void Unlock()
    {
        available = true;
        image.sprite = defenseSprite;
    }

    private void OnDestroy()
    {
        DefenseSpawnPoint.DefenseSpawnned -= RestartCounter;
        DefenseButtonSelected -= ConfigureFrame;
        GameManager.LevelExited -= DeactivateFrame;
    }
}
