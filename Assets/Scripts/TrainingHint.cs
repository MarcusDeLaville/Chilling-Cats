using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingHint : MonoBehaviour
{
    [SerializeField] private Text _hintText;
    [SerializeField] private Animator _animator;

    public void SwapHint(string hintText)
    {
        StartCoroutine(ShowtNewHint(hintText));
    }

    public void HideHint()
    {
        _animator.SetBool("isOpen", false);
    }

    private IEnumerator ShowtNewHint(string text)
    {
        _animator.SetBool("isOpen", false);
        yield return new WaitForSeconds(0.5f);
        _hintText.text = text;
        _animator.SetBool("isOpen", true);
    }
}
