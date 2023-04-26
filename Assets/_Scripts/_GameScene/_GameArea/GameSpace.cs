using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene._PlayerControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts._GameScene._Logic
{
  public class GameSpace : MonoBehaviour
  {
    //Don't change its value using this variable in class
    private Vector3 _selectedLocation;

    #region Private Variable

    [SerializeField] private Transform _playableGameObject;
    [SerializeField] private Vector3 _gameSpaceStartArea;
    [SerializeField] private Vector3 _gameSpaceEndArea;
    [SerializeField] private Vector3 _gameSpaceNumberOfCells;
    [SerializeField] private Vector3 _gridCellSize;
    [SerializeField] private Grid _gameGrid;
    [SerializeField] private Vector3Int _firstSearchCellPosition;
    [SerializeField] private Dictionary<string,Vector3Int> _filledCells;
    [SerializeField] private List<IPlayable> _playables;
    

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
      _firstSearchCellPosition = GetCellFromWorldPosition(Camera.main.transform.position);
      
      
    }
    
    
    
    #endregion

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
    
    
    
    private Vector3Int GetCellFromWorldPosition(Vector3 worldPos)
    {
      Vector3Int cellPos = _gameGrid.WorldToCell(worldPos);
      //Debug.Log(cellPos);
      return cellPos;
    }

    private Vector3 GetWorldPositionFromCell(Vector3Int cellPos)
    {
      return new Vector3(cellPos.x * _gridCellSize.x,cellPos.y*_gridCellSize.y,transform.position.z);
    }
    private void SaveFilledCells(Vector3Int spawnCellPosition,IPortable portableObject)
    {
      Vector2Int endPoint =new Vector2Int(spawnCellPosition.x + portableObject.CellSize.x ,spawnCellPosition.y + portableObject.CellSize.y);
      for (int i = spawnCellPosition.x; i <= endPoint.x; i++)
      {
        for (int j = spawnCellPosition.y; j <= endPoint.y; j++)
        {
          _filledCells.Add(portableObject.ID,new Vector3Int(i,j,0));
        }
      }
      
    }
    
    
 

   

    #region CreateGameObject                 /// <summary>
                                            /// ////////////////
                                            /// </summary>

    private void CreateBarracks()
    {
      Vector3Int spawnCellPosition = FindEmphtyCells(Barracks.StaticCellSize, _firstSearchCellPosition, _filledCells);
      Vector3 spawnWorldPosition = GetWorldPositionFromCell(spawnCellPosition);
      Barracks newBarracks = Instantiate<Barracks>(new Barracks(), spawnWorldPosition, Quaternion.identity, _playableGameObject);
      SaveFilledCells(spawnCellPosition, newBarracks);


    }

    private Vector3Int FindEmphtyCells(Vector3Int gameObjectSizes , Vector3Int startPoint, Dictionary<string,Vector3Int> filledCells)
    {

      bool IsAreaEmphty = true;
      
      Vector3Int searchCellPosition = startPoint;
      Vector3Int firstPointMemento = new Vector3Int();
      do
      {
        if (searchCellPosition == firstPointMemento)
        {
          Debug.LogWarning("Cant find emphty cell");
          return new Vector3Int();
        }
        
        if (firstPointMemento != null)
        {
          searchCellPosition = CoordinateAdvanceFixer(searchCellPosition);
        }
        
        for (int i = searchCellPosition.x; i < gameObjectSizes.x; i++)
        {
          for (int j = searchCellPosition.y ; j < gameObjectSizes.y; j++)
          {
            if (filledCells.ContainsValue(new Vector3Int(i, j, 0)))
            {
              IsAreaEmphty = false;
            }
          }
        }
        if (firstPointMemento == null)
        {
          firstPointMemento = searchCellPosition;
        }
      } while (IsAreaEmphty == false);

      return searchCellPosition;
    }

    
    private Vector3Int CoordinateAdvanceFixer(Vector3Int searchCellPosition)
    {
      searchCellPosition.x += 1;
      if (searchCellPosition.x > _gameSpaceNumberOfCells.x)
      {
        searchCellPosition.x = 0;
        searchCellPosition.y += 1;
      }

      if (searchCellPosition.y > _gameSpaceNumberOfCells.y)
      {
        searchCellPosition.y = 0;
      }
      return searchCellPosition;
    }
    
    
    
    
    
    
    public void CreateCreatableOnGameSpace(Type type)
    {
      if (type == typeof(Barracks))
      {
        CreateBarracks();
      }
      else if (type == typeof(PowerPlant))
      {
        
      }




    }

    #endregion
   

    
    
    
    
    
    
    
    
    
  
  }
  
}

