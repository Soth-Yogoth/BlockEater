using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Block
{
    private Gameboard _gameboard;

    public int MovesCount { get; set; } = 10;
    public int ProtectionCount { get; set; } = 0;
    
    public event Action<Player> OnStateChanged;
    public event Action<Player> OnDissolve;

    public Color Color { get; }
    
    private bool escaped = false;
    

    public Player(Gameboard gameboard, Vector2Int position, Sprite sprite, int value, Color color) : base(position, sprite, value)
    {
        _gameboard = gameboard;
        Position = position;
        Color = color;

        Tint = color;
    }

    public bool TryMove(Vector2Int direction)
    {
        if (escaped) return false;
        
        Vector2Int newPosition = Position + direction;
        
        if (_gameboard.Blocks.IsInBounds(newPosition.x, newPosition.y))
        {
            Block nextBlock = _gameboard.Blocks[newPosition.x, newPosition.y];
            
            if (nextBlock is Exit)
            {
                _gameboard.DeleteBlock(Position);
                OnDissolve?.Invoke(this);

                escaped = true;
            }
            else if (TryConsume(nextBlock))
            {
                _gameboard.DeleteBlock(Position);
                _gameboard.UpdateBlock(newPosition, this);
                
                Position = newPosition;
                
                nextBlock.OnConsume(_gameboard);
                
                OnStateChanged?.Invoke(this);
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

    public override Block MakeCopy(Gameboard newGameboard)
    {
        Player copy = new Player(newGameboard, Position, Sprite, Value, Color);
        
        copy.MovesCount = MovesCount;
        copy.ProtectionCount = ProtectionCount;
        
        return copy;
    }
}
