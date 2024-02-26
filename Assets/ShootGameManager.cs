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
    public GameObject failed;
    public Scrollbar Slider;

    bool running;

    public Animator anim1;
    public Animator anim2;

    public RuntimeAnimatorController enemyAnim;

    void Start()
    {
        playerHealth = 2;
        totalShoots = 0;
        speed = 1f;
        currentTime = totalTime;
        running = true;
        // InvokeRepeating(nameof(ObjectSpawn), 0.5f, 3);
        ObjectSpawn();
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

    public void ObjectSpawn()
    {
        enemy = Instantiate(enemies[Random.Range(0,enemies.Length)],transform.GetChild(0).transform);
        enemy.GetComponent<MiniGameEnemy>().ShootGameManager = this;
        enemy.transform.position = spawnlocations[Random.Range(0, spawnlocations.Length)].position;
        enemy.transform.SetSiblingIndex(2);
        Slider.transform.parent = enemy.transform;
        Slider.GetComponent<Image>().enabled = true;
        Slider.GetComponent<Animator>().enabled = true;
        Slider.enabled = true;
        enemy.GetComponent<MiniGameEnemy>().anim.runtimeAnimatorController = enemyAnim;
        enemy.GetComponent<MiniGameEnemy>().anim.enabled = true;
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

        if (speed < 3) { speed = 1 + (0.05f * totalShoots);
            anim1.speed = speed;
            anim2.speed = speed;
        }
            
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
