using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] public BlockData emptyBlock;
    //PlayerPrefs.SetInt("LastUnlockedLevel", 3);
}
