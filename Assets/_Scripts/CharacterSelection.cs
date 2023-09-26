using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [Header("Screptable Object Refrances")]
    public CharactersManagment PM;

    public GameObject name;
    public GameObject Health;
    public GameObject Gold;
    public GameObject Description;
    public Image BG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void mapdata(int CharacterSelected)
    {
        name.GetComponent<Text>().text = PM.Players[CharacterSelected].name;
        Health.GetComponent<Text>().text = "HP: " + PM.Players[CharacterSelected].Health + "/" + PM.Players[CharacterSelected].Health;
        Gold.GetComponent<Text>().text = "Gold: " + PM.Players[CharacterSelected].Gold;
        Description.GetComponent<Text>().text = PM.Players[CharacterSelected].Description;
        BG.sprite = PM.Players[CharacterSelected].CharacterBG;
    }
}
