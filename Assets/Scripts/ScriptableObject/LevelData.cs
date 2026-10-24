using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct BlockRow
{
    public BlockData[] Row;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int moves = 1;
    [SerializeField] private BlockData emptyBlock;
    [SerializeField] private BlockRow[] _gridContent;
    
    public int GridSize
    {
        get => _gridContent.Length;
        set => _gridContent = GenerateGridContent(value);
    }

    public BlockData GetBlock(int x, int y)
    {
        if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
        {
            return _gridContent[x].Row[y] ? _gridContent[x].Row[y] : emptyBlock;
        }
        else return emptyBlock;
    }
    
    public void UpdateBlock(int x, int y, BlockData block)
    {
        if (x < 0 || x >= GridSize || y < 0 || y >= GridSize) return;
        
        _gridContent[x].Row[y] = block;
    }

    private BlockRow[] GenerateGridContent(int gridSize)
    {
        BlockRow[] gridContent = new BlockRow[gridSize];
        
        for (int i = 0; i < gridSize; i++)
        {
            gridContent[i].Row = new BlockData[gridSize];
            
            for (int j = 0; j < gridSize; j++)
            {
                gridContent[i].Row[j] = (i < _gridContent.Length && j < _gridContent[i].Row.Length) ? 
                    _gridContent[i].Row[j] : emptyBlock;
            }
        }
        
        return gridContent;
    }

    public LevelState CreateLevelState()
    {
        BlockData[,] grid = GetArray(_gridContent);
            
        return new LevelState(grid, emptyBlock, moves);
    }

    private BlockData[,] GetArray(BlockRow[] rows)
    {
        int arraySize = rows.Length;
        
        BlockData[,] array = new BlockData[arraySize, arraySize];
        
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 0; j < arraySize; j++)
            {
                array[i, j] = _gridContent[i].Row[j];
            }
        }

        return array;
    }
}

