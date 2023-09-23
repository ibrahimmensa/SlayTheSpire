using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Screens
{
    MAINMENU,CHARACTER_SELECTION,ROUTE_SELECTION
}
//>>>>>>> b1837753be4a1a21093f454e5df7b81cb46b672f
public class MainMenu : MonoBehaviour
{
    public Image BG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Invoke("fadeBG", 4);
    }

    public void fadeBG()
    {
        BG.GetComponent<Animator>().SetTrigger("fade");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
