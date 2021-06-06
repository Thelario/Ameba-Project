using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public string link;
    
    public void LoadLink()
    {
        Application.OpenURL(link);
    }
}
