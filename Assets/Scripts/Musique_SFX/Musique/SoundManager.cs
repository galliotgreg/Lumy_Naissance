using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public bool oneIsMain = true;
    public float fadeOutFactor = 1f;
    public float fadeInFactor = 100f;
    public float volumeJoueur;
    string currentScene;
    List<string> previousScene;

    #region Audio Source
    public AudioSource lumyFxSource;
    public AudioSource inGameFXSource;

    public AudioSource menuFxSource;

    public AudioSource musicSource;
    public AudioSource musicSource2;
    #endregion

    public static SoundManager instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.95f;

    #region AudioClip
    /*** Musics ***/

    //Menu
    public AudioClip[] introThemeClips;
    public AudioClip[] mainThemeClips; // Vidé (si j'essaie de le supprimer ça fait poper une erreur)
    public AudioClip[] menuprincipalThemeClips; // Menu Principal
    public AudioClip[] partiepersoThemeClips; // Partie Perso
    public AudioClip[] optionsThemeClips; // Options
    public AudioClip[] glossaireThemeClips; // Glossaire
    public AudioClip[] editorThemeClips; // MC, Nuee
    public AudioClip[] optionCreditThemeClips; // Crédits
    //InGame
    public AudioClip[] inGameMap1ThemeClips;
    public AudioClip[] inGameMap2ThemeClips;    

    /*** FX ***/

    //Tuto
    public AudioClip[] hintClips; // Sound for helping player during tuto
    public AudioClip[] popUpDialogueClips;

    //MC Editor
    public AudioClip[] selectNodeClips;
    public AudioClip[] transitionOKClips; // Creation of a valid transition
    
    //InGame
    public AudioClip[] playGameClips;
    public AudioClip[] pauseGameClips;

    public AudioClip[] lumySelectedClips;
    public AudioClip[] prysmeSelectedClips;
    public AudioClip[] prysmeIsAttackedClips;
    public AudioClip[] swarmIsAttackedClips;

    public AudioClip[] victoryResourcesClips;

    public AudioClip[] lumyMovementClips;
    public AudioClip[] lumyAttackClips;
    public AudioClip[] lumyPickupClips;
    public AudioClip[] lumyDeathClips;
    
    //Menu
    public AudioClip[] onHoverMenuClips;
    public AudioClip[] onClickMenuClips;
    public AudioClip[] addStatSwarmClips;
    public AudioClip[] removeStatSwarmClips;
    public AudioClip[] launchGame;

    //General
    public AudioClip[] onHoverButtonClips;
    public AudioClip[] onClickButtonClips;
    #endregion

    public bool isAllreadyPlaying = false;  

    private void Start()
    {
        inGameFXSource.volume = SwapManager.instance.getPlayerPrefSFX();
        lumyFxSource.volume = SwapManager.instance.getPlayerPrefSFX();
        menuFxSource.volume = SwapManager.instance.getPlayerPrefSFX();

        musicSource.volume = SwapManager.instance.getPlayerPrefMusic();
        musicSource2.volume = SwapManager.instance.getPlayerPrefMusic();
        volumeJoueur = SwapManager.instance.getPlayerPrefMusic();
    }
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //DontDestroyOnLoad(gameObject);
    }

    #region Music Functions
    public void PlayIntroTheme()
    {
        RandomizeClips(introThemeClips, musicSource);
    }

    public void PlayMainTheme()
    {
        //RandomizeClips(mainThemeClips, musicSource);
        
        musicSource.loop = true;
    }

    public void PlayMenuPrincipalTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;
        musicSource2.loop = false;

        SwapTracks(menuprincipalThemeClips[0]);

        StartCoroutine(WaitAndPlay(menuprincipalThemeClips[0], menuprincipalThemeClips[1], musicSource, musicSource2, thisScene));
    }

    public void PlayPartiePersoTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;
        musicSource2.loop = false;

        SwapTracks(partiepersoThemeClips[0]);

        StartCoroutine(WaitAndPlay(partiepersoThemeClips[0], partiepersoThemeClips[1], musicSource, musicSource2, thisScene));
    }

    public void PlayOptionsTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;
        musicSource2.loop = false;

        SwapTracks(optionsThemeClips[0]);
        
        StartCoroutine(WaitAndPlay(optionsThemeClips[0], optionsThemeClips[1], musicSource, musicSource2, thisScene));
    }

    public void PlayGlossaireTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;
        musicSource2.loop = false;
        
        SwapTracks(glossaireThemeClips[0]);

        StartCoroutine(WaitAndPlay(glossaireThemeClips[0], glossaireThemeClips[1], musicSource, musicSource2, thisScene));
    }

    public void PlayEditorTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;
        musicSource2.loop = false;

        SwapTracks(editorThemeClips[0]);

        StartCoroutine(WaitAndPlay(editorThemeClips[0], editorThemeClips[1], musicSource, musicSource2, thisScene));
    }

    public void PlayCreditsTheme()
    {
        musicSource.loop = true;
        musicSource2.loop = true;

        SwapTracks(optionCreditThemeClips[0]);
    }

    public void PlayInGameMap1Theme()
    {
        musicSource.loop = true;
        musicSource2.loop = true;

        SwapTracks(inGameMap1ThemeClips[0]);
    }
    public void PlayInGameMap2Theme()
    {
        musicSource.loop = true;
        musicSource2.loop = true;

        SwapTracks(inGameMap1ThemeClips[0]);
    }
    
    #endregion

    #region SFX Functions   

    #region Tuto SFX
    public void PlayHintSFX()
    {
        RandomizeClips(hintClips, menuFxSource, true, true);
    }
    public void PlayPopUpDialogueSFX()
    {
        RandomizeClips(popUpDialogueClips, menuFxSource);
    }
    #endregion

    #region MCEditor SFX
    public void PlaySelectNodeSFX()
    {
        RandomizeClips(selectNodeClips, menuFxSource, true, true);
    }
    public void PlayTransitionOKSFX()
    {
        RandomizeClips(transitionOKClips, menuFxSource, true, true);
    }
    #endregion

    #region InGame SFX
    public void PlayPlayGameSFX()
    {
        RandomizeClips(playGameClips, inGameFXSource, true, true);
    }
    public void PlayLumySelectedSFX()
    {
        RandomizeClips(lumySelectedClips, lumyFxSource, true, true);
    }
    public void PlayPrysmeSelectedSFX()
    {
        RandomizeClips(prysmeSelectedClips, lumyFxSource, true, true);
    }
    public void PlayPrysmeIsAttackedSFX()
    {
        RandomizeClips(prysmeIsAttackedClips, inGameFXSource, true, true);
    }
    public void PlaySwarmIsAttackedSFX()
    {
        RandomizeClips(swarmIsAttackedClips, inGameFXSource, true, true);
    }
    public void PlayVictoryResourcesSFX()
    {
        RandomizeClips(victoryResourcesClips, menuFxSource, true, true);
    }
    public void PlayLumyMovementSFX()
    {
        RandomizeClips(lumyMovementClips, lumyFxSource, true, true);
    }
    public void PlayLumyAttackSFX()
    {
        RandomizeClips(lumyAttackClips, lumyFxSource, true, true);
    }
    public void PlayLumyPickupSFX()
    {
        RandomizeClips(lumyPickupClips, lumyFxSource, true, true);
    }
    public void PlayLumyDeathSFX()
    {
        RandomizeClips(lumyDeathClips, lumyFxSource, true, true);
    }
    #endregion

    #region Menu SFX
    public void PlayOnHoverMenuSFX()
    {
        RandomizeClips(onHoverMenuClips, menuFxSource, true);
    }
    public void PlayOnClickMenuSFX()
    {
        RandomizeClips(onClickMenuClips, menuFxSource, true);
    }
    public void PlayAddSwarmSFX()
    {
        RandomizeClips(addStatSwarmClips, menuFxSource, true);
    }
    public void PlayRemoveSwarmSFX()
    {
        RandomizeClips(removeStatSwarmClips, menuFxSource, true);
    }
    public void PlayLaunchGameSFX()
    {
        RandomizeClips(launchGame, menuFxSource, true);
    }

    #endregion

    #region General    
    public void PlayOnHoverButtonSFX()
    {
        RandomizeClips(onHoverButtonClips, menuFxSource, true, false);
    }    
    public void PlayOnClickButtonSFX()
    {
        RandomizeClips(onClickButtonClips, menuFxSource);
    }
    #endregion

    #region Options
    public void PlayFeebackSFXVolumeSFX()
    {
        RandomizeClips(addStatSwarmClips, menuFxSource, true);
    }
    #endregion
    #endregion

    public void RandomizeClips(AudioClip[] clips, AudioSource aSource, bool isPlayedOneTime = false, bool isPitched = false)
    {
        int randomIndex = Random.Range(0, clips.Length);

        if (isPitched)
        {
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);
            aSource.pitch = randomPitch;
        }

        aSource.clip = clips[randomIndex];

        if (isPlayedOneTime)
        {
            aSource.PlayOneShot(clips[randomIndex]);
        }
        else
        {
            aSource.Play();
        }

    }

    private IEnumerator WaitAndPlay(AudioClip clip, AudioClip waitingClip, AudioSource source1, AudioSource source2, string initialScene)
    {
        yield return new WaitForSeconds(clip.length);

        string currentScene = NavigationManager.instance.GetCurrentScene();

        AudioSource source = new AudioSource();

        if (oneIsMain)
        {
            source = source1;
        }
        if (!oneIsMain)
        {
            source = source2;
        }

        if (currentScene == initialScene)
        {
            source.clip = waitingClip;
            source.Play();
            source.loop = true;
        }
    }

    void SwapTracks(AudioClip clip)
    {
        AudioSource trackToFade = new AudioSource();
        AudioSource trackNewMain = new AudioSource();

        if (oneIsMain)
        {
            trackToFade = musicSource;
            trackNewMain = musicSource2;
        }
        if (!oneIsMain)
        {
            trackToFade = musicSource2;
            trackNewMain = musicSource;
        }


        trackNewMain.clip = clip;
        trackNewMain.Play();

        oneIsMain = !oneIsMain;

        StartCoroutine(FadeIn(trackNewMain, fadeInFactor));
        StartCoroutine(FadeTheFuckOut(trackToFade, fadeOutFactor));
    }

    //FADE OUT
    public IEnumerator FadeTheFuckOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = volumeJoueur;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0f;
    }

    //FADE IN
    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume = 0.01f;
        float startVolume = audioSource.volume;

        while (audioSource.volume < volumeJoueur)
        {
            audioSource.volume += startVolume * Time.deltaTime * FadeTime;

            yield return null;
        }

        audioSource.volume = volumeJoueur;
    }

    public void MusicOnScene()
    {
        currentScene = NavigationManager.instance.GetCurrentScene();
        previousScene = NavigationManager.instance.GetPreviousScene();

        switch (currentScene)
        {
            case "MenuPrincipalScene":
                PlayMenuPrincipalTheme();
                break;
            case "PartiePersoScene":
                PlayPartiePersoTheme();
                break;
            case "EditeurCastesScene":
                string sceneAvant = previousScene[previousScene.Count - 1];
                if (sceneAvant != "EditeurMCScene")
                {
                    PlayEditorTheme();
                }
                break;
            case "EditeurMCScene":
                string sceneAvant2 = previousScene[previousScene.Count - 1];
                if (sceneAvant2 != "EditeurCastesScene")
                {
                    PlayEditorTheme();
                }
                break;
            case "OptionScene":
                PlayOptionsTheme();
                break;
            case "GlossaireScene":
                PlayGlossaireTheme();
                break;
            case "MapTutoInteResized":
                PlayInGameMap1Theme();
                break;
            case "Map2.1":
                PlayInGameMap2Theme();
                break;
            default:
                Debug.Log("PAS DE SCENE ?!");
                break;
        }
    }

}
