using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform uiContainer;
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private GameObject VictoryWindow;
    [SerializeField] private GameObject DefeatWindow;
    [SerializeField] private GameObject Guide;
    [SerializeField] private GameObject GameMenu;
    
    private GameObject victoryWindow;
    private GameObject defeatWindow;
    private GameObject guide;
    private GameObject gameMenu;
    
    private const string TutorialShownKey = "TutorialShown";

    private void Start()
    {
        bool tutorialWasShown = PlayerPrefs.GetInt(TutorialShownKey, 0) == 1;

        if (!tutorialWasShown)
        {
            GuideButton_onClick();
            PlayerPrefs.SetInt(TutorialShownKey, 1);
            PlayerPrefs.Save();
        }
    }

    public void ShowVictoryWindow()
    {
        gameManager.PauseGame();
        
        if (victoryWindow == null)
        {
            victoryWindow = Instantiate(VictoryWindow, uiContainer);
            victoryWindow.GetComponent<PanelHandler>().Initialize(gameManager);
        }
        else victoryWindow.SetActive(true);
    }
    
    public void ShowDefeatWindow()
    {
        gameManager.PauseGame();
        
        if (defeatWindow == null)
        {
            defeatWindow = Instantiate(DefeatWindow, uiContainer);
            defeatWindow.GetComponent<PanelHandler>().Initialize(gameManager);
        }
        else defeatWindow.SetActive(true);
    }
    
    public void GuideButton_onClick()
    {
        if(gameManager.IsPaused && (guide == null || !guide.activeSelf)) return;
        
        gameManager.PauseGame();
        
        if (guide == null)
        {
            guide = Instantiate(Guide, uiContainer);
            guide.GetComponent<PanelHandler>().Initialize(gameManager);
        }
        else if (guide.activeSelf)
        {
            gameManager.ResumeGame();
            guide.SetActive(false);
        }
        else guide.SetActive(true);
    }
    
    public void GameMenu_onClick()
    {
        if(gameManager.IsPaused && (gameMenu == null || !gameMenu.activeSelf)) return;
        
        gameManager.PauseGame();
        
        if (gameMenu == null)
        {
            gameMenu = Instantiate(GameMenu, uiContainer);
            gameMenu.GetComponent<PanelHandler>().Initialize(gameManager);
        }
        else if (gameMenu.activeSelf)
        {
            gameManager.ResumeGame();
            gameMenu.SetActive(false);
        }
        else gameMenu.SetActive(true);
    }
}
