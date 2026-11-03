using UnityEngine;

public abstract class Block
{
    public Vector2Int Position;
    public int Value;
    
    public Sprite Sprite;
    public Color Tint { get; protected set; } = Color.white;

    public Block(Vector2Int pos, Sprite sprite, int value)
    {
        Position = pos;
        Value = value;

        Sprite = sprite;
    }

    public abstract void OnConsume(Gameboard gameboard);

    public abstract Block MakeCopy(Gameboard gameboard);
}
