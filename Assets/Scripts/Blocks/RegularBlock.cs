using UnityEngine;

public class RegularBlock : Block
{
    public RegularBlock(Vector2Int pos, Sprite sprite, int value) : base(pos, sprite, value)
    {
        
    }

    public override void OnConsume(Gameboard gameboard)
    {
        
    }
    
    public override Block MakeCopy(Gameboard gameboard)
    {
        RegularBlock copy = new RegularBlock(Position, Sprite, Value);
        return copy;
    }
}
