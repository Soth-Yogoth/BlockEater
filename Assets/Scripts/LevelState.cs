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
    public Gameboard Gameboard;
    public Block[,] Grid => Gameboard.Blocks;
    public List<Player> Players { get; }

    private bool timer;
    private float timeToDefeat;
    
    public event Action<Player> OnPlayerRemoved;

    public LevelState(Gameboard gameboard, List<Player> players, BlockData emptyBlock)
    {
        Gameboard = gameboard;
        Gameboard.EmptyBlock = emptyBlock;
        
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
            List<Block> availableBlocks = Gameboard.GetBlocksAvailableToConsume(player.Position);

            foreach (var block in availableBlocks)
            {
                if (player.CanConsume(block)) return false;
            }
        }
        
        return true;
    }
    
    // private void RemovePlayer(Player player)
    // {
    //     if (Players.Remove(player))
    //         OnPlayerRemoved?.Invoke(player);
    // }

    public LevelState MakeCopy()
    {
        Gameboard newGameboard = Gameboard.MakeCopy();
        Dictionary<Block, Block> blockCopies = new Dictionary<Block, Block>();
        
        List<Player> newPlayers = new List<Player>();
        
        foreach (Player player in Players)
        {
            Player newPlayer = player.MakeCopy(newGameboard) as Player;
            newPlayers.Add(newPlayer);
            
            blockCopies.Add(player, newPlayer);
        }
        
        newGameboard.CopyBlocksFrom(Gameboard, blockCopies);
        
        LevelState copy = new LevelState(newGameboard, newPlayers, Gameboard.EmptyBlock);
        copy.OnPlayerRemoved = OnPlayerRemoved;
        
        return copy;
    }
}
