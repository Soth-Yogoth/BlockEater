using UnityEngine;

[CreateAssetMenu(fileName = "PortalEffect", menuName = "Scriptable Objects/PortalEffect")]
public class PortalEffect : BlockEffect
{
    public override void Play(Player player)
    {
        return;
    }
    
    public override void Play(LevelState levelState, Vector2Int actorPosition)
    {
        BlockData player = levelState.Grid[actorPosition.x, actorPosition.y];

        for (int i = 0; i < levelState.Grid.GetLength(0); i++)
        {
            for (int j = 0; j < levelState.Grid.GetLength(1); j++)
            {
                BlockData block = levelState.Grid[i, j];
                if (block.Effect && block.Effect.GetType() == typeof(PortalEffect))
                {
                    levelState.UpdateBlock(i, j, player);
                    levelState.ClearBlock(actorPosition.x, actorPosition.y);
                }
            }
        }
    }
}
