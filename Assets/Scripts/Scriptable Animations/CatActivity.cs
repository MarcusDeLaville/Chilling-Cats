using System;
using System.Collections;
using UnityEngine;

public class CatActivity : MonoBehaviour
{
    [SerializeField] private Sprite[] _frames;
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            for (int i = 0; i < _frames.Length; i++)
            {
                _renderer.sprite = _frames[i];
                yield return new WaitForSeconds(_speed);
            }
        }
    }
}
