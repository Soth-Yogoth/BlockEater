using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Gameboard
{
    public Block[,] Blocks { get; private set; }

    // public Gameboard(Block[,] blocks)
    // {
    //     Blocks = blocks;
    // }

    public void Fill(Block[,] blocks)
    {
        Blocks = blocks;
    }

    public void UpdateBlock(Vector2Int blockPosition, Block newBlock)
    {
        if (Blocks.IsInBounds(blockPosition.x, blockPosition.y)) 
            Blocks[blockPosition.x, blockPosition.y] = newBlock;
    }

    //Список блоков, до которых можно добраться
    public List<Block> GetBlocksAvailableToConsume(Vector2Int startPoint, bool includeDiagonals = false)
    {
        List<Block> availableBlocks = new List<Block>(); 
        
        bool[,] visitedBlocks = new bool[Blocks.GetLength(0), Blocks.GetLength(1)];

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
            
        queue.Enqueue(startPoint);
        visitedBlocks[startPoint.x, startPoint.y] = true;
            
        while (queue.Count > 0)
        {
            Vector2Int currentPosition = queue.Dequeue();
            
            Vector2Int[] directions = includeDiagonals ? Directions.OrthogonalAndDiagonal : Directions.Orthogonal;

            foreach (var direction in directions)
            {
                Vector2Int nextPosition = currentPosition + direction;

                if (!Blocks.IsInBounds(nextPosition.x, nextPosition.y) 
                    || visitedBlocks[nextPosition.x, nextPosition.y]) 
                    continue;
                
                Block nextBlock = Blocks[nextPosition.x, nextPosition.y];

                if (nextBlock is EmptyBlock) 
                {
                    visitedBlocks[nextPosition.x, nextPosition.y] = true;
                    queue.Enqueue(nextPosition);
                }
                else availableBlocks.Add(nextBlock);
            }
        }
        
        return availableBlocks; 
    }
}
