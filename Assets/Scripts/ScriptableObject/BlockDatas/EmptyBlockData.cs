using UnityEngine;

[CreateAssetMenu(fileName = "EmptyBlock", menuName = "Scriptable Objects/EmptyBlock")]
public class EmptyBlockData : BlockData
{
    public override Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new EmptyBlock(position, Sprite, value);
    }
}
