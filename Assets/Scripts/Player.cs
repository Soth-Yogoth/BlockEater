using System;
using UnityEngine;

public class Player
{
    private Vector2Int position;
    public Vector2Int Position 
    { 
        get  { return position; }
        set
        {
            position = value;
            if(OnStateChanged != null) OnStateChanged.Invoke(this);
        }
    }
    
    public int MovesCount { get; set; }
    public int ProtectionCount {get; set;}
    
    public event Action<Player> OnStateChanged;

    public Player(Vector2Int position, int movesCount)
    {
        Position = position;
        MovesCount = movesCount;
        ProtectionCount = 0;
    }

    public bool TryConsume(BlockData block)
    {
        if (block == null || block.BlockType == BlockType.empty) return true;
        
        Debug.Log("Try Consume " + block.BlockName);
        
        if (ProtectionCount > 0)
        {
            ProtectionCount--;
        }
        else if (block.Cost <= MovesCount)
        {
            MovesCount -= block.Cost;
        }
        else return false;
        return true;
    }

    public override string ToString()
    {
        string s = "Осталось места в желудке: " + MovesCount + ",\n<sprite=0>: " + ProtectionCount;
        return s;
    }
}
