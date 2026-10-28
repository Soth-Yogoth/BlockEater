using UnityEngine;

[CreateAssetMenu(fileName = "CornBlockData", menuName = "Scriptable Objects/CornBlockData")]
public class CornBlockData : BlockData
{
    [SerializeField] BlockData popcornBlockData;
    
    public override Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new CornBlock(position, Sprite, value, popcornBlockData);
    }
}
