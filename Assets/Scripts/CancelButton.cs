using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public void OnClick()
    {

        GameManager.instance.notToClick = false;
        GameManager.instance.selectPosText.SetActive(false);
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.cancel);
    }

}
