using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopcornEffect", menuName = "Scriptable Objects/PopcornEffect")]
public class PopcornEffect : BlockEffect
{
    [SerializeField] private BlockData popcornBlock;
    
    private static readonly Vector2Int[] Directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, 1),
    };
    
    public override void Play(Player player)
    {
        return;
    }
    
    public override void Play(LevelState levelState, Vector2Int actorPosition)
    {
        BlockData[,] grid = levelState.Grid;
        
        Queue<Vector2Int>  queue = new Queue<Vector2Int>();
        bool[,] visitedBlocks = new bool[grid.GetLength(0), grid.GetLength(1)];
        
        queue.Enqueue(actorPosition);

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            
            foreach (Vector2Int direction in Directions)
            {
                Vector2Int next = current + direction;

                if (next.x >= 0 && next.x < grid.GetLength(0) && next.y >= 0 &&
                    next.y < grid.GetLength(1))
                {
                    BlockData block = grid[next.x, next.y];

                    if (block.BlockType == BlockType.empty && !visitedBlocks[next.x, next.y])
                    {
                        queue.Enqueue(next);
                        visitedBlocks[next.x, next.y] = true;
                    }
                    else if (block.BlockType == BlockType.block) 
                        levelState.UpdateBlock(next.x, next.y, popcornBlock);
                }
            }
        }
        
        return;
    }
}
