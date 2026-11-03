using System.Collections.Generic;
using UnityEngine;

public class CornBlock : RegularBlock
{
    private BlockData popcornBlock;
    
    public CornBlock(Vector2Int pos, Sprite sprite, int value, BlockData popcornBlockData) : base(pos, sprite, value)
    {
        popcornBlock = popcornBlockData;
    }

    public override void OnConsume(Gameboard gameboard)
    {
        List<Block> targetBlocks = gameboard.GetBlocksAvailableToConsume(Position, true);
        
        foreach (var block in targetBlocks)
        {
            if (block is RegularBlock) gameboard.UpdateBlock(block.Position, popcornBlock.CreateBlock(gameboard, block.Position));
        }
    }
    
    public override Block MakeCopy(Gameboard gameboard)
    {
        CornBlock copy = new CornBlock(Position, Sprite, Value, popcornBlock);
        return copy;
    }
}
