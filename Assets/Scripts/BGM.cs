using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }
    void Update()
    {

    }

    public static void StopBGM()
    {
        audioSource.Stop();
    }



}