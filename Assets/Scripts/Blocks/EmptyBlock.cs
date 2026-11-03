using UnityEngine;

public class EmptyBlock : Block
{
    public EmptyBlock(Vector2Int pos, Sprite sprite, int value) : base(pos, sprite, value)
    {
        
    }

    public override void OnConsume(Gameboard gameboard)
    {
        
    }

    public override Block MakeCopy(Gameboard gameboard)
    {
        EmptyBlock copy = new EmptyBlock(Position, Sprite, Value);
        return copy;
    }
}
