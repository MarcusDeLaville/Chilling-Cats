using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCat : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _isRight = true;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Turn());
    }

    private IEnumerator Turn()
    {
        while (true)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            yield return new WaitForSeconds(Random.Range(2,4));
        }
    }
}
