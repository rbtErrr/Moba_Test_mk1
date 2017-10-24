using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    GameObject[] spawnPoints;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject explotion;

    private GameObject newEnemy;
    private float generatedSpawnTime = 4;
    private float currentSpawnTime = 0;

    
    public GameObject Player
    {
        get { return player; }
    }

    public GameObject Explotion
    {
        get { return explotion; }
    }

    public GameObject BulletPrefab
    {
        get { return bulletPrefab; }
    }

    public Transform BulletSpawnPoint
    {
        get { return bulletSpawnPoint; }
    }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }


    // Use this for initialization
    void Start()
    {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
    }

    IEnumerator spawn()
    {
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
            int randomNumber = Random.Range(0, spawnPoints.Length);
            GameObject spawnLocation = spawnPoints[randomNumber];
            newEnemy = Instantiate(enemy) as GameObject;
            newEnemy.transform.position = spawnLocation.transform.position;

        }

        yield return null;
        StartCoroutine(spawn());
    }
}