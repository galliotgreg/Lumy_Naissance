using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyJoin : MonoBehaviour {
    [SerializeField]
    private PhyBone srcBone;
    [SerializeField]
    private PhyBone[] dstBones;
    [SerializeField]
    private Color color;
    [SerializeField]
    private float intensity;
    [SerializeField]
    private Color trgColor;
    [SerializeField]
    private float trgIntensity;
    [SerializeField]
    private Color srcColor;
    [SerializeField]
    private float srcIntensity;
    [SerializeField]
    private float totalTime;
    [SerializeField]
    private float lastTime;
    [SerializeField]
    private float delta;
    [SerializeField]
    private int seed = 42;

    private System.Random rand;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter; 


    private Light pointLight;

    public PhyBone[] DstBones
    {
        get
        {
            return dstBones;
        }

        set
        {
            dstBones = value;
        }
    }

    public PhyBone SrcBone
    {
        get
        {
            return srcBone;
        }

        set
        {
            srcBone = value;
        }
    }

    // Use this for initialization
    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        pointLight = GetComponent<Light>();
        rand = new System.Random(seed);
        PlaceTail(); 
    }
	
    private void PlaceTail()
    {
        if(this.gameObject.transform.parent.name == "Head")
        {
            this.gameObject.transform.Rotate(0, 180, 0);
        }
    }

	// Update is called once per frame
	void Update () {
        totalTime += Time.deltaTime;

        if (DstBones.Length > 0)
        {
            Vector2 pos2D = DstBones[0].Origin;
            this.transform.position = new Vector3(pos2D.x, this.transform.position.y, pos2D.y);
        }
        else if (SrcBone != null) {
            Vector2 pos2D = SrcBone.End;
            this.transform.position = new Vector3(pos2D.x, this.transform.position.y, pos2D.y);
        }

        if (totalTime > lastTime + delta)
        {
            UpdateTarget();
        }
        UpdateLight();

        UpdateRotation(); 

        LerpLight();
	}

    private void UpdateRotation()
    {
        if (SrcBone != null)
        {
            gameObject.transform.Rotate(new Vector3(0, SrcBone.transform.rotation.y /2, 0));
        }
       
    }

    private void LerpLight()
    {
        color = Color.Lerp(srcColor, trgColor, (totalTime - lastTime) / delta);
        intensity = Mathf.Lerp(srcIntensity, trgIntensity, (totalTime - lastTime) / delta);
    }

    private void UpdateLight()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_EmissionColor", color * intensity);
            pointLight.color = color;
            pointLight.intensity = intensity;
            
        }
    }

    private void UpdateTarget()
    {
        srcColor = trgColor;
        srcIntensity = trgIntensity;

        lastTime = totalTime;
        float r = 0f;
        if (rand.NextDouble() > 0.5f)
        {
            r = 1f;
        }
        float g = 0f;
        if (rand.NextDouble() > 0.5f)
        {
            g = 1f;
        }
        float b = 0f;
        if (rand.NextDouble() > 0.5f)
        {
            b = 1f;
        }
        //trgColor = new Color(r, g, b);
        //trgIntensity = (float) rand.NextDouble() + 1f;
    }
}
