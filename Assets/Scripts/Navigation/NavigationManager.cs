using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour {
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static NavigationManager instance = null;

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private string[] initSceneLayers;
    [SerializeField]
    private Camera camera;


    public float zoomEndDistance = 0.0f;
    public float zoomStep = 0.1f;
    public float fadeStep = 0.05f;

    // Use this for initialization
    void Start () {
        StartCoroutine(InitSceneLayers());
    }

    IEnumerator InitSceneLayers()
    {
        AsyncOperation loadLayer1 = SceneManager.LoadSceneAsync(initSceneLayers[1], LoadSceneMode.Additive);
        AsyncOperation loadLayer2 = SceneManager.LoadSceneAsync(initSceneLayers[2], LoadSceneMode.Additive);
        while (!loadLayer1.isDone)
        {
            yield return null;
        }
        while (!loadLayer2.isDone)
        {
            yield return null;
        }
    }

    public void SwapScenes(string curScene, string nextScene, int layer, Vector3 sightPoint)
    {
        StartCoroutine(ZoomIn(curScene, nextScene, layer, sightPoint));
    }

    IEnumerator ZoomIn(string curScene, string nextScene, int layer, Vector3 sightPoint)
    {
        GameObject root = SceneManager.GetSceneByName(curScene).GetRootGameObjects()[0];

        // Zoomer sur le bouton
        GameObject canvas = GameObject.Find(curScene + "Canvas");
        Vector3 dir = (root.transform.position - camera.transform.position).normalized;
        while (Vector3.Dot(dir, root.transform.position - camera.transform.position) > zoomEndDistance)
        {
            Vector3 towards = Vector3.MoveTowards(
                root.transform.position,
                camera.transform.position + root.transform.position - sightPoint,
                zoomStep);
            root.transform.position = towards;
            canvas.GetComponent<CanvasGroup>().alpha -= fadeStep;
            yield return true;
        }

        // Attendre la fin du déchargement de la scène initiale
        AsyncOperation unload = SceneManager.UnloadSceneAsync(curScene);
        while (!unload.isDone)
        {
            yield return null;
        }

        // Attendre la fin du chargement de la scène de destination
        AsyncOperation load = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }

        // Faire apparaître le canvas en fondu
        canvas = GameObject.Find(nextScene + "Canvas");
        canvas.GetComponent<CanvasGroup>().alpha = 0f;
        float alpha = canvas.GetComponent<CanvasGroup>().alpha;
        while (alpha < 1.0f)
        {
            alpha = canvas.GetComponent<CanvasGroup>().alpha;
            canvas.GetComponent<CanvasGroup>().alpha += fadeStep;
            yield return true;
        }

        // Arrêter la coroutine de transition
        StopCoroutine(ZoomIn(curScene, nextScene, layer, sightPoint));
        yield return true;
    }
}
