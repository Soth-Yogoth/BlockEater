using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameView gameView;
    
    private LevelState _levelState;
    
     void Start()
     {
         _levelState = levelData.CreateLevelState();
         
         gameView.ResizeGridCell(_levelState.Grid.GetLength(0));
         gameView.RenderGrid(_levelState.Grid);
         
         // foreach (var player in _levelState.Players)
         // {
         //     gameView.AddPlayerInfo(player, player.Color);
         // }
     }

     public void HandlePlayerMove(Vector2Int direction)
     {
         foreach (var player in Player.Players)
         {
             if (player.TryMove(direction))
             {
                 if (_levelState.IsVictory())
                     gameView.ShowVictory();
                 else if (_levelState.IsDefeated())
                     gameView.ShowDefeat();
                 else gameView.RenderGrid(_levelState.Grid);
             }
         }
     }
}

