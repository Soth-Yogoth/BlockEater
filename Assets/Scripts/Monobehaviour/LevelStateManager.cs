using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelStateManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameView gameView;
    
    private LevelState _levelState;
    private bool _endScreenShown;
    
    private LevelStateHistory _levelStateHistory;
    private int cancelCount = 3;
    
     void Start()
     {
         _levelState = levelData.CreateLevelState();
         _levelState.OnPlayerRemoved += HandlePlayerRemoved;
         
         foreach (Player player in _levelState.Players) gameView.AddPlayerInfo(player);
         
         gameView.ResizeGridCell(_levelState.Grid.GetLength(0));
         gameView.RenderGrid(_levelState.Grid);
         
         _levelStateHistory = new LevelStateHistory();
         _levelStateHistory.Add(_levelState);
     }

     public void HandlePlayerMove(Vector2Int direction)
     {
         switch (direction)
         {
             case { x: 0, y: 1 }:
                 _levelState.Players.Sort((a, b) => b.Position.y.CompareTo(a.Position.y));
                 break;
             case { x: 0, y: -1 }:
                 _levelState.Players.Sort((a, b) => a.Position.y.CompareTo(b.Position.y));
                 break;
             case { x: 1, y: 0 }:
                 _levelState.Players.Sort((a, b) => b.Position.x.CompareTo(a.Position.x));
                 break;
             case { x: -1, y: 0 }:
                 _levelState.Players.Sort((a, b) => a.Position.x.CompareTo(b.Position.x));
                 break;
         }

         bool playerMoved = false;
         List<Player> players = _levelState.Players.ToList();
         
         foreach (var player in players)
         {
             if (player.TryMove(direction))
             {
                 playerMoved = true;
                 
                 if (_levelState.IsVictory())
                     gameView.ShowVictory();
                 else if (_levelState.IsDefeated() && cancelCount <= 0)
                     gameView.ShowDefeat();
             }
         }
         
         if (playerMoved)
         {
             _levelStateHistory.Add(_levelState);
             gameView.RenderGrid(_levelState.Grid);
         }
     }

     public void OnMoveCancelled()
     {
         if (_levelStateHistory.TryGet(out LevelState lastLevelState) && cancelCount-- > 0)
         {
             _levelState.OnPlayerRemoved -= HandlePlayerRemoved;

             foreach (Player player in _levelState.Players)
             {
                 gameView.RemovePlayerInfo(player);
             }
             
             _levelState = lastLevelState.MakeCopy();
             gameView.RenderGrid(_levelState.Grid);
             
             _levelState.OnPlayerRemoved += HandlePlayerRemoved;
             
             foreach (Player player in  _levelState.Players)
             {
                 gameView.AddPlayerInfo(player);
                 player.OnDissolve += (player) => _levelState.Players.Remove(player);
             }
         }
     }

     private void HandlePlayerRemoved(Player player)
     {
         gameView.RemovePlayerInfo(player);
     }
     
     private void OnDisable()
     {
         _levelState.OnPlayerRemoved -= HandlePlayerRemoved;
         foreach (Player player in _levelState.Players.ToArray())
             gameView.RemovePlayerInfo(player);
     }
    
}

