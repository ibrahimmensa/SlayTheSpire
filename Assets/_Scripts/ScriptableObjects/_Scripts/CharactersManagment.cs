using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManager", menuName = "CharacterManager")]
public class CharactersManagment : ScriptableObject
{
    public Characters[] Players;
}
