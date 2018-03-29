using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class IntroThenLoop : MonoBehaviour
{
    public AudioClip StartClip;
    public AudioClip LoopClip;

    void Start()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = StartClip;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(StartClip.length);
        GetComponent<AudioSource>().clip = LoopClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }
}