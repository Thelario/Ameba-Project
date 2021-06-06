using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsUnlocker : MonoBehaviour
{
    public Transform body;

    private List<ButtonWorldMap> worldMapButtons = new List<ButtonWorldMap>();

    int completedLevelsAmount = 0;

    private void Awake()
    {
        StatsBattleManager.WonGame += WonGame;
    }

    void Start()
    {
        foreach(Transform t in body)
        {
            if (t.TryGetComponent(out ButtonWorldMap b))
                worldMapButtons.Add(b);
        }

        ReadData();
    }

    public void ReadData()
    {
        //PlayerPrefs.DeleteKey("CompletedLevelsAmount");
        completedLevelsAmount = PlayerPrefs.GetInt("CompletedLevelsAmount", 0);

        for (int i = 0; i < completedLevelsAmount; i++)
        {
            worldMapButtons[i].unlocked = true;
            worldMapButtons[i].completed = true;
        }

        worldMapButtons[completedLevelsAmount].unlocked = true;
    }

    public void SaveData()
    {
        int n = 0;

        foreach (ButtonWorldMap b in worldMapButtons)
        {
            if (b.completed)
                n++;
        }

        //Debug.Log("Levels Completed: " + n);
        PlayerPrefs.SetInt("CompletedLevelsAmount", n);
    }

    public void WonGame()
    {
        CompleteLevel(GameManager.GetInstance().b);
    }

    public void UnlockLevel(ButtonWorldMap b)
    {
        foreach (ButtonWorldMap bwp in worldMapButtons)
        {
            if (b.Equals(bwp))
                b.unlocked = true;
        }

        SaveData();
    }

    public void CompleteLevel(ButtonWorldMap b)
    {
        int n = 0;

        foreach (ButtonWorldMap bwp in worldMapButtons)
        {
            n++;

            if (b.Equals(bwp))
            {
                b.completed = true;
                UnlockLevel(worldMapButtons[n]);
                break;
            }
        }
    }
}
