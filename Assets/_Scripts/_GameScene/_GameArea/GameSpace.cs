using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.BuildCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts._GameScene._GameArea
{
  public class GameSpace : MonoBehaviour
  {
    #region UnityObjects
    
    [SerializeField] private Grid _gameGrid;
    [SerializeField] private Transform _buildings;

    #endregion
    
    #region Public Propertys
    
    public UnityEvent<Vector2Int, Vector2Int> SaveMyPlace , EraseMyPlace;
    public Transform PlayableGameObject { get => _buildings; }
    
    #endregion

    #region BuildCreaters

    private BarracksCreater barracksCreater;
    private PowerPlantCreater powerPlantCreater;

    #endregion
    
    #region Private Variable

    #region Static

    [SerializeField] private static Vector2 _cellSize;

    #endregion

    #region Changeable On Editör

    [SerializeField] private Vector2Int _firstSearchCellPosition; //Enter from Editör

    #endregion
    
    [SerializeField] private Vector3 _gameSpaceStartByPoint;
    [SerializeField] private Vector3 _gameSpaceEndByPoint;
    [SerializeField] private Vector2Int _gameSpaceEndByCell;
    [SerializeField] private Vector2Int _gameSpaceStartByCell;
    [SerializeField] private Vector2Int _gameSpaceSizeByCell;
    [SerializeField] private List<IProduct> _allCreatedProducts;
    #endregion

    #region Public Propert (Only Get)
    
    public List<IProduct> AllCreatedProducts { get => _allCreatedProducts; }
    public Vector3 GameSpaceStartAreaByPoint { get => _gameSpaceStartByPoint; }
    public Vector3 GameSpaceEndAreaByPoint { get => _gameSpaceEndByPoint; }
    public Vector2Int GameSpaceStartAreaByCell { get => _gameSpaceStartByCell; }
    public Vector2Int GameSpaceEndAreaByCell { get => _gameSpaceEndByCell; }
    public static Vector2 GameSpaceCellSize { get => _cellSize; }
    
    #endregion
    
    #region Start Func.

    public void InIt()
    {
      SetVariable();
      SetGameSpaceAreaCoordinate();
      AddEvents();
    }
    private void SetGameSpaceAreaCoordinate()
    {
      _gameSpaceEndByPoint = new Vector3(500, 500,100);
      _gameSpaceStartByPoint = transform.position;
      _cellSize = new Vector2(_gameGrid.cellSize.x, _gameGrid.cellSize.y);
      _gameSpaceSizeByCell = new Vector2Int( Convert.ToInt32((GameSpaceEndAreaByPoint.x - GameSpaceStartAreaByPoint.x) / GameSpaceCellSize.x),Convert.ToInt32((GameSpaceEndAreaByPoint.y - GameSpaceStartAreaByPoint.y) / GameSpaceCellSize.y));
      _gameSpaceEndByCell = ConvertPointToCell(_gameSpaceStartByPoint);
      _gameSpaceStartByCell = ConvertPointToCell(new Vector3(_gameSpaceEndByPoint.x - GameSpaceCellSize.x, _gameSpaceEndByPoint.y - GameSpaceCellSize.y, _gameSpaceEndByPoint.z));
    }
    private void SetVariable()
    {
      barracksCreater = new BarracksCreater();
      powerPlantCreater = new PowerPlantCreater();
      SaveMyPlace = new UnityEvent<Vector2Int, Vector2Int>();
      EraseMyPlace = new UnityEvent<Vector2Int, Vector2Int>();
      
    }
    private void AddEvents()
    {
      SaveMyPlace.AddListener(SaveFilledCells);
      EraseMyPlace.AddListener(EraseFromFilledCells);
    }
   
    #endregion

    #region Public CellToPosition - PositionToCell
    public Vector2Int ConvertPointToCell(Vector3 worldPos)
    {
      Vector3Int cellPos = _gameGrid.WorldToCell(worldPos);
      return new Vector2Int(cellPos.x,cellPos.y);
    }

    public Vector3 ConvertCellToPoint(Vector2Int cellPos)
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
          TriggerBarracksCreater();
         
          break;
        
        case PowerPlant:
          CreatePowerPlant();
          break;
      }
    }
    
    private void TriggerBarracksCreater()
    {
      Vector2Int spawnCellPosition = SearchEmpthyCellForSpawn(Barracks.GameObjectSizeByCell, _firstSearchCellPosition, AllCreatedProducts);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPosition), Barracks.GameObjectSizeByCell);
      IProduct barracks = barracksCreater.FactoryCreate(spawnPositionByPoint);
      barracks.Transform.SetParent(_buildings);
      _allCreatedProducts.Add(barracks);
    }
    
    private void CreatePowerPlant()
    {
      if (!IsPowerPlantPoolEmphty())
      {
        Vector2Int spawnCellPosition = SearchEmpthyCellForSpawn(PowerPlant.GameObjectSizeByCell, _firstSearchCellPosition, AllCreatedProducts);
        PowerPlant powerPlant = PowerPlantPool.SharedInstance.GetPooledObject();
        if (powerPlant != null)
        {
          powerPlant.InIt();
          powerPlant.transform.position = SpawnPointFinder(ConvertCellToPoint(spawnCellPosition),PowerPlant.GameObjectSizeByCell);
          powerPlant.transform.rotation = Quaternion.identity;
          powerPlant.gameObject.SetActive(true);
          powerPlant.transform.SetParent(_buildings);
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
    
    private Vector2Int SearchEmpthyCellForSpawn(Vector2Int gameObjectSizeByCell , Vector2Int startPointByCell, List<IProduct> allCreatedProducts)
    {

      bool IsSpawnable = false;
      int containsCounter = 0;
      Vector2Int searchCellPosition = startPointByCell;
      Vector2Int firstPointMemento = new Vector2Int();
      while (IsSpawnable == false)
      {
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
        
        
        
        
        
      }




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
      Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
      return new Vector3(spawnpoint.x + ((floatVector2.x / 2) * GameSpaceCellSize.x), spawnpoint.y + ((floatVector2.y / 2) * GameSpaceCellSize.y), spawnpoint.z);

    }
    

    #endregion
    
   
    
    
    
    
    
    
   

    
   

    
    
    
    
    
    
    
    
    
  
  }


}

