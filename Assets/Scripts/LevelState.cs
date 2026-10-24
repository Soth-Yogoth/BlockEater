using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelState
{
    public GameView gameView;
    public bool IsDefeated => isDefeated();
    public bool IsVictory => isVictory();
    public BlockData[,] Grid { get; }
    private BlockData _emptyBlock;

    private List<Player> _players = new List<Player>();
    public List<Player> Players => _players;

    public LevelState(BlockData[,] grid, BlockData emptyBlock, int moves)
    {
        _emptyBlock = emptyBlock;
        Grid = grid;

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                BlockData block = grid[i, j];

                if (block == null) continue;
                if (block.BlockType == BlockType.player)
                {
                    Player newPlayer = new Player(new Vector2Int(i, j), moves);
                    _players.Add(newPlayer);
                }
            }
        }
    }

    public bool TryMovePlayer(Vector2Int direction)
    {
        if (IsVictory || IsDefeated) return false;
        
        foreach (var player in _players.ToList())
        {
            Debug.Log("Player "  + player.Position + ", moves: " + player.MovesCount);
            
            Vector2Int newPosition = player.Position + direction;
            
            if (newPosition.x < 0 || newPosition.x >= Grid.GetLength(0) ||
                newPosition.y < 0 || newPosition.y >= Grid.GetLength(1))
                continue;
            
            BlockData block = Grid[newPosition.x, newPosition.y];

            if (block != null && block.BlockType == BlockType.exit)
            {
                Grid[player.Position.x, player.Position.y] = _emptyBlock;
                _players.Remove(player);
            }
            else if (player.TryConsume(block))
            {
                Grid[newPosition.x, newPosition.y] = Grid[player.Position.x, player.Position.y];
                Grid[player.Position.x, player.Position.y] = _emptyBlock;
            
                player.Position = newPosition;

                if (block.Effect != null)
                {
                    block.Effect.Play(player);
                    block.Effect.Play(this, newPosition);
                }
            }
        }
        return true;
    }
    
    public void UpdateBlock(int x, int y, BlockData block)
    {
        if (x < 0 || x >= Grid.Length || y < 0 || y >= Grid.Length) return;
        
        //РЕШЕНИЕ ВРЕМЕННОЕ
        if(block.BlockType == BlockType.player) _players.First().Position = new Vector2Int(x, y);
        Grid[x,y] = block;
    }
    
    public void ClearBlock(int x, int y)
    {
        if (x < 0 || x >= Grid.Length || y < 0 || y >= Grid.Length) return;
        
        Grid[x,y] = _emptyBlock;
    }

    private static readonly Vector2Int[] Directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private bool isVictory()
    {
        return _players.Count == 0;
    }

    private bool isDefeated()
    {
        foreach (var player in _players)
        {
            Debug.Log("Player "  + player.Position + ", moves: " + player.MovesCount + ", protect:  " + player.ProtectionCount);
            
            if (player.ProtectionCount > 0) return false;
            
            bool[,] visitedBlocks = new bool[Grid.GetLength(0), Grid.GetLength(1)];

            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            
            queue.Enqueue(player.Position);
            visitedBlocks[player.Position.x, player.Position.y] = true;
            
            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();

                foreach (var direction in Directions)
                {
                    Vector2Int next = current + direction;

                    // Проверяем границы массива
                    if (next.x < 0 || next.x >= Grid.GetLength(0) ||
                        next.y < 0 || next.y >= Grid.GetLength(1))
                        continue;

                    if (visitedBlocks[next.x, next.y]) continue;

                    BlockData block = Grid[next.x, next.y];

                    if (block == null || block.BlockType == BlockType.empty)
                    {
                        visitedBlocks[next.x, next.y] = true;
                        queue.Enqueue(next);
                    }
                    else if (block.Cost <= player.MovesCount) return false;
                }
            }
        }
        
        return true;
    }
}
