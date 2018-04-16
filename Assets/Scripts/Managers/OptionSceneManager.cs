using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSceneManager : MonoBehaviour {
    public static OptionSceneManager instance = null;

    #region UI Variables
    #region Video
    [SerializeField]
    private Dropdown resolution;
    [SerializeField]
    private Toggle windowed;
    [SerializeField]
    private Dropdown quality;
    #endregion
    #region Audio
    [SerializeField]
    private Slider sfx;
    [SerializeField]
    private Slider general;
    [SerializeField]
    private Slider music;
    #endregion
    #endregion

    // Use this for initialization
    void Start () {
        windowed.isOn = Screen.fullScreen;
        quality.value = QualitySettings.GetQualityLevel();

        music.value = SoundManager.instance.musicSource.volume;
        sfx.value = SoundManager.instance.menuFxSource.volume;
        general.value = Mathf.Max(SoundManager.instance.musicSource.volume, SoundManager.instance.menuFxSource.volume);

        resolution.onValueChanged.AddListener(delegate { SetResolution(); });
        windowed.onValueChanged.AddListener((on) => { SetWindowed(); });
        quality.onValueChanged.AddListener(delegate{ SetQuality(); });

        music.onValueChanged.AddListener(delegate { SetVolumeMusic(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
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

    private void SetResolution()
    {
        switch (resolution.value)
        {
            case 0:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 1:
                windowed.isOn = !Screen.fullScreen;
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                break;
            case 2:
                windowed.isOn = !Screen.fullScreen;
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 3:
                windowed.isOn = !Screen.fullScreen;
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            case 4:
                windowed.isOn = !Screen.fullScreen;
                Screen.SetResolution(3840, 2160, Screen.fullScreen);
                break;
            default:
                break;
        }
    }

    private void SetWindowed()
    {
        if(windowed.isOn == true)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    private void SetQuality()
    {
        QualitySettings.SetQualityLevel(quality.value);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetVolumeFX()
    {   
        SoundManager.instance.inGameFXSource.volume = sfx.value;
        SoundManager.instance.lumyFxSource.volume = sfx.value;
        SoundManager.instance.menuFxSource.volume = sfx.value;

        SoundManager.instance.PlayFeebackSFXVolumeSFX();     
    }

    private void SetVolumeMusic()
    {
        SoundManager.instance.musicSource.volume = music.value;
    }

    public void SetVolumeGeneral()
    {
        SoundManager.instance.inGameFXSource.volume = general.value;
        SoundManager.instance.lumyFxSource.volume = general.value;
        SoundManager.instance.menuFxSource.volume = general.value;
        SoundManager.instance.musicSource.volume = general.value;

        sfx.value = general.value/5.0f;
        SetVolumeFX();
        music.value = general.value;
        SetVolumeMusic();
    }
}
