using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayThenLoadVideo : MonoBehaviour {

    public string target;
    public GameObject player;
    public AudioSource sonVideo;
    public float fadeoutIntro = 0.8f;
    //public Button skip;

    private bool alreadyClick = false;

    // Use this for initialization
    void Start () {
        player.GetComponent<VideoPlayer>().Play();
        StartCoroutine(WaitForLengthCo(player.GetComponent<VideoPlayer>().clip.length));
        SoundManager.instance.PlayMainTheme();
        //skip.onClick.AddListener(SkipVideo);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (alreadyClick)
        {
            return; 
        }
        if (Input.GetMouseButtonDown(0) && NavigationManager.instance.GetCurrentScene() != null)
        {
            alreadyClick = true; 
            SkipVideo();
        }
    }

    // Change scene after waiting for an amount of time equal to the video's length
    IEnumerator WaitForLengthCo(double duration)
    {
        yield return new WaitForSeconds((float)duration);
        NavigationManager.instance.ActivateFadeToBlack();
        NavigationManager.instance.SwapScenesWithoutZoom(target);
        player.GetComponent<VideoPlayer>().Stop();
    }

    // Skip video after click
    void SkipVideo()
    {
        NavigationManager.instance.ActivateFadeToBlack();
        NavigationManager.instance.SwapScenesWithoutZoom(target);
        StartCoroutine(SoundManager.instance.FadeTheFuckOut(sonVideo, fadeoutIntro));
        StopCoroutine(WaitForLengthCo(player.GetComponent<VideoPlayer>().clip.length));
    }
}
