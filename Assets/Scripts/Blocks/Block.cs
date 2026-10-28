using UnityEngine;

public abstract class Block
{
    public Vector2Int Position;
    //public BlockData Data;
    public Sprite Sprite;
    public int Value;

    public Block(Vector2Int pos, Sprite sprite, int value)
    {
        Position = pos;
        //Data = data;
        Sprite = sprite;
        Value = value;
    }

    public abstract void OnConsume(Gameboard gameboard);
}
