using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameEnemy : MonoBehaviour
{
    public ShootGameManager ShootGameManager;
    Image health;
    int speed;
    // Start is called before the first frame update
    void Start()
    {
        health = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //health.fillAmount -= Time.deltaTime * ShootGameManager.speed;
        //if(health.fillAmount <= 0 )
        //{
        //    Missed();
        //}
    }
    public void shoot()
    {
        if(ShootGameManager.Slider.value > 0.5)
        {
            ShootGameManager.TotalShoots(1);
            Destroy(gameObject);
        }
        else
        {
            ShootGameManager.ChangePlayerHealth(1);
            Destroy(gameObject);
        }
        ShootGameManager.ObjectSpawn();
    }
    public void Missed()
    {
        ShootGameManager.ChangePlayerHealth(1);
        Destroy(gameObject);
    }
}