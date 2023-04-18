using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Object = System.Object;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _gameBoard;
    [SerializeField] private Tile tile;
    [SerializeField] private int _scale;
    public void InIt()
    {
        tile.init();
        GenerateGrid(10,10);
        
    }
    
    private void GenerateGrid(int width , int height)
    {
      var ranges = GetPositionRange(tile);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               var _spawnTitles = Instantiate(tile, new Vector3(x,y), Quaternion.identity,_gameBoard);
               RectTransform temp = _spawnTitles.GetComponent<RectTransform>();
               temp.anchorMin = new Vector2(0, 0);
               temp.anchorMax = new Vector2(0, 0);
               temp.pivot = new Vector2(0, 0);
               temp.anchoredPosition = new Vector2(x * ranges.x, y * ranges.y);
               _spawnTitles.name = $"Title {x},{y}";
            }
        }   
        
        
    }

    private Vector2 GetPositionRange(Tile tile)
    {
        Vector2 ranges = Vector2.zero;
        ranges.x = tile.PixelSize[0] * tile.GetComponent<RectTransform>().localScale.x;
        ranges.y = tile.PixelSize[1] * tile.GetComponent<RectTransform>().localScale.y;
        return ranges;

    }
    
    
}
