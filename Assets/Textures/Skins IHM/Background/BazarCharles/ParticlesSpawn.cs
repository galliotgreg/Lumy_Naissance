using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawn : MonoBehaviour {
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private GameObject particlesTrail;
    [SerializeField]
    private float period;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;

    private Vector3 pos;
    private float time = 0f;
    private float eps = 0.01f;
    private GameObject partSystem;
    private GameObject partSystemTrail;
    

    // Use this for initialization
    void Start () {
 
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
        pos = new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), 0f);

        if (UnityEngine.Random.value <= 0.5)
        {
            partSystem = Instantiate(particles, pos, Quaternion.identity);
        }
        else
        {
            partSystemTrail = Instantiate(particlesTrail, pos, Quaternion.identity);
        }

        Destroy(partSystem, 14f);
        Destroy(partSystemTrail, 14f);
    }

   
}
