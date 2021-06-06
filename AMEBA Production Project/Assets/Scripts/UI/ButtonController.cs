using UnityEngine;
using UnityEngine.UI;


public enum ButtonType
{
    MAP_BUTTON,
    BILLS_BUTTON,
    HUB_BUTTON
}

/// <summary>
/// This class is another method for working with Buttons. The standard class I will be using is CanvasSwitcher,
/// which is very similar to this class, but this class allows me to do more things before chaning the canvas.
/// With this apporack, I will be able to do more stuff when the button is called, which can be useful many times.
/// Whoever, this class doesn't follow the Single Responsability Principle (SRP) of Object Oriented Programming, but, who cares?
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public ButtonType buttonType;

    CanvasManager canvasManager;
    Button menuButton;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.GetInstance();
    }

    void OnButtonClicked()
    {
        switch(buttonType)
        {
            case ButtonType.MAP_BUTTON:
                // Call some code before going to map
                //canvasManager.SwitchCanvas(CanvasType.MapMenu);
                break;
            case ButtonType.BILLS_BUTTON:
                // Call some code before going to bills
                //canvasManager.SwitchCanvas(CanvasType.BillsMenu);
                break;
            case ButtonType.HUB_BUTTON:
                // Call some code before going to hub
                //canvasManager.SwitchCanvas(CanvasType.HubMenu);
                break;
        }
    }
}
