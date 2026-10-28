using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/BlockData")]
public class BlockData : ScriptableObject
{
    [SerializeField] public Sprite Sprite;
    [SerializeField] protected int value;
    
    public virtual Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new RegularBlock(position, Sprite, value);
    }
}
