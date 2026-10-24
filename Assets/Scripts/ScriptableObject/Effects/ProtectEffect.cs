using UnityEngine;

[CreateAssetMenu(fileName = "ProtectEffect", menuName = "Scriptable Objects/ProtectEffect")]
public class ProtectEffect : BlockEffect
{
    [SerializeField] private int quantity;
    
    public override void Play(Player player)
    {
        player.ProtectionCount += quantity;
    }
    
    public override void Play(LevelState levelState, Vector2Int actorPosition)
    {
        return;
    }
}