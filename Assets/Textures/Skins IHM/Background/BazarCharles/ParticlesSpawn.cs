using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawn : MonoBehaviour {
    [Header("Set in inspector")]
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private GameObject particlesTrail;
    [SerializeField]
    private GameObject partSpawner;
    [SerializeField]
    private float period;

    private float xMax;
    private float yMax;
    private float partDuration;
    private float partTrailDuration;
    private Vector3 pos;
    private float time = 0f;
    private float eps = 0.01f;
    private GameObject partSystem;
    private GameObject partSystemTrail;
    
    // Use this for initialization
    void Start () {
        //Access canvas width and height
        GameObject spawnerParent = partSpawner.transform.parent.gameObject;
        RectTransform canvasRectTransform = spawnerParent.GetComponent<RectTransform>();
        //Set max coordinates
        xMax = canvasRectTransform.rect.width/2f;
        yMax = canvasRectTransform.rect.height/2f;
        //Get ParticleSystem duration
        partDuration = partSystem.GetComponent<ParticleSystem>().main.duration;
        partTrailDuration = partSystemTrail.GetComponent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    void Update () {
        
        //periodic action
        if( time % period < eps)
        {
            CreateParticles();  
        }
        //timer
        time += Time.deltaTime;

    }

    private void CreateParticles()
    {
        //Pick position
        pos = new Vector3(UnityEngine.Random.Range(-xMax, xMax), UnityEngine.Random.Range(-yMax, yMax), 0f);

        //Choose which particles to create
        if (UnityEngine.Random.value <= 0.5)
        {
            partSystem = Instantiate(particles, pos, Quaternion.identity);
        }
        else
        {
            partSystemTrail = Instantiate(particlesTrail, pos, Quaternion.identity);
        }
        
        //Destroy excedents particles
        Destroy(partSystem, 2*partDuration);
        Destroy(partSystemTrail, 2*partTrailDuration);
    }

   
}
