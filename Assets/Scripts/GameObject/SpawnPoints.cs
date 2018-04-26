using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {


    [SerializeField]
    private int nbSpawnPoints = 0;

    [SerializeField]
    ResourceScript MineraiJ1;
    [SerializeField]
    ResourceScript MineraiJ2;

    private int chooseSpawnPoint; 
    // Use this for initialization
    void Start () {
        chooseRandomPoint();  
    }

    private void chooseRandomPoint()
    {
        if(nbSpawnPoints < 2)
        {
            return;
           
        }
        //Choose a Random Point
        chooseSpawnPoint = UnityEngine.Random.Range(0, nbSpawnPoints);

        //Get the List of the Minerai Objects 
        List<Transform> spawnMinJ1 = MineraiJ1.SpawnPoints;
        List<Transform> spawnMinJ2 = MineraiJ2.SpawnPoints;

        //Set the Random point in the list if it exists
        if(MineraiJ1.SpawnPoints[chooseSpawnPoint] != null && MineraiJ2.SpawnPoints[chooseSpawnPoint] != null)
        {
            MineraiJ1.gameObject.transform.position = MineraiJ1.SpawnPoints[chooseSpawnPoint].position;
            MineraiJ2.gameObject.transform.position = MineraiJ2.SpawnPoints[chooseSpawnPoint].position;
            

        }
        else
        {
            Debug.LogWarning("Bad Implementation of the Spawner of resources"); 
        }
    }

}
