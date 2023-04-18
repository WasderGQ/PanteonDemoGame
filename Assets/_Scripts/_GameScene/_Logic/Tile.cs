using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public RectTransform MyTilePosition;
    [SerializeField] private Vector2  _myCoordinate;
    


    public void init()
    {
        
    }

    public void GetMyCoordinate(Vector2 coordinate)
    {
        _myCoordinate = coordinate;
    }
    

    private void OnMouseDown()
    {
        Debug.Log(_myCoordinate);
    }
}
    