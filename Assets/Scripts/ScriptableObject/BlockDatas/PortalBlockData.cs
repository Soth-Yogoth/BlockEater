using UnityEngine;

[CreateAssetMenu(fileName = "PortalBlockData", menuName = "Scriptable Objects/PortalBlockData")]
public class PortalBlockData : BlockData
{
    public override Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new PortalBlock(position, Sprite, value);
    }
}
