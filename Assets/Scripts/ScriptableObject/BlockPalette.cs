using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockPalette", menuName = "Scriptable Objects/BlockPalette")]
public class BlockPalette : ScriptableObject
{
    [SerializeField] private List<BlockData> blocks;
    
    public List<BlockData> Blocks { get => blocks; }
}
