using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LaunchVideoInPopUp : MonoBehaviour
{

    public GameObject popup;
    public GameObject player;
    public Button pauseBtn;
    public Button closeBtn;

    private bool playing;

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OpenPopUpVideo);
        pauseBtn.onClick.AddListener(PauseVideo);
        closeBtn.onClick.AddListener(ClosePopUpVideo);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenPopUpVideo()
    {
        player.GetComponent<VideoPlayer>().Play();
        popup.SetActive(true);
        playing = true;
    }

    void PauseVideo()
    {
        if (playing)
        {
            player.GetComponent<VideoPlayer>().Pause();
            playing = false;
        } else
        {
            player.GetComponent<VideoPlayer>().Play();
            playing = true;
        }
    }

    void ClosePopUpVideo()
    {
        player.GetComponent<VideoPlayer>().Stop();
        popup.SetActive(false);
        playing = false;
    }

}