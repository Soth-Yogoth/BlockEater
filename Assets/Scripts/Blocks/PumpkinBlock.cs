using UnityEngine;

public class PumpkinBlock : RegularBlock
{
    private int protectionValue;

    public PumpkinBlock(Vector2Int pos, Sprite sprite, int value, int protection) : base(pos, sprite, value)
    {
        protectionValue = protection;
    }

    public override void OnConsume(Gameboard gameboard)
    {
        Player player = (Player)gameboard.Blocks[Position.x, Position.y];
        player.ProtectionCount += protectionValue;
    }
}
