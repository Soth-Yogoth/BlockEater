using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Block
{
    private Stack<Vector2Int> positionHistory = new Stack<Vector2Int>();
    
    private Gameboard _gameboard;
    
    public int MovesCount { get; set; }
    public int ProtectionCount { get; set; } = 0;
    
    public event Action<Player> OnStateChanged;
    public event Action<Player> OnDissolve;

    public Color Color { get; }
    

    public Player(Gameboard gameboard, Vector2Int position, Sprite sprite, int value, int movesCount, Color color) : base(position, sprite, value)
    {
        _gameboard = gameboard;
        Position = position;
        MovesCount = movesCount;
        Color = color;

        Tint = color;
    }

    public bool TryMove(Vector2Int direction)
    {
        Vector2Int newPosition = Position + direction;
        
        if (_gameboard.Blocks.IsInBounds(newPosition.x, newPosition.y))
        {
            Block nextBlock = _gameboard.Blocks[newPosition.x, newPosition.y];
            
            if (nextBlock is Exit)
            {
                _gameboard.DeleteBlock(Position);
                OnDissolve?.Invoke(this);
            }
            else if (TryConsume(nextBlock))
            {
                _gameboard.DeleteBlock(Position);
                _gameboard.UpdateBlock(newPosition, this);
            
                positionHistory.Push(Position);
                Position = newPosition;
                            
                OnStateChanged?.Invoke(this);
                
                nextBlock.OnConsume(_gameboard);
            }
            else return false;
            
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
        OnDissolve?.Invoke(this);
    }
}
