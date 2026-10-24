using UnityEngine;

//[CreateAssetMenu(fileName = "BlockEffect", menuName = "Scriptable Objects/BlockEffect")]
public abstract class BlockEffect : ScriptableObject
{
    public abstract void Play(Player player);
    public abstract void Play(LevelState levelState, Vector2Int actorPosition);
}
