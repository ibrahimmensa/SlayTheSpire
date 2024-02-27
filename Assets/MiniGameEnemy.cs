using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameEnemy : MonoBehaviour
{
    public ShootGameManager ShootGameManager;
    Image health;
    int speed;
    public Animator anim;
    public Animator EnemyControllerAnim;

    bool beenShot = false;

    public string EnemyName;

    public float GreenScrollbarPartSize;
    public float GreenScrollbarPartMinPos;
    public float GreenScrollbarPartMaxPos;

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
        if (!beenShot)
        {
            //make the slider stop
            ShootGameManager.Slider.GetComponent<Animator>().enabled = false;
            ShootGameManager.anim1.enabled = false;
            ShootGameManager.anim2.enabled = false;
            beenShot = true;
            if (/*ShootGameManager.Slider.value > 0.5*/ ShootGameManager.rectOverlaps(ShootGameManager.ScrollBarGreenPart.GetComponent<RectTransform>(),ShootGameManager.ScrollBarHandle.GetComponent<RectTransform>()))
            {
                ShootGameManager.TotalShoots(1);
                ShootGameManager.Slider.transform.parent = null;
                StartCoroutine(goBack());
            }
            else
            {
                EnemyControllerAnim.SetBool("Attack", true);
                ShootGameManager.ChangePlayerHealth(1);
                StartCoroutine(PlayVfxAndDestroyGameObject());
            }
        }
    }

    IEnumerator PlayVfxAndDestroyGameObject()
    {
        yield return new WaitForSeconds(1);
        GameObject vfx = Instantiate(ShootGameManager.ScreenAnimations.Animations[Random.Range(0, ShootGameManager.ScreenAnimations.Animations.Length)],ShootGameManager.transform);
        vfx.transform.SetSiblingIndex(5);
        vfx.GetComponent<Animator>().speed = 2;
        yield return new WaitForSeconds(0.5f);
        ShootGameManager.Slider.transform.parent = null;
        Destroy(vfx);
        StartCoroutine(goBack());
    }

    IEnumerator goBack()
    {
        anim.SetTrigger("GoBack");
        yield return new WaitForSeconds(0.75f);
        ShootGameManager.Slider.transform.parent = ShootGameManager.transform;
        ShootGameManager.Slider.transform.SetSiblingIndex(2);
        Destroy(gameObject);
        if (!ShootGameManager.failed.activeSelf)
            ShootGameManager.ObjectSpawn();
    }

    public void Missed()
    {
        ShootGameManager.ChangePlayerHealth(1);
        Destroy(gameObject);
    }
}
