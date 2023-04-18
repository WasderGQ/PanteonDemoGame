using System;
using UnityEngine;
public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _gameBoard;
    [SerializeField] private GameObject _tilePosition;
    [SerializeField] private int _scale;
    public void InIt()
    {
        
        GenerateGrid(10,9);
        
    }
    
    private void GenerateGrid(int width , int height)
    {
      var ranges = GetPositionRange(_tilePosition);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               var spawnTitles = Instantiate(_tilePosition, new Vector3(x,y), Quaternion.identity,_gameBoard);
               spawnTitles.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
               spawnTitles.GetComponentInChildren<Tile>().GetMyCoordinate(new Vector2(x,y));
               RectTransform temp = spawnTitles.GetComponent<RectTransform>();
               temp.anchoredPosition = new Vector2(x * ranges.x, y * ranges.y);
               spawnTitles.name = $"Title {x},{y}";
               
            }
        }
    }

    private Vector2 GetPositionRange(GameObject positionObject)
    {
        Vector2 ranges = Vector2.zero;
        ranges.x = Convert.ToInt32(positionObject.GetComponent<RectTransform>().sizeDelta.x) * positionObject.GetComponent<RectTransform>().localScale.x;
        ranges.y = Convert.ToInt32(positionObject.GetComponent<RectTransform>().sizeDelta.y) * positionObject.GetComponent<RectTransform>().localScale.y;
        return ranges;

    }
    
    
    
}
