using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Screns
{
    MAINMENU,
    CHARACTER,
    MAP
}
public class MenuManager : MonoBehaviour
{
    [Header("Screptable Object Refrances")]
    public CharactersManagment PM;

    [Space()]
    [Header("Screens Refrances")]
    public MainMenu MainMenu;
    public CharacterSelection CharacterSelection;
    public MapManagement MapManagement;
    // Start is called before the first frame update
    void Start()
    {
        SwitchScreen(Screns.MAINMENU);
    }

    public  void SwitchScreen(Screns screens)
    {
        DisableAll();
        switch(screens)
        {
            case Screns.MAINMENU:
                {
                    MainMenu.gameObject.SetActive(true);
                    break;
                }
            case Screns.CHARACTER:
                {
                    CharacterSelection.gameObject.SetActive(true);
                    break;
                }
            case Screns.MAP:
                {
                    MapManagement.gameObject.SetActive(true);
                    break;
                }
        }
    }
    public void DisableAll()
    {
        MainMenu.gameObject.SetActive(false);
        CharacterSelection.gameObject.SetActive(false);
        MapManagement.gameObject.SetActive(false);
    }

}
