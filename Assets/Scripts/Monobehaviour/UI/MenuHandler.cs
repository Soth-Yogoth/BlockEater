using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
