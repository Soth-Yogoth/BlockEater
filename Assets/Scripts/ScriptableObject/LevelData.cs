using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BlockRow
{
    public BlockData[] row;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField][Min(1)] private int moves;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private BlockRow[] gridContent;
    
    private BlockData emptyBlock => gameConfig.emptyBlock;
    
    public int GridSize
    {
        get => gridContent.Length;
        set => gridContent = GenerateGridContent(value);
    }

    public BlockData GetBlock(int x, int y)
    {
        Debug.Log("x: " + x + " y: " + y + ", block: " + gridContent[x].row[y]);
        bool isExist = IsInBounds(x, y) && gridContent[x].row[y];
        return isExist ? gridContent[x].row[y] : emptyBlock;
    }
    
    public void UpdateBlock(int x, int y, BlockData block)
    {
        if (IsInBounds(x, y)) gridContent[x].row[y] = block;
    }

    private BlockRow[] GenerateGridContent(int gridSize)
    {
        BlockRow[] newGridContent = new BlockRow[gridSize];
        
        for (int i = 0; i < gridSize; i++)
        {
            newGridContent[i].row = new BlockData[gridSize];
            
            for (int j = 0; j < gridSize; j++)
            {
                newGridContent[i].row[j] = IsInBounds(i, j) ? gridContent[i].row[j] : emptyBlock; 
            }
        }
        
        return newGridContent;
    }

    public LevelState CreateLevelState()
    {
        List<Player> players = new List<Player>();
        
        Gameboard gameboard = new Gameboard();
        gameboard.Fill(GetArray(gameboard, players));
            
        return new LevelState(gameboard, players, emptyBlock);
    }

    private Block[,] GetArray(Gameboard gameboard, List<Player> players)
    {
        int arraySize = gridContent.Length;
        
        Block[,] array = new Block[arraySize, arraySize];
        
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                Vector2Int position = new Vector2Int(i, j);
                Block newBlock = gridContent[i].row[j].CreateBlock(gameboard, position);
                
                if (newBlock is Player currentPlayer)
                {
                    currentPlayer.OnDissolve += (player) => players.Remove(player);
                    currentPlayer.MovesCount = moves;
                    players.Add(currentPlayer);
                }
                
                array[i, j] = newBlock;
            }
        }
        
        return array;
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < GridSize && y >= 0 && y < GridSize;
    }
}

