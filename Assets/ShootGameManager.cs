using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemies;

    [SerializeField]
    Transform[] spawnlocations;

    [SerializeField]
    GameObject[] HealthImage;


    [SerializeField]
    Text shoots;
    int playerHealth;
    int totalShoots;



    [SerializeField]
    Text timmer;
    float totalTime = 60f; 
    float currentTime;

    GameObject enemy;
    public float speed;

    [SerializeField]
    GameObject failed;

    bool running;

    void Start()
    {
        playerHealth = 2;
        totalShoots = 0;
        speed = 0.5f;
        currentTime = totalTime;
        running = true;
        InvokeRepeating(nameof(ObjectSpawn), 0.5f, 3);
    }

    void Update()
    {
        if(running) {
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                if (playerHealth > -1)
                {
                    CancelInvoke(nameof(ObjectSpawn));
                    SceneManager.LoadScene(0);
                }
                else
                {
                    failed.SetActive(true);
                    CancelInvoke(nameof(ObjectSpawn));
                }
                running = false;
            }
        }
    }

    void UpdateTimerUI()
    {
        shoots.text = totalShoots.ToString();
        // Update the UI text to display the current time
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        if(currentTime > 0)
        {
            timmer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void ObjectSpawn()
    {
        enemy = Instantiate(enemies[Random.Range(0,4)],transform.GetChild(0).transform);
        enemy.GetComponent<MiniGameEnemy>().ShootGameManager = this;
        enemy.transform.position = spawnlocations[Random.Range(0, 4)].position;
    }
    //--------------------------------------------------------------------------------------------------------------------------------
    public void ChangePlayerHealth(int health)
    {
        if (playerHealth <= 0)
        {
            failed.SetActive(true);
            CancelInvoke(nameof(ObjectSpawn));
            return;
        }


        Destroy(HealthImage[playerHealth]);
        playerHealth = playerHealth - health;
    }
    public void TotalShoots(int shoots)
    {
        totalShoots = totalShoots + shoots;

        if (totalShoots > 3) { speed = 0.7f; }
            
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
