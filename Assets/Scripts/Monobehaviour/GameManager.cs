using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    public void PauseGame()
    {
        IsPaused = true;
    }

    public void ResumeGame()
    {
        IsPaused = false;
    }
}
