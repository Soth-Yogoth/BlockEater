using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Block
{
    public static List<Player> Players = new List<Player>();
    
    private Stack<Vector2Int> positionHistory = new Stack<Vector2Int>();
    private Vector2Int currentPosition;
    
    private Gameboard _gameboard;
    
    public int MovesCount { get; set; }
    public int ProtectionCount { get; set; } = 0;
    
    public event Action<Player> OnStateChanged;

    public Color Color { get; }
    

    public Player(Gameboard gameboard, Vector2Int position, Sprite sprite, int value, int movesCount, Color color) : base(position, sprite, value)
    {
        _gameboard = gameboard;
        Position = position;
        MovesCount = movesCount;
        Color = color;
        
        Players.Add(this);
    }

    public bool TryMove(Vector2Int direction)
    {
        Vector2Int newPosition = currentPosition + direction;
        if (_gameboard.Blocks.IsInBounds(newPosition.x, newPosition.y) && TryConsume(_gameboard.Blocks[newPosition.x, newPosition.y]))
        {
            positionHistory.Push(currentPosition);
            currentPosition = newPosition;
            return true;
        }
        
        return false;
    }

    public bool TryConsume(Block block)
    {
        if (block is EmptyBlock) ;
        else if (ProtectionCount > 0) ProtectionCount--;
        else if (block.Value <= MovesCount) MovesCount -= block.Value;
        else return false;

        return true;
    }
    
    public bool CanConsume(Block block) => ((ProtectionCount > 0) || (block.Value <= MovesCount));

    public override string ToString()
    {
        string s = "Осталось места в желудке: " + MovesCount + ",\n<sprite=0>: " + ProtectionCount;
        return s;
    }

    public override void OnConsume(Gameboard gameboard)
    {
        return;
    }
}
