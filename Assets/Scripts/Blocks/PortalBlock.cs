using System.Collections.Generic;
using UnityEngine;

public class PortalBlock : RegularBlock
{
    //А что если превратиться в попкорн ?
    private static PortalBlock firstPortal;
    private static PortalBlock secondPortal;
    
    public PortalBlock(Vector2Int pos, Sprite sprite, int value) : base(pos, sprite, value)
    {
        if (firstPortal == null) firstPortal = this;
        else secondPortal = this;
    }

    public override void OnConsume(Gameboard gameboard)
    {
        if (firstPortal == null || secondPortal == null) return;
        
        Block player = gameboard.Blocks[Position.x, Position.y];
        
        //gameboard.ClearBlock(Position.x, Position.y);
        //gameboard.UpdateBlock(Position.x, Position.y, player);
    }

    public override Block MakeCopy(Gameboard gameboard)
    {
        PortalBlock copy = new PortalBlock(Position, Sprite, Value);
        return copy;
    }
}
