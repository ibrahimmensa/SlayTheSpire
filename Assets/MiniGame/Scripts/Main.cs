using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject[] obj;
    public Animation gameAnimatons;
    public string[] AnimationNames;
    [SerializeField]
    GameObject SelectedAnimators;
    private void OnEnable()
    {
        SelectedAnimators = obj[Random.Range(0,obj.Length)];
        SelectedAnimators.SetActive(true);
        GameAlgo(SelectedAnimators.transform.GetChild(0).GetComponent<Animator>());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameAlgo(Animator anim)
    {
      
    }
}
