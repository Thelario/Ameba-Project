using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSelectionManager : Singleton<DefenseSelectionManager>
{
    [HideInInspector] public DefenseButton selectedButton;

    public Transform leftHud;

    public List<GameObject> defenseSpawnPoints = new List<GameObject>();
    public GameObject unavailableDefenseButton;

    public Transform defenseHolder;

    private GameManager gm;

    protected override void Awake()
    {
        base.Awake();

        gm = GameManager.GetInstance();

        DefenseSpawnPoint.DefenseSpawnned += MakeNullSelectedButton;
        GameManager.LevelExited += DestroyDefenses;
        DefenseButton.DefenseButtonSelected += ConfigureDefensesSpawnPoints;
        GameManager.LevelStart += StartGame;
    }

    public void ConfigureDefensesSpawnPoints(bool activate)
    {
        foreach (GameObject go in defenseSpawnPoints)
        {
            go.SetActive(activate);
        }
    }

    // Method for configuring the defenses of the current level
    public void StartGame()
    {
        ConfigureDefensesSpawnPoints(false);
        
        foreach(GameObject go in gm.defensesButtonsPrefabs)
        {
            Instantiate(go, leftHud);
        }

        for (int i = 0; i < 6 - gm.defensesButtonsPrefabs.Count; i++)
        {
            Instantiate(unavailableDefenseButton, leftHud);
        }
    }

    // Whenever a defense is spawnned, it is to make null the references, so that we cannot spawn more defenses of the selected type, 
    // and we need to deactivate the defense spawn points.
    public void MakeNullSelectedButton()
    {
        selectedButton = null;

        ConfigureDefensesSpawnPoints(false);
    }

    // Method used for unlocking next defense button, allowing players to use more defenses
    public void UnlockNextDefense()
    {
        foreach (GameObject g in gm.defensesButtonsPrefabs)
        {
            DefenseButton db = g.GetComponent<DefenseButton>();

            if (db.available == false)
            {
                db.Unlock();
                break;
            }
        }
    }

    // Method for destroying the existing defenses
    public void DestroyDefenses()
    {
        ConfigureDefensesSpawnPoints(false);

        foreach (Transform t in defenseHolder)
        {
            Destroy(t.gameObject);
        }

        foreach (Transform t in leftHud)
        {
            Destroy(t.gameObject);
        }
    }
}
