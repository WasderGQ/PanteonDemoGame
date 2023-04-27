
using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace _Scripts._GameScene._Logic
{
  public class GameSpace : MonoBehaviour
  {

    #region Static Variable
    
    public static UnityEvent<Vector2Int, Vector2Int> SaveMyPlace , EraseMyPlace;
    
    #endregion
    
    #region Private Variable
    [SerializeField] private Grid _gameGrid;
    [SerializeField] private Vector3 _gameSpaceStartPoint;
    [SerializeField] private Vector3 _gameSpaceEndPoint;
    [SerializeField] private static Vector2 _cellSize;
    [SerializeField] private Vector2Int _gameSpaceSizeByCell;
    
    [SerializeField] private List<Vector2Int> _filledCellList;
    
    [SerializeField] private Transform _playableGameObject;
    [SerializeField] private Vector2Int _firstSearchCellPosition; //Enter from Editör
    #endregion

    #region Public Propert (Only Get)
    public Vector3 GameSpaceStartArea { get => _gameSpaceStartPoint; }
    public Vector3 GameSpaceEndArea { get => _gameSpaceEndPoint; }
    public static Vector2 GameSpaceCellSize { get => _cellSize; }
    
    #endregion
    
    public void InIt()
    {
      SetVariable();
      SetGameSpaceAreaCoordinate();
      AddEvents();
    }
    
    #region Start Func.

    private void SetGameSpaceAreaCoordinate()
    {
      _gameSpaceEndPoint = new Vector3(500, 500,100);
      _gameSpaceStartPoint = transform.position;
      _cellSize = new Vector2(_gameGrid.cellSize.x, _gameGrid.cellSize.y);
      _gameSpaceSizeByCell = new Vector2Int(  Convert.ToInt32((_gameSpaceEndPoint.x - _gameSpaceStartPoint.x) / GameSpaceCellSize.x),Convert.ToInt32((_gameSpaceEndPoint.y - _gameSpaceStartPoint.y) / GameSpaceCellSize.y));
      
    }

    private void SetVariable()
    {
      
      SaveMyPlace = new UnityEvent<Vector2Int, Vector2Int>();
      EraseMyPlace = new UnityEvent<Vector2Int, Vector2Int>();
      
    }
    
    
    private void AddEvents()
    {
      
      SaveMyPlace.AddListener(SaveFilledCells);
      EraseMyPlace.AddListener(EraseFromFilledCells);
    }
   
    #endregion

    #region CellToPosition - PositionToCell
    private Vector2Int GetCellFromWorldPosition(Vector3 worldPos)
    {
      Vector3Int cellPos = _gameGrid.WorldToCell(worldPos);
      return new Vector2Int(cellPos.x,cellPos.y);
    }

    private Vector3 GetWorldPositionFromCell(Vector2Int cellPos)
    {
      return new Vector3(cellPos.x * GameSpaceCellSize.x,cellPos.y*GameSpaceCellSize.y,transform.position.z);
    }
    
    #endregion
    
    #region CreateFactory Func.              

    public void CreateFactory(object obj)
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
    
    private void CreateBarracks()
    {
      if (!IsBarracksPoolEmphty())
      {
        Vector2Int spawnCellPosition = SearchEmpthyCellForCreate(Barracks.GameObjectSizeByCell, _firstSearchCellPosition, _filledCellList);
        Barracks barracks = BarracksPool.SharedInstance.GetPooledObject();
        if (barracks != null)
        {
          barracks.InIt();
          barracks.transform.position = SpawnPointFinder(GetWorldPositionFromCell(spawnCellPosition), Barracks.GameObjectSizeByCell);
          barracks.transform.rotation = Quaternion.identity;
          barracks.gameObject.SetActive(true);
          barracks.transform.SetParent(_playableGameObject);
          SaveFilledCells(spawnCellPosition, new Vector2Int(spawnCellPosition.x + Barracks.GameObjectSizeByCell.x, spawnCellPosition.y + Barracks.GameObjectSizeByCell.y));
        }
      }
      else
        {
          Debug.LogWarning("Pool is Emphty. You cant spawn more Barracks");
        }
    }
    
    private void CreatePowerPlant()
    {
      if (!IsPowerPlantPoolEmphty())
      {
        Vector2Int spawnCellPosition = SearchEmpthyCellForCreate(PowerPlant.GameObjectSizeByCell, _firstSearchCellPosition, _filledCellList);
        PowerPlant powerPlant = PowerPlantPool.SharedInstance.GetPooledObject();
        if (powerPlant != null)
        {
          powerPlant.InIt();
          powerPlant.transform.position = SpawnPointFinder(GetWorldPositionFromCell(spawnCellPosition),PowerPlant.GameObjectSizeByCell);
          powerPlant.transform.rotation = Quaternion.identity;
          powerPlant.gameObject.SetActive(true);
          powerPlant.transform.SetParent(_playableGameObject);
        }
        SaveFilledCells(spawnCellPosition, new Vector2Int(spawnCellPosition.x + PowerPlant.GameObjectSizeByCell.x, spawnCellPosition.y + PowerPlant.GameObjectSizeByCell.y));
      }
      else
      {
        Debug.LogWarning("Pool is Emphty. You cant spawn more PowerPlant");
        
      }
      
    }

    
    #endregion

    #region FactoryPool Control Func. 

    private bool IsBarracksPoolEmphty()
    { 
      List<Barracks> inActive = BarracksPool.SharedInstance.PooledObjects.FindAll(x => x.isActiveAndEnabled == true);
      if (BarracksPool.SharedInstance.PooledObjects.Count - inActive.Count == 0)
      {
        return true;
      }
      if (BarracksPool.SharedInstance.PooledObjects.Count == 0)
      {
        return true;
      }
      return false;
    }

    private bool IsPowerPlantPoolEmphty()
    {
      List<PowerPlant> inActive = PowerPlantPool.SharedInstance.PooledObjects.FindAll(x => x.isActiveAndEnabled == true);
      if (PowerPlantPool.SharedInstance.PooledObjects.Count - inActive.Count == 0)
      {
        return true;
      }
      return false;
    }
    
    #endregion

    #region FilledCells Operation Func.


    private void EraseFromFilledCells(Vector2Int sizeStartCell, Vector2Int sizeEndCell)
    {
      for (int i = sizeStartCell.x; i < sizeEndCell.x; i++)
      {
        for (int j = sizeStartCell.y; j < sizeEndCell.y; j++)
        {
          _filledCellList.Remove(new Vector2Int(i, j));
        }
      }

    }
    
    private void SaveFilledCells(Vector2Int sizeStartCell,Vector2Int sizeEndCell)
    {
      for (int i = sizeStartCell.x; i < sizeEndCell.x; i++)
      {
        for (int j = sizeStartCell.y; j < sizeEndCell.y; j++)
        {
          Vector2Int newFilledCell = new Vector2Int(i, j);
          //_filledCells.Add($"{newFilledCell.x},{newFilledCell.y},{newFilledCell.z}",newFilledCell);
          _filledCellList.Add(newFilledCell);
        }
      }
      
    }
    
    private Vector2Int SearchEmpthyCellForCreate(Vector2Int gameObjectSizeByCell , Vector2Int startPointByCell, List<Vector2Int> filledCells)
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
          searchCellPosition = SearchCellCoordinateAdvanceFixer(searchCellPosition,gameObjectSizeByCell);
        }
        
        for (int i = searchCellPosition.x; i < gameObjectSizeByCell.x+searchCellPosition.x; i++)
        {
          for (int j = searchCellPosition.y ; j < gameObjectSizeByCell.y+searchCellPosition.y; j++)
          {
            if (filledCells.Contains(new Vector2Int(i, j)))
            {
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
    
    private Vector2Int SearchCellCoordinateAdvanceFixer(Vector2Int searchPositionByCell,Vector2Int gameObjectSizeByCell)
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

    #endregion
    
    #region Position Operators

    private Vector3 SpawnPointFinder(Vector3 spawnpoint ,Vector2Int gameObjectSizeByCell)
    {
      return new Vector3(spawnpoint.x + ((gameObjectSizeByCell.x / 2) * GameSpaceCellSize.x), spawnpoint.y + ((gameObjectSizeByCell.y / 2) * GameSpaceCellSize.y), spawnpoint.z);

    }
    

    #endregion
    
   
    
    
    
    
    
    
   

    
   

    
    
    
    
    
    
    
    
    
  
  }


}

