using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Text Power;
    public Text cardName;
    public Text Discription;
    public Text Rarity;

    [Header("Card Data")]
    public int Magic_power, CurseEffect;
    public float EnemyDamage, BlockedDamage;
    public bool Damage, Defence, Curse,Medicated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
