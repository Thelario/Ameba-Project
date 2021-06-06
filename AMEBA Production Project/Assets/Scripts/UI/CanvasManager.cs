using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType
{
    HubMenu,
    WorldMapMenu,
    InGameMenu,
    PauseMenu,
    EndGameMenu,
    VictoryMenu,
    SettingsMenu,
    AboutMenu
}



/// <summary>
/// This is the class that will control the child objects and allow us to open and close each child
/// </summary>
public class CanvasManager : Singleton<CanvasManager>
{
    List<CanvasController> canvasControllerList;
    public CanvasController lastActiveCanvas;

    protected override void Awake()
    {
        base.Awake();

        // This next line of code SHOULDN'T BE HERE, IT SHOULD GO ON A GAME HANDLER OS SMTH, BUT FOR THE MOMENT I AM LEAVING IT HERE
        //SoundManager.Iinitialize();

        // For this to work, all menus in the game need to be active in the begginning. Make sure tu TURN ON THE MENUS.
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        // The operation of transforming an array into a list using linq, as in the previous line of code, is a huge
        // costly operation, but as we are only going to do it once at the beginning of the game and for tiny operations
        // like this is not that horrible to do it.

        // This line iterates all the menus and deactivates them, using linq.
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.HubMenu);
    }

    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveCanvas = desiredCanvas;
        }
        else { /* Debug.LogWarning("The desired menu canvas was not found!"); */ }
    }
}
