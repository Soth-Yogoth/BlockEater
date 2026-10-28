using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelStateManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameView gameView;
    
    private LevelState _levelState;
    private bool _endScreenShown;
    
     void Start()
     {
         _levelState = levelData.CreateLevelState();
         _levelState.OnPlayerRemoved += HandlePlayerRemoved;
         
         foreach (Player player in _levelState.Players) gameView.AddPlayerInfo(player);
         
         gameView.ResizeGridCell(_levelState.Grid.GetLength(0));
         gameView.RenderGrid(_levelState.Grid);
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
         
         foreach (var player in _levelState.Players)
         {
             if (player.TryMove(direction))
             {
                 gameView.RenderGrid(_levelState.Grid);
                 
                 if (_levelState.IsVictory())
                     gameView.ShowVictory();
                 else if (_levelState.IsDefeated())
                     gameView.ShowDefeat();
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

