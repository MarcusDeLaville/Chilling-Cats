using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdvertiseEvent : MonoBehaviour
{
    [SerializeField] private int _eventDelay;

    [SerializeField] private Animator _animator;
    [SerializeField] private Advertising _advertising;

    private bool _isCollected = false;

    private void Start()
    {
        StartCoroutine(AdventAdvertising());
    }

    private IEnumerator AdventAdvertising()
    {
        while (true)
        {
            yield return new WaitForSeconds(_eventDelay);

            _animator.SetBool("isShowing", true);
            yield return new WaitForSeconds(10);
            _animator.SetBool("isShowing", false);
            yield return new WaitForSeconds(1);
            _isCollected = false;
        }
    }
}
