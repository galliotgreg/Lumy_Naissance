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
    private float period;
    [SerializeField]
    private float depth;

    private int compteurParticules = 0;   
    private float xMax;
    private float yMax;
    private float time = 0f;
    private float eps = 0.005f;
    private List<GameObject> particlesList;
    private List<GameObject> particlesToBeDestroyed;


    // Use this for initialization
    void Start () {
        particlesList = new List<GameObject>();
        particlesToBeDestroyed = new List<GameObject>();
        //Access parent canvas width and height
        GameObject spawnerParent = this.gameObject.transform.parent.gameObject;
        RectTransform canvasRectTransform = spawnerParent.GetComponent<RectTransform>();
        //Set max coordinates
        xMax = canvasRectTransform.rect.width/2f;
        yMax = canvasRectTransform.rect.height/2f;
        Debug.Log("parent name :" + spawnerParent.name);
        Debug.Log("xMax :" + xMax);
        Debug.Log("yMax :" + yMax);
    }

    // Update is called once per frame
    void Update () {
        //periodic creation
        if ( time % period < eps)
        { 
            CreateParticles();
        }
        //DestroyParticles();
        //timer
        time += Time.deltaTime;
    }

    private void CreateParticles()
    {
        //Pick position
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-xMax, xMax), UnityEngine.Random.Range(-yMax, yMax), depth);

        //Choose which particles to create
        if (UnityEngine.Random.value <= 0.5)
        {
            GameObject part = Instantiate(particles, pos, Quaternion.identity);
            part.transform.SetParent(this.gameObject.transform, false);
            particlesList.Add(part);  
        }
        else
        {
            GameObject partTrail = Instantiate(particlesTrail, pos, Quaternion.identity);
            partTrail.transform.SetParent(this.gameObject.transform, false);
            particlesList.Add(partTrail);   
        }  
  
    }

    private void DestroyParticles()
    {
        if (particlesList != null)
        { 
            //Check if particle system is expired
            for (int i = 0; i < particlesList.Count; i++)
            {
                float timeSinceCreation = time - period * (i+compteurParticules);
                //Debug.Log("time since creation:" + timeSinceCreation);
                //Debug.Log("time:" + time);
                
                if (timeSinceCreation > particlesList[i].GetComponent<ParticleSystem>().main.duration)
                {
                   
                    //Debug.Log("time since creation:" + timeSinceCreation);
                    //Debug.Log("time:" + time);

                    particlesToBeDestroyed.Add(particlesList[i]);
                    particlesList.RemoveAt(i);
                    i--;
                    compteurParticules +=  1;
                }
            }
        } 
        
        if (particlesToBeDestroyed != null)
        {
            //Destroy particles
            foreach (GameObject part in particlesToBeDestroyed)
            {
                Destroy(part);
            }
        }
            
    }
}
