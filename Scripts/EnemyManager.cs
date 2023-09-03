using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> allEnemies = new List<GameObject>();  // spawns 100 enemies
    public List<GameObject> littleEnemies = new List<GameObject>();  // if user destroys the big ones it stores little ones here
    List<GameObject> randomPrefab = new List<GameObject>();
    public GameObject truck;
    public GameObject van;
    public GameObject ambulance;
    public float spawnRate = 3f;
    float nextSpawn = 0.0f;
    public GameObject myAudi;
    public BulletManager bulletList;  // gets the bullet list
    public Health getHealth;
    bool damaged = true;
    bool invulnerability = false;
    public float invulTime = 3f;
    public float counter = 0;
    public Score getScore;

    // for sound
    public SoundManager getSound;
    // Use this for initialization
    void Start()
    {
        // Add random prefab sprites to list
        randomPrefab.Add(truck);
        randomPrefab.Add(van);
        randomPrefab.Add(ambulance);
        myAudi = GameObject.Find("Audi");   // find audi object
        bulletList = GameObject.Find("BulletManager").GetComponent<BulletManager>();  // find bullet manager to get the list
        getHealth = GameObject.Find("Player").GetComponent<Health>();   // get health script to get the health
        getScore = GameObject.Find("scoreNumber").GetComponent<Score>();    // get score script to update score
        getSound = GameObject.Find("AudioObject").GetComponent<SoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        SpawnCars();
        CarCollision();
        RemoveBullet();
        BabyCollision();
        for (int i = 0; i < allEnemies.Count; i++)        // gets the nubmer of enemies spawning
        {
            for (int j = 0; j < bulletList.bulletHolder.Count; j++)        // gets the number of bullets in list
            {
                if (Collision(allEnemies[i], bulletList.bulletHolder[j]))      // if bullet hits enemy
                {
                    BulletCollision(allEnemies[i], bulletList.bulletHolder[j]);

                    // add 50 points;  
                    getScore.scoreValue += 50;
                    getSound.Explosion();
                }
            }
        }
        // grants invulnerability
        if (invulnerability)
        {
            counter += Time.deltaTime;
            if (counter > invulTime)
            {
                counter = 0;
                invulnerability = false;
                damaged = true;
            }
        }

        GameOver();
    }
    void SpawnCars()
    {
        for (int i = 0; i < 30; i++) // spawn 30 enemies
        {
            int prefabIndex = Random.Range(0, 3);
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;    // spawns car every 3 seconds and updates the next spawnrate
                allEnemies.Add(Instantiate(randomPrefab[prefabIndex]));
            }
        }
    }

    void CarCollision()
    {
        foreach (GameObject enemies in allEnemies)
        {
            if (Collision(enemies, myAudi) && damaged && !invulnerability)         // if car collides with big asteroid
            {
                getSound.Crash();
                invulnerability = true;
                // only get hit once
                damaged = false;

                // lose 1 health
                getHealth.health--;
            }

        }
        foreach (GameObject lilenemies in littleEnemies)  // if car  collides with small asteroid;
        {
            if (Collision(lilenemies, myAudi) && damaged && !invulnerability)
            {
                getSound.Crash();
                invulnerability = true;

                // only get hit once
                damaged = false;

                getHealth.health--;
            }
        }
    }


    void BulletCollision(GameObject enemyCar, GameObject theBullet)
    {
        // gets the position of next car
        Enemy nextCar = enemyCar.GetComponent<Enemy>();
        Vector3 location = nextCar.policePosition;


        // Split
        GameObject babyCar1 = Instantiate(enemyCar, location, Quaternion.identity);   // adds the 2 small cars
        GameObject babyCar2 = Instantiate(enemyCar, location, Quaternion.identity);
        littleEnemies.Add(babyCar1); // store the 2 cars in the list for later
        littleEnemies.Add(babyCar2);
        enemyCar.SetActive(false);
        theBullet.SetActive(false);
        allEnemies.Remove(enemyCar);
        bulletList.bulletHolder.Remove(theBullet);
        babyCar1.transform.localScale = new Vector3(.7f, .7f, 0);  // change size of car
        babyCar2.transform.localScale = new Vector3(.7f, .7f, 0);  // change size of car

        Enemy one = babyCar1.GetComponent<Enemy>();
        Enemy two = babyCar2.GetComponent<Enemy>();
        one.accelRate = 2f;
        two.accelRate = 2f;
    }


    void BabyCollision()
    {
        for (int i = 0; i < littleEnemies.Count; i++)
        {
            for (int j = 0; j < bulletList.bulletHolder.Count; j++)        // gets the number of bullets in list
            {
                if (Collision(littleEnemies[i], bulletList.bulletHolder[j]))
                {
                    getSound.Explosion();
                    littleEnemies[i].SetActive(false);
                    bulletList.bulletHolder[j].SetActive(false);
                    littleEnemies.Remove(littleEnemies[i]);
                    bulletList.bulletHolder.Remove(bulletList.bulletHolder[j]);
                    // add 20 points
                    getScore.scoreValue += 20;
                }
            }
        }
    }


    void RemoveBullet()          // if the bullets go off the map set it to null and destroy it
    {
        GameObject bManager = GameObject.Find("BulletManager");
        BulletManager bM = bManager.GetComponent<BulletManager>();

        GameObject empty = null;
        for (int i = 0; i < bM.bulletHolder.Count; i++)
        {
            if (bM.bulletHolder[i].transform.position.x < -26 || bM.bulletHolder[i].transform.position.x > 26 || bM.bulletHolder[i].transform.position.y < -10 ||
                bM.bulletHolder[i].transform.position.y > 10)
            {
                empty = bM.bulletHolder[i];    // set certain bullet equal to null
                bM.bulletHolder.Remove(bM.bulletHolder[i]);  // remove null
                Destroy(empty); // destroy null
            }
        }
    }




    // collision 
    public bool Collision(GameObject obj1, GameObject obj2)
    {
        float boxBminX = obj2.GetComponent<SpriteRenderer>().bounds.min.x;         // min x box 2
        float boxBminY = obj2.GetComponent<SpriteRenderer>().bounds.min.y;         // min y box 2
        float boxBmaxX = obj2.GetComponent<SpriteRenderer>().bounds.max.x;         // max x box 2
        float boxBmaxY = obj2.GetComponent<SpriteRenderer>().bounds.max.y;         // max y box 2
        float boxAminX = obj1.GetComponent<SpriteRenderer>().bounds.min.x;         // min x box 1         
        float boxAminY = obj1.GetComponent<SpriteRenderer>().bounds.min.y;         // min y box 1
        float boxAmaxX = obj1.GetComponent<SpriteRenderer>().bounds.max.x;         // max x box 1
        float boxAmaxY = obj1.GetComponent<SpriteRenderer>().bounds.max.y;         // max y box 1

        if ((boxBminX < boxAmaxX) && (boxBmaxX > boxAminX) && (boxBmaxY > boxAminY) && (boxBminY < boxAmaxY))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // game over
    void GameOver()
    {
        if (getHealth.health == 0)
        {
            SceneManager.LoadScene("GameOver");    // loads game over if user has 0 hp
        }
    }
}
