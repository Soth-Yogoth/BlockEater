using System.Collections.Generic;
using UnityEngine;

public static class GridExtensions
{
    public static bool IsInBounds<T>(this T[,] grid, int x, int y)
        => x >= 0 && x < grid.GetLength(0)
        && y >= 0 && y < grid.GetLength(1);
}

public class LevelState
{
    public Block[,] Grid { get; }

    private Gameboard _gameboard;
    private BlockData _emptyBlock;
    
    //public List<Player> Players;

    public LevelState(Gameboard gameboard, BlockData emptyBlock, int moves)
    {
        _emptyBlock = emptyBlock;
        Grid = gameboard.Blocks;
        
        _gameboard = gameboard;
        //Players = InitializePlayers(grid, moves);
    }
    
    public void ClearBlock(int x, int y)
    {
        if (Grid.IsInBounds(x, y)) Grid[x, y] = _emptyBlock.CreateBlock(_gameboard, new Vector2Int(x, y));
    }

    public bool IsVictory()
    {
        return Player.Players.Count == 0;
    }

    public bool IsDefeated()
    {
        foreach (var player in Player.Players)
        {
            List<Block> availableBlocks = _gameboard.GetBlocksAvailableToConsume(player.Position);

            foreach (var block in availableBlocks)
            {
                if (player.CanConsume(block)) return false;
            }
        }
        
        return true;
    }

    // private List<Player> InitializePlayers(BlockData[,] grid, int moves)
    // {
    //     List<Player> players = new List<Player>();
    //     
    //     for (int i = 0; i < grid.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < grid.GetLength(1); j++)
    //         {
    //             Block block = grid[i,j];
    //             
    //             if (block is Player)
    //             {
    //                 Color color = new Color(i * 10, j * 10, i + j * 10);
    //                 
    //                 // Player newPlayer = new Player(_gameboard, new Vector2Int(i, j), moves, color);
    //                 // players.Add(newPlayer);
    //             }
    //         }
    //     }
    //
    //     return players;
    // }
}
