using UnityEngine;

[CreateAssetMenu(fileName = "ExitBlockData", menuName = "Scriptable Objects/ExitBlockData")]
public class ExitBlockData : BlockData
{
    public override Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new Exit(position, Sprite, value);
    }
}
