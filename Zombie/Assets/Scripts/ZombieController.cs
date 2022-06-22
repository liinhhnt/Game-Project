using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ZombieController : MonoBehaviour
{
    public SkeletonAnimation skeleton;
    public GameObject zombieDie;
    private float speed;
    private float timeActime;
    public  float currentTime;
    public string animationName;
    private Vector2 pos;
    private bool existZombie = false;
    public bool isClicked;
    //public static CombineInstance instance;
    void Start()
    {
        /*skeleton.AnimationState.SetAnimation(0, "Dig", true).Complete += delegate
        {
            skeleton.AnimationState.SetAnimation(0, "Run", true);
        };*/
        //instance = this;
        isClicked = false;
        existZombie = false;
        timeActime = Random.Range(0f, 10f);
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeActime)
        {
            if (!existZombie)
            {
                SpawnZombie();
                existZombie = true;
            }
            
            {
                pos.y -= speed * Time.deltaTime;
                transform.position = pos;
                if (pos.y < -12f)
                {
                    KillZombie();
                }
            }
            if (isClicked)
            {
                KillZombie();
            }
        }
    }

    public void KillZombie()
    {
        zombieDie.SetActive(true);
        skeleton.gameObject.SetActive(false);
        StartCoroutine(WaitForKillZombie());
        isClicked = false;
        currentTime = 0f;
    }
    public IEnumerator WaitForKillZombie()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Kill!");
        Destroy(gameObject);
    }
    private void SpawnZombie()
    {
        GameObject spawnedZombie = Instantiate(gameObject);
        skeleton.AnimationState.SetAnimation(0, animationName, true);
        pos = new Vector2(Random.Range(-4f, 4f), 10);
        speed = Random.Range(1f, 3f);
    }

   
}
