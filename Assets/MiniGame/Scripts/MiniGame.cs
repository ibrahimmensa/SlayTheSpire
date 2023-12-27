using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiniGame", menuName = "MiniGameData")]
public class MiniGame : ScriptableObject
{
    public int totalTurns;

    public int winCounts;
    
    public int loseCounts;
}
