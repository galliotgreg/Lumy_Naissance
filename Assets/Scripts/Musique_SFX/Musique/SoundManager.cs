using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    #region Audio Source
    public AudioSource lumyFxSource;
    public AudioSource inGameSource;

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
    public AudioClip[] mainThemeClips; // Menu Principal, Partie Perso, Option
    public AudioClip[] glossaireThemeClips;
    public AudioClip[] editorThemeClips; // MC, Nuee
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
    public AudioClip[] startGameClips;
    public AudioClip[] lumySelectedClips;
    public AudioClip[] prysmeIsAttackedClips;    
    public AudioClip[] swarmIsAttackedClips;

    public AudioClip[] victoryResourcesClips;
    public AudioClip[] victoryPrysmeDestroyedClips;
    public AudioClip[] defeatResourcesClips;
    public AudioClip[] defeatPrysmeDestroyedClips;

    public AudioClip[] lumyMovementClips;
    public AudioClip[] lumyAttackClips;
    public AudioClip[] lumyPickupClips;
    public AudioClip[] lumyDeathClips;
        //Menu
    public AudioClip[] onOverButtonClips;
    public AudioClip[] onClickButtonClips;
    public AudioClip[] onLoadingSceneClips; // Sound for the transition between 2 menus
    #endregion


    void Awake() {
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
        menuFxSource.clip = mainThemeClips[0];                
        menuFxSource.Play();

        StartCoroutine(WaitAndPlay(mainThemeClips[0], mainThemeClips[1], musicSource));
        musicSource.loop = true;   
    }
    public void PlayGlossaireTheme()
    {
        RandomizeClips(glossaireThemeClips, musicSource);
    }
    public void PlayEditorTheme()
    {
        RandomizeClips(editorThemeClips, musicSource);
    }
    public void PlayInGameMap1Theme()
    {
        RandomizeClips(inGameMap1ThemeClips, musicSource);
    }
    public void PlayInGameMap2Theme()
    {
        RandomizeClips(inGameMap2ThemeClips, musicSource);
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
    public void PlayStartGameSFX()
    {
        RandomizeClips(startGameClips, inGameSource, true, true);
    }
    public void PlayLumySelectedSFX()
    {
        RandomizeClips(lumySelectedClips, lumyFxSource, true, true);
    }
    public void PlayPrysmeIsAttackedSFX()
    {
        RandomizeClips(prysmeIsAttackedClips, inGameSource, true, true);
    }
    public void PlaySwarmIsAttackedSFX()
    {
        RandomizeClips(swarmIsAttackedClips, inGameSource, true, true);
    }
    public void PlayVictoryResourcesSFX()
    {
        RandomizeClips(victoryResourcesClips, menuFxSource, true, true);
    }
    public void PlayVictoryPrysmeDestroyedSFX()
    {
        RandomizeClips(victoryPrysmeDestroyedClips, menuFxSource, true, true);
    }
    public void PlayDefeatResourcesSFX()
    {
        RandomizeClips(defeatResourcesClips, menuFxSource, true, true);
    }
    public void PlayDefeatPrysmeDestroyedSFX()
    {
        RandomizeClips(defeatPrysmeDestroyedClips, menuFxSource, true, true);
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
    public void PlayOnOverButtonSFX()
    {
        RandomizeClips(onOverButtonClips, menuFxSource, true, true);
    }
    public void PlayOnClickButtonSFX()
    {
        RandomizeClips(onClickButtonClips, menuFxSource, true, true);
    }
    public void PlayOnLoadingSceneSFX()
    {
        RandomizeClips(onLoadingSceneClips, menuFxSource, true, true);
    }
    #endregion

    #endregion

    public void RandomizeClips(AudioClip[] clips,  AudioSource aSource, bool isPlayedOneTime = false , bool isPitched = false)
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

    private IEnumerator WaitAndPlay(AudioClip clip, AudioClip waitingClip, AudioSource source)
    {
        yield return new WaitForSeconds(clip.length);
        source.clip = waitingClip;
        source.Play();
    }
}
