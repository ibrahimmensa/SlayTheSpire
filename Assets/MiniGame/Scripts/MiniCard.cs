using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCard : MonoBehaviour
{
    public bool aAz, jAck, empty;
    public MiniGameManager miniGameManager;
    public AnimationController animController;
    public Button CardBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadCard ()
    {
        if(animController.onclick)
        {
            var card = GetComponent<MiniCard>();
            miniGameManager.Fader.SetActive(true);
            if(animController.aAz)
            {
                if (card.aAz)
                {
                    Invoke(nameof(LevelCompleted), 0.3f);
                }
                else
                {
                    Invoke(nameof(LevelFailed), 0.3f);
                }
            }
            else if(animController.jAck)
            {
                if (card.jAck)
                {
                    Invoke(nameof(LevelCompleted), 0.3f);
                }
                else
                {
                    Invoke(nameof(LevelFailed), 0.3f);
                }
            }
        }
    }
    void LevelCompleted()
    {
        miniGameManager.ChangePanel(Panals.COMPLETED);
    }
    void LevelFailed()
    {
        miniGameManager.ChangePanel(Panals.END);
    }
}
