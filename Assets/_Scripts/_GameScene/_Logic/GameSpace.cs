using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene._Logic
{
  public class GameSpace : MonoBehaviour
  {
    //Don't change its value using this variable in class
    private Vector3 _selectedLocation;

    #region Private Variable

    [SerializeField] private Vector3 _gameSpaceStartArea;
    [SerializeField] private Vector3 _gameSpaceEndArea;
    [SerializeField] private Vector3 _gameSpaceNumberOfCells;
    [SerializeField] private Vector3 _gridCellSize;
    [SerializeField] private Grid _gameGrid;
    [SerializeField] private List<Vector3Int> _filledCells;

    #endregion

    #region Public Propert (Only Get)

    public Vector3 GameSpaceStartArea
    {
      get => _gameSpaceStartArea;
    }
    
    public Vector3 GameSpaceEndArea
    {
      get => _gameSpaceEndArea;
    }

    #endregion

    #region Private Property (For Control)
    private Vector3 SelectedLocation
    {
      get => _selectedLocation;
      set
      {

        if (IsLocationAcceptable(value))
        {
          _selectedLocation = value;
        }

        _selectedLocation = new Vector3();
      }
    }
    
    #endregion

    
    public void InIt()
    {
      GetCellFromWorldPosition(Vector3.zero);
      SetGameSpaceAreaCoordinate();
      
    }
   

    #region Start Func.
    
    private void SetGameSpaceAreaCoordinate()
    {
      _gameSpaceEndArea = new Vector3(1000, 1000,100);
      _gameSpaceStartArea = new Vector3(0, 0,100);
      _gridCellSize = new Vector3(10, 10, 0);
      _gameSpaceNumberOfCells = new Vector3(_gameSpaceEndArea.x / _gridCellSize.x, _gameSpaceEndArea.y / _gridCellSize.y,
        0);
    }
    

    #endregion




  

    private Vector3Int GetCellFromWorldPosition(Vector3 worldPos)
    {
      worldPos = new Vector3(10, 10, _gameGrid.transform.position.z);
      Vector3Int cellPos = _gameGrid.WorldToCell(worldPos);
      Debug.Log(cellPos);
      return cellPos;
    }

    private void SaveFilledCells(IMovable GameObject)
    {
      //_gameGrid.
      
    }
    
    


    #region Control Func.

    private bool IsLocationAcceptable(Vector3 value)
    {
      bool IsValidX = default(bool);
      bool IsValidY = default(bool);
      if (value.x <= _gameSpaceEndArea.x && value.x >= _gameSpaceStartArea.x)
      {
        IsValidX = true;
      }

      if (value.y <= _gameSpaceEndArea.y && value.y >= _gameSpaceStartArea.y)
      {
        IsValidY = true;
      }

      return IsValidX && IsValidY;
    }

    #endregion
    
    


  }
}
