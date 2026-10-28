using System;
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
    private Gameboard _gameboard;
    public Block[,] Grid => _gameboard.Blocks;
    public List<Player> Players { get; }
    
    public event Action<Player> OnPlayerRemoved;

    public LevelState(Gameboard gameboard, List<Player> players, BlockData emptyBlock, int moves)
    {
        _gameboard = gameboard;
        _gameboard.EmptyBlock = emptyBlock;
        
        Players = players;
    }

    public bool IsVictory()
    {
        return Players.Count == 0;
    }

    public bool IsDefeated()
    {
        foreach (var player in Players)
        {
            List<Block> availableBlocks = _gameboard.GetBlocksAvailableToConsume(player.Position);

            foreach (var block in availableBlocks)
            {
                if (player.CanConsume(block)) return false;
            }
        }
        
        return true;
    }
    
    private void RemovePlayer(Player player)
    {
        if (Players.Remove(player))
            OnPlayerRemoved?.Invoke(player);
    }
}
