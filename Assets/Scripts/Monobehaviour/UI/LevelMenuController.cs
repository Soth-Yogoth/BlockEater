using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] private RectTransform levelContainer;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private List<SceneAsset> scenes;

    void Start()
    {
        int i = 1;

        foreach (SceneAsset sceneAsset in scenes)
        {
            GameObject newButton = Instantiate(buttonPrefab, levelContainer);
            
            if (newButton.TryGetComponent<RectTransform>(out RectTransform rectTransform) &&
                rectTransform.GetChild(0).TryGetComponent<TMP_Text>(out TMP_Text textMesh) &&
                newButton.TryGetComponent<Button>(out Button button))
            {
                textMesh.text = i++.ToString();
                button.onClick.AddListener(() => SceneManager.LoadScene(sceneAsset.name));
                
                continue;
            }
            
            Debug.LogError("LevelMenuController: Button prefab is missing some components");
            break;
        }
    }
}
