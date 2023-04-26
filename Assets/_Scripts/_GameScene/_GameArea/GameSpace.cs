
using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace _Scripts._GameScene._Logic
{
  public class GameSpace : MonoBehaviour
  {
    //Don't change its value using this variable in class
    private Vector3 _selectedLocation;

    #region Private Variable

    #region Factory Prefabs

    [SerializeField] private Barracks _barracksPrefab;
    [SerializeField] private PowerPlant _powerPlantPrefab;

    #endregion


    [SerializeField] private ObjectPool<Barracks> _barracks;
    [SerializeField] private Grid _gameGrid;
    [SerializeField] private Transform _playableGameObject;
    [SerializeField] private Vector3 _gameSpaceStartPoint;
    [SerializeField] private Vector3 _gameSpaceEndPoint;
    [SerializeField] private static Vector2 _gridCellSize;
    [SerializeField] private Vector2Int _firstSearchCellPosition;
    [SerializeField] private Vector2Int _gameSpaceSizeByCell;
    [SerializeField] private Dictionary<string,Vector3Int> _filledCells;
    [SerializeField] private List<Vector2Int> _DEBUGfilledCells;
    

    #endregion

    #region Public Propert (Only Get)

    public Vector3 GameSpaceStartArea
    {
      get => _gameSpaceStartPoint;
    }
    
    public Vector3 GameSpaceEndArea
    {
      get => _gameSpaceEndPoint;
    }

    public static Vector2 GameSpaceCellSize
    {
      get => _gridCellSize;
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
      _gameSpaceEndPoint = new Vector3(1000, 1000,100);
      _gameSpaceStartPoint = transform.position;
      _gridCellSize = new Vector2(_gameGrid.cellSize.x, _gameGrid.cellSize.y);
      _gameSpaceSizeByCell = new Vector2Int(  Convert.ToInt32((_gameSpaceEndPoint.x - _gameSpaceStartPoint.x) / _gridCellSize.x)  ,
        Convert.ToInt32((_gameSpaceEndPoint.y - _gameSpaceStartPoint.y) / _gridCellSize.y));
      Debug.Log(_gameSpaceSizeByCell);
    }

    #endregion

    
    
    private Vector2Int GetCellFromWorldPosition(Vector3 worldPos)
    {
      Vector3Int cellPos = _gameGrid.WorldToCell(worldPos);
      return new Vector2Int(cellPos.x,cellPos.y);
    }

    private Vector3 GetWorldPositionFromCell(Vector2Int cellPos)
    {
      return new Vector3(cellPos.x * _gridCellSize.x,cellPos.y*_gridCellSize.y,transform.position.z);
    }
    
    
    
    
    #region CreateGameObject                 

    private void CreateBarracks()
    {
      Vector2Int spawnCellPosition = FindEmphtyCellsToCreate(Barracks.GameObjectSizeByCell, _firstSearchCellPosition, _DEBUGfilledCells);
      Barracks barracks = BarracksPool.SharedInstance.GetPooledObject(); 
      if (barracks != null) {
        barracks.transform.position = GetWorldPositionFromCell(spawnCellPosition);
        barracks.transform.rotation = Quaternion.identity;
        barracks.gameObject.SetActive(true);
        barracks.transform.SetParent(_playableGameObject);
      }
      SaveFilledCells(spawnCellPosition,new Vector2Int(spawnCellPosition.x+Barracks.GameObjectSizeByCell.x,spawnCellPosition.y+Barracks.GameObjectSizeByCell.y));


    }

    private void CreatePowerPlant()
    {
      Vector2Int spawnCellPosition = FindEmphtyCellsToCreate(PowerPlant.GameObjectSizeByCell, _firstSearchCellPosition, _DEBUGfilledCells);
      PowerPlant powerPlant = PowerPlantPool.SharedInstance.GetPooledObject(); 
      if (powerPlant != null) {
        powerPlant.transform.position = GetWorldPositionFromCell(spawnCellPosition);
        powerPlant.transform.rotation = Quaternion.identity;
        powerPlant.gameObject.SetActive(true);
        powerPlant.transform.SetParent(_playableGameObject);
      }
      SaveFilledCells(spawnCellPosition,new Vector2Int(spawnCellPosition.x+PowerPlant.GameObjectSizeByCell.x,spawnCellPosition.y+PowerPlant.GameObjectSizeByCell.y));
    }
    
    
    private void SaveFilledCells(Vector2Int sizeStartCell,Vector2Int sizeEndCell)
    {
      for (int i = sizeStartCell.x; i <= sizeEndCell.x; i++)
      {
        for (int j = sizeStartCell.y; j <= sizeEndCell.y; j++)
        {
          Vector2Int newFilledCell = new Vector2Int(i, j);
          //_filledCells.Add($"{newFilledCell.x},{newFilledCell.y},{newFilledCell.z}",newFilledCell);
          _DEBUGfilledCells.Add(newFilledCell);
        }
      }
      
    }
    
    private Vector2Int FindEmphtyCellsToCreate(Vector2Int gameObjectSizeByCell , Vector2Int startPointByCell, List<Vector2Int> filledCells)
    {

      
      int containsCounter;
      Vector2Int searchCellPosition = startPointByCell;
      Vector2Int firstPointMemento = new Vector2Int();
      do
      {
        containsCounter = 0;
        if (searchCellPosition == firstPointMemento && searchCellPosition != startPointByCell)
        {
          Debug.LogWarning("Cant find emphty cell");
          return new Vector2Int();
        }
        
        if (firstPointMemento !=  new Vector2Int())
        {
          searchCellPosition = CoordinateAdvanceFixer(searchCellPosition,gameObjectSizeByCell);
        }
        
        for (int i = searchCellPosition.x; i < gameObjectSizeByCell.x+searchCellPosition.x; i++)
        {
          for (int j = searchCellPosition.y ; j < gameObjectSizeByCell.y+searchCellPosition.y; j++)
          {
            if (filledCells.Contains(new Vector2Int(i, j)))
            {
              Debug.Log("Aynısı bulundu");
              Debug.Log(filledCells.Contains(new Vector2Int(i, j)));
              containsCounter++;
            }
          }
        }
        if (firstPointMemento == new Vector2Int())
        {
          firstPointMemento = searchCellPosition;
        }
      } while (containsCounter != 0);

      return searchCellPosition;
    }

    
    private Vector2Int CoordinateAdvanceFixer(Vector2Int searchPositionByCell,Vector2Int gameObjectSizeByCell)
    {
      searchPositionByCell.x += 1;
      if (searchPositionByCell.x+gameObjectSizeByCell.x >_gameSpaceSizeByCell.x)
      {
        searchPositionByCell.x = 0;
        searchPositionByCell.y += 1;
      }

      if (searchPositionByCell.y+gameObjectSizeByCell.y > _gameSpaceSizeByCell.y)
      {
        searchPositionByCell.y = 0;
      }
      return searchPositionByCell;
    }
    
    
    
    
    
    
    public void CreateCreatableOnGameSpace(object obj)
    {
      switch (obj)
      {
        case Barracks:
          CreateBarracks();
         
          break;
        
        case PowerPlant:
          CreatePowerPlant();
          break;
      }
    }

    #endregion
   

    
    
    
    
    
    
    
    
    
  
  }


}

