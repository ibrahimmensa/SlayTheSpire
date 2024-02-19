using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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
    public Cards ToLoadCards;

    [Space()]
    [Header("Screens Refrances")]
    public MainMenu MainMenu;
    public CharacterSelection CharacterSelection;
    public MapManagement MapManagement;
    public AudioSource bgm;
    public VideoPlayer vp;

    private void OnEnable()
    {
        if(PlayerPrefs.GetInt("Start",0) == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Start", 1);
        }
        bgm.volume = PlayerPrefs.GetFloat("bg", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchScreen(Screns.MAINMENU);
        if(PlayerPrefs.GetInt("start",0) == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("start", 1);
            Debug.Log("FT");
        }
        else
        {
            PM.LoadLevel = PlayerPrefs.GetInt("Levels");
            foreach (CardsData obj in ToLoadCards.AttackCards) 
            {
                if(!obj.canShow)
                {
                    if(PlayerPrefs.HasKey(obj.name))
                    {
                        obj.canShow = true;
                        obj.looted = true;
                        Debug.Log(obj.name + " card looted updated");
                    }
                }
            }
        }
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
