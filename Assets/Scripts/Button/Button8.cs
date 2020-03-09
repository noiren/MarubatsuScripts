using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button8 : MonoBehaviour
{
    public AudioClip poin;
    AudioSource audiosource;

    // ボタンが押された場合、今回呼び出される関数
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void OnClick()
    {
        audiosource.PlayOneShot(poin);

        FadeManager.Instance.LoadScene("Credit", 1.0f);
    }
}
