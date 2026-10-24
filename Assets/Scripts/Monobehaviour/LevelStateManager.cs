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
         
         foreach (Player player in _levelState.Players)
         {
             gameView.AddPlayerInfo(player);
         }
     }

     public void HandlePlayerMove(Vector2Int direction)
     {
         if (_levelState.TryMovePlayer(direction))
         {
             gameView.RenderGrid(_levelState.Grid);

             if (_levelState.IsVictory)
                 gameView.ShowVictory();
             else if (_levelState.IsDefeated)
                 gameView.ShowDefeat();
         }
     }
}

