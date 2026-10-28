using UnityEngine;

public class EmptyBlock : Block
{
    public EmptyBlock(Vector2Int pos, Sprite sprite, int value) : base(pos, sprite, value)
    {
        
    }

    public override void OnConsume(Gameboard gameboard)
    {
        throw new System.NotImplementedException();
    }
}
