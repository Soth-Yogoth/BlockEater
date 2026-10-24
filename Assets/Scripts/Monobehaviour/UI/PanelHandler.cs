using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    private GameManager gm;

    public void Initialize(GameManager gm)
    {
        this.gm = gm;
    }

    public void ClosePanel()
    {
        gm.ResumeGame();
        gameObject.SetActive(false);
    }
}
