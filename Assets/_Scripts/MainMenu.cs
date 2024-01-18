using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum Screens
{
    MAINMENU,CHARACTER_SELECTION,ROUTE_SELECTION
}
//>>>>>>> b1837753be4a1a21093f454e5df7b81cb46b672f
public class MainMenu : MonoBehaviour
{
    public Image BG;
    public MenuManager MM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(fader());
    }

    public void fadeBG()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator fader()
    {
        yield return new WaitForSeconds(2.0f);
        BG.GetComponent<Animator>().SetTrigger("fade");
        yield return new WaitForSeconds(2.0f);
        BG.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        MM.SwitchScreen(Screns.CHARACTER);
        //Destroy(BG);
        //if (PlayerPrefs.HasKey("Levels"))
        //{
        //    SceneManager.LoadScene(1);
        //}
        //else
        //{
        //    MM.SwitchScreen(Screns.CHARACTER);
        //}
    }
}
