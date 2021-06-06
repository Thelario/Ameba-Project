using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button buttonRef;

    private void Awake()
    {
        buttonRef = GetComponent<Button>();

        buttonRef.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick, transform.position, Settings.GetInstance().sfxVolume);
    }

    private void OnMouseEnter()
    {
        SoundManager.PlaySound(SoundManager.Sound.MouseOverButton, transform.position, Settings.GetInstance().sfxVolume);
    }
}
