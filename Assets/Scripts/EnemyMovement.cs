using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private GameObject player;

    private Transform playerPos;
    private NavMeshAgent navAgent;


    // Use this for initialization
    void Start () {
        player = GameManager.instance.Player;
        playerPos = player.transform;
        navAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        navAgent.SetDestination(playerPos.position);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Bullet")
        {
            Instantiate(GameManager.instance.Explotion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
