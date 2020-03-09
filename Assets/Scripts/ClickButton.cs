using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public void OnClick()
    {
        
        if (GameManager.instance.isClick && GameManager.instance.canGoNext)
        {
            GameManager.instance.ChangeTurn();
            GameManager.instance.selectPosText.SetActive(false);
            GameManager.instance.isClick = false;
        }
        else if(!GameManager.instance.isClick && GameManager.instance.canGoNext)
        {
            GameManager.instance.selectPosText.SetActive(false);
            GameManager.instance.notClicker.SetActive(true);
            Debug.Log("どこかクリックしてくれい");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.notClick);
        }
        else if (GameManager.instance.isClick && !GameManager.instance.canGoNext)
        {
            GameManager.instance.selectPosText.SetActive(false);
            GameManager.instance.notCanGO.SetActive(true);
            Debug.Log("そこはもうなんか置いてあるぞい");
            GameManager.instance.audioSource.PlayOneShot(GameManager.instance.notClick);
            
        }
    }

}
