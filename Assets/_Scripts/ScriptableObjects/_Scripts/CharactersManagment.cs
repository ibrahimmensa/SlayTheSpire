using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManager", menuName = "MSO/Player/NewCharacterManager")]
public class CharactersManagment : ScriptableObject
{
    public int TotalDeaths;
    public Characters[] Players;
}
