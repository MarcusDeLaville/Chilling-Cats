using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public void OpenPanel(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetBool("isOpen", true);
        gameObject.GetComponent<Image>().raycastTarget = true;
    }

    public void ClosePanel(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetBool("isOpen", false);
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
