using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private int[] _pixelSize;
    
    
    public int[] PixelSize
    {
        get => _pixelSize;
    }


    public void init()
    {
        GetImagePixelSize();
    }


    private void GetImagePixelSize()
    {
        _pixelSize = new int[]
        {
             GetComponent<Image>().sprite.texture.width,
             GetComponent<Image>().sprite.texture.height
        };


    }

    
}
