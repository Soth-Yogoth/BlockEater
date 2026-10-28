using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private TMP_SpriteAsset spriteAsset;
    
    [SerializeField] private RectTransform PlayerInfoContainer;
    private Dictionary<Player, TMP_Text> playerInfos = new Dictionary<Player, TMP_Text>();
    
    private UIController uiController;
    
    void Start()
    {
        uiController = GetComponent<UIController>();
    }
    
    public void ShowVictory()
    {
        uiController.ShowVictoryWindow();
    }

    public void ShowDefeat()
    {
        uiController.ShowDefeatWindow();
    }
    
    // public void AddPlayerInfo(Player player, Color color)
    // {
    //     player.OnStateChanged += PlayerStateListener;
    //     
    //     GameObject playerInfo = new GameObject("PlayerInfo", 
    //         typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
    //     playerInfo.GetComponent<RectTransform>().SetParent(PlayerInfoContainer, false);
    //     
    //     TMP_Text textMesh = playerInfo.GetComponent<TMP_Text>();
    //     
    //     textMesh.fontSize = 18;
    //     textMesh.text = player.ToString();
    //     textMesh.spriteAsset = spriteAsset;
    //     textMesh.color = color;
    //     
    //     playerInfos.Add(player, textMesh);
    // }
    //
    // private void PlayerStateListener(Player player)
    // {
    //     playerInfos.TryGetValue(player, out TMP_Text textMesh);
    //     textMesh.text = player.ToString();
    // }
    
    public void ResizeGridCell(int GridSize)
    {
        float containerWidth = gridLayout.GetComponent<RectTransform>().rect.width;
        gridLayout.cellSize = new Vector2(containerWidth / GridSize, containerWidth / GridSize);
    }

    public void RenderGrid(Block[,] grid)
    {
        foreach (Transform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = grid.GetLength(1) - 1; i >= 0; i--)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                Block block = grid[j, i]; 
                if (block is null) DrawBlock(gridLayout.transform);
                else DrawBlock(gridLayout.transform, block.Sprite);
            }
        }
    }

    private void DrawBlock(Transform container, Sprite sprite)
    {
        var block = new GameObject("Block", 
            typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        
        block.GetComponent<Image>().sprite  = sprite;
        block.transform.SetParent(container, false);
    }
    
    private void DrawBlock(Transform container)
    {
        var block = new GameObject("Block", 
            typeof(RectTransform), typeof(CanvasRenderer));
        
        block.transform.SetParent(container, false);
    }
}
