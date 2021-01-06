using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxClouds : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private float _speed;

    [SerializeField]private SpriteMask spriteMask;

    private float _imagePositionX;

    private void Update()
    {
        spriteMask.UpdateGIMaterials();

        _imagePositionX += _speed * Time.deltaTime;

        _image.uvRect = new Rect(_imagePositionX, 0, _image.uvRect.width, _image.uvRect.height);
    }
}
