using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linking : MonoBehaviour {

    public GameObject AuroraPlanePrefab;
    GameObject AuroraPlanePrefabClone;
    public Material material;

    public GameObject objectA;
    public GameObject objectB;

    Vector3 pointA;
    Vector3 pointB;
    Vector3 instantiatePosition;

    private void Creation()
    {
        pointA = objectA.transform.position;
        pointB = objectB.transform.position;
        //Get the position
        instantiatePosition = (pointB + pointA) / 2;

        //Instantiate the object
        float angle = Mathf.Atan2(pointA.z - pointB.z, pointA.x - pointB.x) * 180 / Mathf.PI;
        AuroraPlanePrefabClone = Instantiate(AuroraPlanePrefab, instantiatePosition, Quaternion.Euler(0, -angle, 0));
        StartCoroutine(FadeTo(3.0f, 4.0f));
        StartCoroutine(ScaleTo(pointA, pointB,4));
    }

    private void Start()
    {
        Creation();
    }

    // Update is called once per frame
    void Update() {
        if (AuroraPlanePrefabClone == null)
        {
            if (Input.GetKeyDown("a"))
                    {
                        Creation();
                    }
        }
        else
        {
            if (Input.GetKeyDown("z"))
            {
                Color new_color = new Color(0, 0.9172413f, 1, 1);
                StartCoroutine(Set_Wave_Strength(0.7f, 2.0f));
                StartCoroutine(LerpColor(new_color));
            }
            if (Input.GetKeyDown("e"))
            {
                Color new_color = new Color(0.9172413f, 0, 0, 1);
                StartCoroutine(Set_Wave_Strength(0.2f, 2.0f));
                StartCoroutine(LerpColor(new_color));


            }
            if (Input.GetKeyDown("d"))
            {
                   Destroy(AuroraPlanePrefabClone, 0);   
            }
            if (Input.GetKeyDown("t"))
            {
                StartCoroutine(FadeTo(0.0f, 1.0f));
            }
            if (Input.GetKeyDown("f"))
            {
                StartCoroutine(FadeTo(3.0f, 1.0f));
            }

            if ((pointA != objectA.transform.position || pointB != objectB.transform.position)) {
                pointA = objectA.transform.position;
                pointB = objectB.transform.position;
                float angle = Mathf.Atan2(pointA.z - pointB.z, pointA.x - pointB.x) * 180 / Mathf.PI;
                AuroraPlanePrefabClone.transform.position = (pointB + pointA) / 2;
                AuroraPlanePrefabClone.transform.rotation = Quaternion.Euler(0, -angle, 0);

                AuroraPlanePrefabClone.transform.localScale = AuroraPlanePrefab.transform.localScale * (Vector3.Distance(pointA, pointB) / 21.1f);
            }
        }
    }

    IEnumerator ScaleTo(Vector3 startPoint, Vector3 endPoint, float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Vector3 pointC = Vector3.Lerp(startPoint, endPoint, t);
            AuroraPlanePrefabClone.transform.position = (pointC+ pointA) / 2;
            AuroraPlanePrefabClone.transform.localScale = AuroraPlanePrefab.transform.localScale * (Vector3.Distance(pointA, pointC) / 21.1f);

            yield return null;
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = AuroraPlanePrefabClone.GetComponent<Renderer>().material.GetFloat("_Emissive_Strength");
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            AuroraPlanePrefabClone.GetComponent<Renderer>().material.SetFloat("_Emissive_Strength", Mathf.Lerp(alpha, aValue, t));
            //if((t + Time.deltaTime / aTime) > 1.0f) Destroy(AuroraPlanePrefabClone, 0);
            yield return null;
        }
    }

    IEnumerator Set_Wave_Strength(float aValue, float aTime)
    {
        float alpha = AuroraPlanePrefabClone.GetComponent<Renderer>().material.GetFloat("_VO_STRENGTH");
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            AuroraPlanePrefabClone.GetComponent<Renderer>().material.SetFloat("_VO_STRENGTH", Mathf.Lerp(alpha, aValue, t));
            yield return null;
        }
    }

    IEnumerator LerpColor(Color new_color)
    {
        float duration = 1.5f; // This will be your time in seconds.
        float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.

        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        Color previous_color = AuroraPlanePrefabClone.GetComponent<Renderer>().material.GetColor("_Color_1");
        while (progress < 1)
        {
            AuroraPlanePrefabClone.GetComponent<Renderer>().material.SetColor("_Color_1", Color.Lerp(previous_color, new_color, progress));
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }

}
