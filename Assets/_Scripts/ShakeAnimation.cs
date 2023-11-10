using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void End()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void destoryObject()
    {
        Destroy(gameObject);
    }
}
