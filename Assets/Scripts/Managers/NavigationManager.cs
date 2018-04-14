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
    private int wCount = 0;

    private bool layerUnloaded = true;
    private bool layerLoaded = true;
    private bool addToPreviousList = true;
    private bool sceneLoaded = false;
    private bool zoomOnCanvas = true;
    private bool fadeToBlack = false;

    // loading text
    bool isDots1 = true;
    bool isDots2 = false;
    bool isDots3 = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(InitSceneLayers());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            wCount++;
    }

    public string GetCurrentScene()
    {
        return this.currentScene;
    }
    public Camera GetCurrentCamera()
    {
        if(this.camera != null)
        {
            return this.camera;
        }
        this.camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        return this.camera; 
    }


    IEnumerator InitSceneLayers()
    {
        GameObject darkScreen = GameObject.Find("DarkScreen");
        Image darkImg = darkScreen.GetComponent<Image>();
        float darkAlpha = darkImg.color.a;

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

        // Fondre depuis le noir
        while (darkAlpha > 0f)
        {
            darkAlpha = darkScreen.GetComponent<Image>().color.a;
            darkScreen.GetComponent<Image>().color = new Color(darkImg.color.r, darkImg.color.g, darkImg.color.b, darkImg.color.a - fadeStep);
            yield return true;
        }

        currentScene = initialScene;
        currentLayer = layerToLoad;

    }

    public void ActivateFadeToBlack()
    {
        fadeToBlack = true;
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

    private string dotLoadingText()
    {
        string dots1 = "<color=grey>.</color>..";
        string dots2 = ".<color=grey>.</color>.";
        string dots3 = "..<color=grey>.</color>";

        if (isDots1)
        {
            isDots1 = false;
            isDots2 = true;
            isDots3 = false;

            return dots1;
        }
        else if (isDots2)
        {
            isDots1 = false;
            isDots2 = false;
            isDots3 = true;

            return dots2;
        }
        else if (isDots3)
        {
            isDots1 = true;
            isDots2 = false;
            isDots3 = false;

            return dots3;
        } else
        {
            return "";
        }
    }

    IEnumerator SwapScenesCo(string nextScene, Vector3 sightPoint)
    {
        GameObject root = SceneManager.GetSceneByName(currentScene).GetRootGameObjects()[0];
        
        // Faire disparaître le canvas en fondu
        GameObject canvas = GameObject.Find(currentScene + "Canvas");
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        /*float alpha = canvas.GetComponent<CanvasGroup>().alpha;
        while (alpha > 0.0f)
        {
            alpha = canvas.GetComponent<CanvasGroup>().alpha;
            canvas.GetComponent<CanvasGroup>().alpha -= fadeStep;
            yield return true;
        }*/

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

        // Afficher le texte de chargement        
        GameObject loadText = GameObject.Find("LoadingText");

        setWText(loadText);        
        loadText.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        loadText.GetComponent<Text>().text = "Chargement";
        

        // Attendre la fin du chargement de la scène de destination

        AsyncOperation load = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            loadText.GetComponent<Text>().text = "Chargement" + dotLoadingText();
            yield return null;
        }

        //Switch the Active Scene if it's InGame To apply New Lighting settings 
        if (nextScene == "MapTutoInteResized")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
        }
        else if (nextScene == "Map2.1")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("BasicLayer"));
        }


        canvas = GameObject.Find(nextScene + "Canvas");
        //canvas.SetActive(false);

        // Désactiver l'interactabilité du nouveau canvas
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        // Attendre la fin du déchargement de la scène initiale
        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentScene);
        while (!unload.isDone)
        {
            loadText.GetComponent<Text>().text = "Chargement" + dotLoadingText();
            yield return null;
        }

        // Vérifier la conservation de la strate-mère
        Scene scene = SceneManager.GetSceneByName(nextScene);
        GameObject rootNode = scene.GetRootGameObjects()[0];
        string newLayer = rootNode.GetComponent<SceneData>().parentLayer;
        if (newLayer != currentLayer)
        {
            layerLoaded = false;
            layerUnloaded = false;
            StartCoroutine(SwapLayersCo(nextScene, newLayer));
        }

        while (!layerUnloaded)
        {
            loadText.GetComponent<Text>().text = "Chargement" + dotLoadingText();
            yield return null;
        }

        findPriorityCamera();

        while (!layerLoaded)
        {
            loadText.GetComponent<Text>().text = "Chargement" + dotLoadingText();
            yield return null;
        }

        // Mettre à jour les propriétés du gestionnaire
        if (addToPreviousList)
        {
            previousScene.Add(currentScene);
            previousPanel.Add(lastPanelLoaded);
        }
        
        currentScene = nextScene;

        // Cacher le texte de chargement
        loadText.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        loadText.GetComponent<Text>().text = "Chargement";        

        // Fondre depuis le noir
        while (darkAlpha > 0f)
         {
            darkAlpha = darkScreen.GetComponent<Image>().color.a;
            darkScreen.GetComponent<Image>().color = new Color(darkImg.color.r, darkImg.color.g, darkImg.color.b, darkImg.color.a - fadeStep);            
            yield return true;
        }

        // Jouer la musique correspondant à la scène
        switch (currentScene)
        {
            case "MenuPrincipalScene":
                Debug.Log("Menu Principal !");
                SoundManager.instance.PlayMenuPrincipalTheme();
                break;
            case "PartiePersoScene":
                Debug.Log("Partie Personnalisée");
                SoundManager.instance.PlayPartiePersoTheme();
                break;
            case "EditeurCastesScene":
                Debug.Log("Editeur de Castes !");
                SoundManager.instance.PlayEditorTheme();
                break;
            case "EditeurMCScene":
                Debug.Log("Editeur de MC !");
                SoundManager.instance.PlayEditorTheme();
                break;
            case "OptionScene":
                Debug.Log("Options !");
                SoundManager.instance.PlayOptionsTheme();
                break;
            case "GlossaireScene":
                Debug.Log("Glossaire !");
                SoundManager.instance.PlayGlossaireTheme();
                break;
            case "MapTutoInteResized":
                Debug.Log("Map Tuto Inte Resized !");
                SoundManager.instance.PlayInGameMap1Theme();
                break;
            case "Map2.1":
                Debug.Log("Map 2 point 1");
                SoundManager.instance.PlayInGameMap2Theme();
                break;
            default:
                Debug.Log("PAS DE SCENE ?!");
                break;
        }

        // Faire apparaître le canvas en fondu
        /*canvas.GetComponent<CanvasGroup>().alpha = 0f;
        alpha = canvas.GetComponent<CanvasGroup>().alpha;

        canvas.SetActive(true);

        while (alpha < 1.0f)
        {
            alpha = canvas.GetComponent<CanvasGroup>().alpha;
            canvas.GetComponent<CanvasGroup>().alpha += fadeStep;
            yield return true;
        }*/

        // Réactiver l'interactabilité du nouveau canvas
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Mettre à jour la caméra du canvas
        canvas.GetComponent<Canvas>().worldCamera = camera;

        // Mettre à jour les indicateurs
        wCount = 0;
        addToPreviousList = true;
        lastPanelLoaded = null;
        sceneLoaded = true;
        zoomOnCanvas = true;
        fadeToBlack = false;

        // Arrêter la coroutine de transition
        StopCoroutine(SwapScenesCo(nextScene, sightPoint));
        yield return true;
    }

    IEnumerator SwapLayersCo(string nextScene, string newLayer)
    {

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

    void setWText(GameObject text)
    {
        if (wCount >= 29)
            text.GetComponent<Text>().text = "WAIT4BABA...";
    }

    void findPriorityCamera()
    {
        // Trouver la caméra prioritaire
        Camera[] camList = Camera.allCameras;
        
        if(camList.Length <=0)
        {
            Debug.LogError("No Camera Present In Scene");
            return; 
        }


        foreach (Camera c in camList)
        {
            if (c.tag == "MainCamera" && c.name != "Main Camera")
            {
                if (camera != null)
                {
                    camera.GetComponent<AudioListener>().enabled = false;
                }
                camera = c;
                camera.GetComponent<AudioListener>().enabled = true;
                return; 
            }
        }
        if(camera != null)
        {
            camera.GetComponent<AudioListener>().enabled = false;
        }
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera.GetComponent<AudioListener>().enabled = true;
    }
}
