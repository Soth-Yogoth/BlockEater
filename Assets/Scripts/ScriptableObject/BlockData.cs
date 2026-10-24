using UnityEngine;

public enum  BlockType
{
    block,
    player,
    empty,
    exit
}

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/BlockData")]
public class BlockData : ScriptableObject
{
    [SerializeField] private string blockName = "EmptyBlock";
    [SerializeField] private BlockType blockType = BlockType.empty;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int cost;
    [SerializeField] private BlockEffect effect;
    
    public string BlockName { get => blockName; }
    public BlockType BlockType { get => blockType; }
    public int Cost { get => cost; }
    public Sprite Sprite { get => sprite; }
    public BlockEffect Effect { get => effect; }
}
