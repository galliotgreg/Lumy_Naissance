using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayThenLoadVideo : MonoBehaviour {

    public string target;
    public GameObject player;
    public Button skip;

    // Use this for initialization
    void Start () {
        player.GetComponent<VideoPlayer>().Play();
        StartCoroutine(WaitForLengthCo(player.GetComponent<VideoPlayer>().clip.length));
        skip.onClick.AddListener(SkipVideo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Change scene after waiting for an amount of time equal to the video's length
    IEnumerator WaitForLengthCo(double duration)
    {
        yield return new WaitForSeconds((float)duration);
        NavigationManager.instance.ActivateFadeToBlack();
        NavigationManager.instance.SwapScenesWithoutZoom(target);
        player.GetComponent<VideoPlayer>().Stop();
    }

    void SkipVideo()
    {
        NavigationManager.instance.ActivateFadeToBlack();
        NavigationManager.instance.SwapScenesWithoutZoom(target);
        StopCoroutine(WaitForLengthCo(player.GetComponent<VideoPlayer>().clip.length));
    }
}
