using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Sounds _sounds;

    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;
    [SerializeField] private Image _musicSprite;

    private void Start()
    {
        SetSoundsLabel();
    }

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

    public void SwitchSound()
    {
        _sounds.SwitchSoundState();

        SetSoundsLabel();
    }

    private void SetSoundsLabel()
    {
        if (_sounds._doSound)
        {
            _musicSprite.sprite = _musicOn;
        }
        else
        {
            _musicSprite.sprite = _musicOff;
        }
    }

}
