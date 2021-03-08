using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxClouds : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private float _speed;

    private float _imagePositionX;

    private void Update()
    {
        _imagePositionX += _speed * Time.deltaTime;
        _image.uvRect = new Rect(_imagePositionX, _image.uvRect.y, _image.uvRect.width, _image.uvRect.height);
    }
}
