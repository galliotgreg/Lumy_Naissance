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

    private string lastPanelLoaded = null;
    private List<string> previousPanel = new List<string>();

    public float zoomEndDistance = 0.0f;
    public float zoomStep = 0.1f;
    public float fadeStep = 0.05f;

    private bool layerUnloaded = true;
    private bool layerLoaded = true;
    private bool addToPreviousList = true;
    private bool sceneLoaded = false;
    private bool zoomOnCanvas = true;

    // Use this for initialization
    void Start () {
        StartCoroutine(InitSceneLayers());
    }

    public string GetCurrentScene()
    {
        return this.currentScene;
    }
    public Camera GetCurrentCamera()
    {
        return this.camera;
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

    public void SwapScenesWithoutZoom(string nextScene)
    {
        zoomOnCanvas = false;
        StartCoroutine(SwapScenesCo(nextScene, new Vector3(0,0,0)));
    }

    public void SwapScenesWithPanel(string nextScene, string panelToActivate, Vector3 sightPoint)
    {
        sceneLoaded = false;
        StartCoroutine(SwapScenesCo(nextScene, sightPoint));
        StartCoroutine(ActivatePanelCo(nextScene, panelToActivate));
    }

    public void GoBack(Vector3 sightPoint)
    {
        // Charger et dépiler la dernière scène de l'historique
        addToPreviousList = false;
        sceneLoaded = false;
        string sceneToGoBackTo = previousScene[previousScene.Count - 1];
        StartCoroutine(SwapScenesCo(sceneToGoBackTo, sightPoint));
        previousScene.RemoveAt(previousScene.Count - 1);

        // Activer et dépiler le dernier volet de l'historique
        if (previousPanel[previousPanel.Count-1] != null)
        {
            StartCoroutine(ActivatePanelCo(sceneToGoBackTo, previousPanel[previousPanel.Count - 1]));
        }
        previousPanel.RemoveAt(previousPanel.Count - 1);

    }

    IEnumerator SwapScenesCo(string nextScene, Vector3 sightPoint)
    {

        GameObject root = SceneManager.GetSceneByName(currentScene).GetRootGameObjects()[0];

        // Zoomer sur le bouton
        GameObject canvas = GameObject.Find(currentScene + "Canvas");
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (zoomOnCanvas)
        {
            Vector3 dir = (canvas.transform.position - camera.transform.position).normalized;
            while (Vector3.Dot(dir, canvas.transform.position - camera.transform.position) > zoomEndDistance)
            {
                Vector3 towards = Vector3.MoveTowards(
                    canvas.transform.position,
                    camera.transform.position + canvas.transform.position - sightPoint,
                    zoomStep);
                canvas.transform.position = towards;
                canvas.GetComponent<CanvasGroup>().alpha -= fadeStep;
                yield return true;
            }
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
            layerUnloaded = false;
            StartCoroutine(SwapLayersCo(nextScene, newLayer));
        }

        while (!layerUnloaded)
        {
            yield return null;
        }

        SceneManager.GetSceneByName(nextScene).GetRootGameObjects()[0].SetActive(true);

        findPriorityCamera();

        while (!layerLoaded)
        {
            yield return null;
        }

        

        // Mettre à jour les propriétés du gestionnaire
        if (addToPreviousList)
        {
            previousScene.Add(currentScene);
            previousPanel.Add(lastPanelLoaded);
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

        // Mettre à jour la caméra du canvas
        canvas.GetComponent<Canvas>().worldCamera = camera;

        // Mettre à jour les indicateurs
        addToPreviousList = true;
        lastPanelLoaded = null;
        sceneLoaded = true;
        zoomOnCanvas = true;

        // Arrêter la coroutine de transition
        StopCoroutine(SwapScenesCo(nextScene, sightPoint));
        yield return true;
    }

    IEnumerator SwapLayersCo(string nextScene, string newLayer)
    {

        // Fondre au noir
        GameObject darkScreen = GameObject.Find("DarkScreen");
        Image darkImg = darkScreen.GetComponent<Image>();
        float darkAlpha = darkImg.color.a;
        while (darkAlpha < 1.0f)
        {
            darkAlpha = darkScreen.GetComponent<Image>().color.a;
            darkScreen.GetComponent<Image>().color = new Color(darkImg.color.r, darkImg.color.g, darkImg.color.b, darkImg.color.a + fadeStep);
            yield return true;
        }

        // Attendre la fin du déchargement de la strate-mère initiale
        AsyncOperation unloadLayer = SceneManager.UnloadSceneAsync(currentLayer);
        while (!unloadLayer.isDone)
        {
            yield return null;
        }

        layerUnloaded = true;

        // Attendre la fin du chargement de la strate-mère de destination
        AsyncOperation loadLayer = SceneManager.LoadSceneAsync(newLayer, LoadSceneMode.Additive);
        while (!loadLayer.isDone)
        {
            yield return null;
        }

        // Fondre depuis le noir
        while (darkAlpha > 0f)
        {
            darkAlpha = darkScreen.GetComponent<Image>().color.a;
            darkScreen.GetComponent<Image>().color = new Color(darkImg.color.r, darkImg.color.g, darkImg.color.b, darkImg.color.a - fadeStep);
            yield return true;
        }

        // Mettre à jour la strate courante
        currentLayer = newLayer;

        layerLoaded = true;

        // Arrêter la coroutine
        StopCoroutine(SwapLayersCo(nextScene, newLayer));
        yield return true;
    }

    IEnumerator ActivatePanelCo(string nextScene, string panelName)
    {
        // Attendre que la scène soit entièrement chargée
        yield return new WaitUntil(() => sceneLoaded);

        // Récupérer le canvas de la scène cible et le panel spécifié dusit canvas
        GameObject canvas = GameObject.Find(nextScene + "Canvas");
        GameObject panel = GameObject.Find(nextScene+"Canvas").transform.Find(panelName).gameObject;


        // Faire apparaître le panel en fondu
        canvas.GetComponent<CanvasGroup>().alpha = 0f;
        panel.SetActive(true);
        float alphaLayer = canvas.GetComponent<CanvasGroup>().alpha;
        while (alphaLayer < 1.0f)
        {
            alphaLayer = canvas.GetComponent<CanvasGroup>().alpha;
            canvas.GetComponent<CanvasGroup>().alpha += fadeStep;
            yield return true;
        }

        // Mettre à jour le dernier panel chargé
        lastPanelLoaded = panelName;

        // Arrêter la coroutine
        StopCoroutine(ActivatePanelCo(nextScene, panelName));
        yield return true;
    }

    void findPriorityCamera()
    {
        // Trouver la caméra prioritaire
        Camera[] camList = Camera.allCameras;
        foreach (Camera c in camList)
        {
            if (Camera.allCamerasCount >= 2)
            {
                if (c.name != "Main Camera")
                {
                    camera.GetComponent<AudioListener>().enabled = false;
                    camera = c;
                    camera.GetComponent<AudioListener>().enabled = true;
                }
            }
            else
            {
                camera = c;
                camera.GetComponent<AudioListener>().enabled = true;
            }
        }
    }
}
