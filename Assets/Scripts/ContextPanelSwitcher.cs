using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextPanelSwitcher : MonoBehaviour
{
    [SerializeField] private Animator[] _contextPanels;
    private Animator _currentPanelAnimator;

    private void Start()
    {
        _currentPanelAnimator = _contextPanels[0];
        _contextPanels[0].SetBool("isOpen", true);
    }

    public void SwitchPanel(Animator animator)
    {
        if (animator != _currentPanelAnimator)
        {
            _currentPanelAnimator = animator;
            StartCoroutine(SwitchPanels());
        }  
    }

    private void OpenPanel()
    {
        _currentPanelAnimator.SetBool("isOpen", true);
    }

    private IEnumerator SwitchPanels()
    {
        for (int i = 0; i < _contextPanels.Length; i++)
        {
            if (_contextPanels[i].GetBool("isOpen") == true)
            {
                _contextPanels[i].SetBool("isOpen", false);
            }
        }

        yield return new WaitForSeconds(0.6f);
        OpenPanel();
    }

}
