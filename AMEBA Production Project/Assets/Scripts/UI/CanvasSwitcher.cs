using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvasType;

    CanvasManager canvasManager;
    Button menuButton;

    GameManager gm;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.GetInstance();
        gm = GameManager.GetInstance();
    }

    void OnButtonClicked()
    {
        switch (desiredCanvasType)
        {
            case CanvasType.WorldMapMenu:
                if (gm.environmentInGame.activeInHierarchy)
                    gm.EndedInGame();

                canvasManager.SwitchCanvas(desiredCanvasType);
                break;
            default:
                canvasManager.SwitchCanvas(desiredCanvasType);
                break;
        }
    }
}
