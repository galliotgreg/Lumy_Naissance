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
    [SerializeField]
    private Dropdown fps;
    [SerializeField]
    private Slider luminosity;
    #endregion
    #region Audio
    [SerializeField]
    private Slider sfx;
    [SerializeField]
    private Slider general;
    #endregion
    #endregion

    // Use this for initialization
    void Start () {
        resolution.onValueChanged.AddListener(delegate { SetResolution(); });
        windowed.onValueChanged.AddListener((on) => { SetWindowed(); });
        quality.onValueChanged.AddListener(delegate{ SetQuality(); });
        fps.onValueChanged.AddListener(delegate { SetFps(); });
        luminosity.onValueChanged.AddListener(delegate { setLuminosity(); });
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
                Screen.SetResolution(1280, 720, windowed.isOn);
                break;
            case 1:
                Screen.SetResolution(1366, 768, windowed.isOn);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, windowed.isOn);
                break;
            case 3:
                Screen.SetResolution(2560, 1440, windowed.isOn);
                break;
            case 4:
                Screen.SetResolution(3840, 2160, windowed.isOn);
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
    private void SetFps()
    {
        
          switch (fps.value)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 120;
                break;
            default:
                break;
        }
    }
    private void setLuminosity()
    {
        float rgbValue = luminosity.value;
        RenderSettings.ambientLight = new Color(rgbValue,rgbValue,rgbValue, 1);
        //RenderSettings.ambientLight = Color.red;
        Debug.Log("lumy");
    }

    private void SetVolumeFX()
    {
        //TODO Pour Greg
    }

    private void SetVolumeGeneral()
    {
        //TODO Pour Greg
    }
}
