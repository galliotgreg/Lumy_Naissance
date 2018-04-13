using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    #region Audio Source
    public AudioSource lumyFxSource;
    public AudioSource inGameFXSource;

    public AudioSource menuFxSource;

    public AudioSource musicSource;
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
    public AudioClip[] LoadingMcClips;
    
    //InGame
    public AudioClip[] playGameClips;
    public AudioClip[] pauseGameClips;

    public AudioClip[] lumySelectedClips;
    public AudioClip[] prysmeSelectedClips;
    public AudioClip[] prysmeIsAttackedClips;
    public AudioClip[] swarmIsAttackedClips;

    public AudioClip[] victoryResourcesClips;
    public AudioClip[] victoryPrysmeDestroyedClips;

    public AudioClip[] lumyMovementClips;
    public AudioClip[] lumyAttackClips;
    public AudioClip[] lumyPickupClips;
    public AudioClip[] lumyDeathClips;
    
    //Menu
    public AudioClip[] onHoverMenuClips;
    public AudioClip[] onClickMenuClips;
    public AudioClip[] onLoadingSceneClips; // Sound for the transition between 2 menus
    public AudioClip[] addStatSwarmClips;
    public AudioClip[] removeStatSwarmClips;

    //General
    public AudioClip[] onHoverButtonClips;
    public AudioClip[] onClickButtonClips;
    #endregion


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

        musicSource.clip = menuprincipalThemeClips[0];
        musicSource.Play();

        StartCoroutine(WaitAndPlay(menuprincipalThemeClips[0], menuprincipalThemeClips[1], musicSource, thisScene));
    }

    public void PlayPartiePersoTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;

        musicSource.clip = partiepersoThemeClips[0];
        musicSource.Play();

        StartCoroutine(WaitAndPlay(partiepersoThemeClips[0], partiepersoThemeClips[1], musicSource, thisScene));
    }

    public void PlayOptionsTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;

        musicSource.clip = optionsThemeClips[0];
        musicSource.Play();

        StartCoroutine(WaitAndPlay(optionsThemeClips[0], optionsThemeClips[1], musicSource, thisScene));
    }

    public void PlayGlossaireTheme()
    {
        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;

        musicSource.clip = glossaireThemeClips[0];
        musicSource.Play();

        StartCoroutine(WaitAndPlay(glossaireThemeClips[0], glossaireThemeClips[1], musicSource, thisScene));
    }

    public void PlayEditorTheme()
    {
        // Ajouter if scene d'avant != MC/Nuée, faire la fonction, sinon ne rien faire

        string thisScene = NavigationManager.instance.GetCurrentScene();

        musicSource.loop = false;

        musicSource.clip = editorThemeClips[0];
        musicSource.Play();

        StartCoroutine(WaitAndPlay(editorThemeClips[0], editorThemeClips[1], musicSource, thisScene));
    }

    public void PlayCreditsTheme()
    {
        musicSource.loop = true;

        musicSource.clip = optionCreditThemeClips[0];
        musicSource.Play();
    }

    public void PlayInGameMap1Theme()   // Ajouter choix 3 5 7 min
    {
        musicSource.loop = true;

        musicSource.clip = inGameMap1ThemeClips[0];
        musicSource.Play();
    }
    public void PlayInGameMap2Theme()   // Ajouter choix 3 5 7 min  +  Changer pour ingame2
    {
        musicSource.loop = true;

        musicSource.clip = inGameMap1ThemeClips[0];
        musicSource.Play();
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
        RandomizeClips(popUpDialogueClips, menuFxSource, true, true);
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
    public void PlayLoadingMcSFX()
    {
        RandomizeClips(LoadingMcClips, menuFxSource, true, true);
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
    public void PlayVictoryPrysmeDestroyedSFX()
    {
        RandomizeClips(victoryPrysmeDestroyedClips, menuFxSource, true, true);
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
        RandomizeClips(onHoverMenuClips, menuFxSource, true, true);
    }
    public void PlayOnClickMenuSFX()
    {
        RandomizeClips(onClickMenuClips, menuFxSource, true, true);
    }
    public void PlayOnLoadingSceneSFX()
    {
        RandomizeClips(onLoadingSceneClips, menuFxSource, true, true);
    }
    public void PlayAddSwarmSFX()
    {
        RandomizeClips(addStatSwarmClips, menuFxSource, true, true);
    }
    public void PlayRemoveSwarmSFX()
    {
        RandomizeClips(removeStatSwarmClips, menuFxSource, true, true);
    }

    #endregion

    #region General    
    public void PlayOnHoverButtonSFX()
    {
        RandomizeClips(removeStatSwarmClips, menuFxSource, true, true);
    }    
    public void PlayOnClickButtonSFX()
    {
        RandomizeClips(removeStatSwarmClips, menuFxSource, true, true);
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

    private IEnumerator WaitAndPlay(AudioClip clip, AudioClip waitingClip, AudioSource source, string initialScene)
    {
        yield return new WaitForSeconds(clip.length);

        string currentScene = NavigationManager.instance.GetCurrentScene();

        if (currentScene == initialScene)
        {
            source.clip = waitingClip;
            source.Play();
            source.loop = true;
        }
    }
}
