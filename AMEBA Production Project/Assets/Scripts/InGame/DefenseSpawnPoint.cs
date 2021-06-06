using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSpawnPoint : MonoBehaviour
{
    DefenseSelectionManager selectionManager;

    public delegate void OnDefenseSpawnned();
    public static event OnDefenseSpawnned DefenseSpawnned;

    public Transform defenseHolder;

    private void Awake()
    {
        selectionManager = DefenseSelectionManager.GetInstance();
    }

    private void OnMouseDown()
    {
        Instantiate(selectionManager.selectedButton.defensePrefab, transform.position, Quaternion.identity, defenseHolder);
        DefenseSpawnned();
    }
}
