using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "MSO/Enemies/NewEnemyList")]
public class Enemies : ScriptableObject
{
    public Enemies_Interactions[] All_Enemies;
}
