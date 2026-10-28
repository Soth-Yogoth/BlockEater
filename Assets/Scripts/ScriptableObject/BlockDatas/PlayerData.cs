using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : BlockData
{
    [SerializeField] private int movesCount;
    [SerializeField] private Color color;
    
    public override Block CreateBlock(Gameboard gameboard, Vector2Int position)
    {
        return new Player(gameboard, position, Sprite, value, movesCount, color);
    }
}
