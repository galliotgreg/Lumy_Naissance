using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour {

    // The static instance of the Singleton for external access
    public static NavigationManager instance = null;
   
    // Enforce Singleton properties
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
    private string initialScene;
    [SerializeField]
    private Camera camera;
    
    private string currentLayer;
    private string currentScene;
    private List<string> previousScene = new List<string>();

    public float zoomEndDistance = 0.0f;
    public float zoomStep = 0.1f;
    public float fadeStep = 0.05f;

    private bool layerLoaded = true;
    private bool addToPreviousList = true;

    // Use this for initialization
    void Start () {
        StartCoroutine(InitSceneLayers());
    }

    IEnumerator InitSceneLayers()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(initialScene, LoadSceneMode.Additive);
        while (!loadScene.isDone)
        {
            yield return null;
        }
        string layerToLoad = SceneManager.GetSceneByName(initialScene).GetRootGameObjects()[0].GetComponent<SceneData>().parentLayer;
        AsyncOperation loadLayer = SceneManager.LoadSceneAsync(layerToLoad, LoadSceneMode.Additive);
        while (!loadLayer.isDone)
        {
            yield return null;
        }
        currentScene = initialScene;
        currentLayer = layerToLoad;

    }

    public void SwapScenes(string nextScene, Vector3 sightPoint)
    {
        StartCoroutine(SwapScenesCo(nextScene, sightPoint));
    }

    public void GoBack(Vector3 sightPoint)
    {
        addToPreviousList = false;
        StartCoroutine(SwapScenesCo(previousScene[previousScene.Count-1], sightPoint));
        previousScene.RemoveAt(previousScene.Count - 1);
    }

    IEnumerator SwapScenesCo(string nextScene, Vector3 sightPoint)
    {

        GameObject root = SceneManager.GetSceneByName(currentScene).GetRootGameObjects()[0];

        // Zoomer sur le bouton
        GameObject canvas = GameObject.Find(currentScene + "Canvas");
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
        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentScene);
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
        SceneManager.GetSceneByName(nextScene).GetRootGameObjects()[0].SetActive(false);

        // Vérifier la conservation de la strate-mère
        string newLayer = SceneManager.GetSceneByName(nextScene).GetRootGameObjects()[0].GetComponent<SceneData>().parentLayer;
        if (newLayer != currentLayer)
        {
            layerLoaded = false;
            StartCoroutine(SwapLayersCo(nextScene, newLayer));
        }
        while (!layerLoaded)
        {
            yield return null;
        }

        SceneManager.GetSceneByName(nextScene).GetRootGameObjects()[0].SetActive(true);

        // Mettre à jour les propriétés du gestionnaire
        if (addToPreviousList)
        {
            previousScene.Add(currentScene);
        }
        
        currentScene = nextScene;

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

        addToPreviousList = true;

        // Arrêter la coroutine de transition
        StopCoroutine(SwapScenesCo(nextScene, sightPoint));
        yield return true;
    }

    IEnumerator SwapLayersCo(string nextScene, string newLayer)
    {
        Debug.Log("Pingouin rentré");
        
        // Faire disparaître la strate-mère initiale en fondu 
        GameObject canvas = GameObject.Find(currentLayer + "Canvas");
        float alphaLayer = canvas.GetComponent<CanvasGroup>().alpha;
        while (alphaLayer > 0.0f)
        {
            alphaLayer = canvas.GetComponent<CanvasGroup>().alpha;
            canvas.GetComponent<CanvasGroup>().alpha -= fadeStep;
            yield return true;
        }

        // Attendre la fin du déchargement de la strate-mère initiale
        AsyncOperation unloadLayer = SceneManager.UnloadSceneAsync(currentLayer);
        while (!unloadLayer.isDone)
        {
            yield return null;
        }

        // Attendre la fin du chargement de la strate-mère de destination
        AsyncOperation loadLayer = SceneManager.LoadSceneAsync(newLayer, LoadSceneMode.Additive);
        while (!loadLayer.isDone)
        {
            yield return null;
        }

        // Faire apparaître la strate-mère de destination en fondu 
        canvas = GameObject.Find(newLayer + "Canvas");
        canvas.GetComponent<CanvasGroup>().alpha = 0f;
        alphaLayer = canvas.GetComponent<CanvasGroup>().alpha;
        while (alphaLayer < 1.0f)
        {
            alphaLayer = canvas.GetComponent<CanvasGroup>().alpha;
            canvas.GetComponent<CanvasGroup>().alpha += fadeStep;
            yield return true;
        }

        // Mettre à jour la strate courante
        currentLayer = newLayer;

        layerLoaded = true;

        // Arrêter la coroutine
        StopCoroutine(SwapLayersCo(nextScene, newLayer));
        yield return true;
    }
    
}
