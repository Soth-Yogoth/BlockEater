using System;
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
    
    void Awake()
    {
        uiController = GetComponent<UIController>();
    }
    
    public void ShowVictory() => uiController.ShowVictoryWindow();
    public void ShowDefeat() => uiController.ShowDefeatWindow();
    
    public void AddPlayerInfo(Player player)
    {
        if (playerInfos.TryGetValue(player, out TMP_Text existingText))
        {
            existingText.text = player.ToString();
            return;
        }
        
        GameObject playerInfo = new GameObject("PlayerInfo", 
            typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        playerInfo.GetComponent<RectTransform>().SetParent(PlayerInfoContainer, false);
        
        TMP_Text textMesh = playerInfo.GetComponent<TMP_Text>();
        
        textMesh.fontSize = 18;
        textMesh.text = player.ToString();
        textMesh.spriteAsset = spriteAsset;
        textMesh.color = player.Color;
        
        player.OnStateChanged += PlayerStateListener;
        player.OnDissolve += RemovePlayerInfo;
        playerInfos.Add(player, textMesh);
    }

    public void RemovePlayerInfo(Player player)
    {
        if (player == null) return;
        player.OnStateChanged -= PlayerStateListener;
        player.OnDissolve -= RemovePlayerInfo;
        
        if (!playerInfos.TryGetValue(player, out TMP_Text textMesh)) return;
        
        playerInfos.Remove(player);
        if (textMesh) Destroy(textMesh.gameObject);
    }
    
    private void PlayerStateListener(Player player)
    {
        if (playerInfos.TryGetValue(player, out TMP_Text textMesh) && textMesh)
            textMesh.text = player.ToString();
    }
    
    public void ResizeGridCell(int GridSize)
    {
        float containerWidth = gridLayout.GetComponent<RectTransform>().rect.width;
        gridLayout.cellSize = new Vector2(containerWidth / GridSize, containerWidth / GridSize);
    }

    public void RenderGrid(Block[,] grid)
    {
        foreach (RectTransform child in gridLayout.transform)
        {
            Destroy(child.gameObject);
        }
    
        for (int i = grid.GetLength(1) - 1; i >= 0; i--)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                DrawBlock(gridLayout.transform, grid[j, i]);
            }
        }
    }
    
    private void DrawBlock(Transform container, Block block)
    {
        var blockView = new GameObject("Block", 
            typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        blockView.transform.SetParent(container, false);
        
        Image image = blockView.GetComponent<Image>();
        image.sprite = block.Sprite;
        image.color = block.Tint;
    }
}
