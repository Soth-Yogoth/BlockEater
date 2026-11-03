using UnityEngine;

public class Exit : Block
{
    public Exit(Vector2Int pos, Sprite sprite, int value) : base(pos, sprite, value)
    {
        
    }

    public override void OnConsume(Gameboard gameboard)
    {
        
    }
    
    public override Block MakeCopy(Gameboard gameboard)
    {
        Exit copy = new Exit(Position, Sprite, Value);
        return copy;
    }
}
