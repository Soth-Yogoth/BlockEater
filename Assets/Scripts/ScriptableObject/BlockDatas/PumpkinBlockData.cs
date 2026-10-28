using UnityEngine;

[CreateAssetMenu(fileName = "PumpkinBlockData", menuName = "Scriptable Objects/PumpkinBlockData")]
public class PumpkinBlockData : BlockData
{
    [SerializeField] private int protectionValue;
    
    public override Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new PumpkinBlock(position, Sprite, value, protectionValue);
    }
}
