using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.BuildingCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts._GameScene._GameArea
{
  public class GameSpace : MonoBehaviour
  {
    #region UnityObjects (Set From Editor)

    #region Static

    [SerializeField] private static Grid _grid;

    #endregion
    
    #region BuildingDepos

    [SerializeField] private Transform _barracksStore;
    [SerializeField] private Transform _powerPlantStore;
    [SerializeField] private Grid _editableGrid;
    
    #endregion

    #endregion
    
    #region Public Propertys
    
    public Transform BarracksStore { get => _barracksStore; }
    public Transform PowerPlantStore { get => _powerPlantStore; }

    #endregion
    
    #region Private Variable

    #region Static
    
    private static Vector3 _myPosition;
    private static Vector2 _cellSize;
    private static Vector2Int _gameSpaceStartByCell;
    private static Vector2Int _gameSpaceSizeByCell;
    private static Vector2Int _gameSpaceEndByCell;
    
    #endregion
    
    #region BuildCreaters

    private BarracksCreater _barracksCreater;
    private PowerPlantCreater _powerPlantCreater;

    #endregion
    
    #region Changeable On Editor

    [SerializeField] private Vector2Int _firstSearchCellPosition; //Enter from Editor

    #endregion

    #region Regular

    private Vector3 _gameSpaceStartByPoint;
    private Vector3 _gameSpaceEndByPoint;
    private List<IProduct> _buildingList;
    private List<IGameSpaceOccupanter> _occupanterList;
    
    #endregion
    
    #endregion

    #region Public Propert (Only Get)
    
    public List<IGameSpaceOccupanter> OccupanterList { get => _occupanterList; }
    public List<IProduct> BuildingList { get => _buildingList; }
    public Vector3 GameSpaceStartAreaByPoint { get => _gameSpaceStartByPoint; }
    public Vector3 GameSpaceEndAreaByPoint { get => _gameSpaceEndByPoint; }
    public static Vector2Int GameSpaceStartAreaByCell { get => _gameSpaceStartByCell; }
    public static Vector2Int GameSpaceEndAreaByCell { get => _gameSpaceEndByCell; }
    public static Vector2 GameSpaceCellSize { get => _cellSize; }
    
    #endregion
    
    #region Start Func.

    public void InIt()
    {
      SetVariable();
      SetGameSpaceAreaCoordinate();
    }
    private void SetGameSpaceAreaCoordinate()
    {
      _gameSpaceEndByPoint = new Vector3(500, 500,100);
      _gameSpaceStartByPoint = transform.position;
      _cellSize = new Vector2(_grid.cellSize.x, _grid.cellSize.y);
      _gameSpaceSizeByCell = new Vector2Int( Convert.ToInt32((GameSpaceEndAreaByPoint.x - GameSpaceStartAreaByPoint.x) / GameSpaceCellSize.x),Convert.ToInt32((GameSpaceEndAreaByPoint.y - GameSpaceStartAreaByPoint.y) / GameSpaceCellSize.y));
      _gameSpaceStartByCell = ConvertPointToCell(GameSpaceStartAreaByPoint);
      _gameSpaceEndByCell= ConvertPointToCell(new Vector3(GameSpaceEndAreaByPoint.x-GameSpaceCellSize.x/2,GameSpaceEndAreaByPoint.y-GameSpaceCellSize.y/2,GameSpaceEndAreaByPoint.z));
    }
    private void SetVariable()
    {
      _buildingList = new List<IProduct>();
      _occupanterList = new List<IGameSpaceOccupanter>();
      _grid = _editableGrid;
      _myPosition = transform.position;
      _barracksCreater = new BarracksCreater();
      _powerPlantCreater = new PowerPlantCreater();
      
      
    }

    #endregion

    #region Public Static CellToPosition - PositionToCell
    public static Vector2Int ConvertPointToCell(Vector3 worldPos)
    { 
      Vector3Int cellPos = _grid.WorldToCell(worldPos);
      return new Vector2Int(cellPos.x,cellPos.y);
    }

    public static Vector3 ConvertCellToPoint(Vector2Int cellPos)
    {
      return new Vector3(cellPos.x * GameSpaceCellSize.x,cellPos.y*GameSpaceCellSize.y,_myPosition.z);
    }
    
    #endregion
    
    #region TriggerCreateFactory Func.              

    public void CreateFactory(object obj)
    {
      switch (obj)
      {
        case Barracks:
          TriggerBarracksCreater();
         
          break;
        
        case PowerPlant:
          TriggerPowerPlantCreater();
          break;
      }
    }
    
    private void TriggerBarracksCreater()
    {
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(Barracks.GameObjectSizeByCell, _firstSearchCellPosition, OccupanterList);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), Barracks.GameObjectSizeByCell);
      IProduct barracks = _barracksCreater.FactoryCreate(spawnPositionByPoint,spawnCellPositionByCell);
      barracks.MyTransform.SetParent(BarracksStore);
      BuildingList.Add(barracks);
      OccupanterList.Add(barracks);
    }
    
    private void TriggerPowerPlantCreater()
    {
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(PowerPlant.GameObjectSizeByCell, _firstSearchCellPosition, OccupanterList);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), PowerPlant.GameObjectSizeByCell);
      IProduct powerPlant = _powerPlantCreater.FactoryCreate(spawnPositionByPoint,spawnCellPositionByCell);
      powerPlant.MyTransform.SetParent(PowerPlantStore);
      BuildingList.Add(powerPlant);
      OccupanterList.Add(powerPlant);
    }

    
    #endregion
    
    #region SearchEmpthyCellForCreate Func.

    private Vector2Int SearchEmptyCellForSpawn(Vector2Int gameObjectSizeByCell, Vector2Int startPointByCell, List<IGameSpaceOccupanter> occupanterList)
    {

      bool IsSpawnable = false;
      int containsCounter = 0;
      Vector2Int searchCellPosition = startPointByCell;
      List<IGameSpaceOccupanter> allProductsOnGameSpace = GetAllProductByList(occupanterList);

      while (IsSpawnable == false)
      {
        containsCounter = 0;
        for (int i = searchCellPosition.x; i < gameObjectSizeByCell.x + searchCellPosition.x; i++)
        {
          for (int j = searchCellPosition.y; j < gameObjectSizeByCell.y + searchCellPosition.y; j++)
            {
              foreach (var occupanter in allProductsOnGameSpace)
              {
                containsCounter += IsCellValidForCreate(new Vector2Int(i,j),occupanter);
              }
            }
        }
        if (containsCounter == 0)
        {
          IsSpawnable = true;
          return searchCellPosition;
        }
        
        searchCellPosition = SearchCellCoordinateAdvanceFixer( searchCellPosition, gameObjectSizeByCell);
       Debug.Log(searchCellPosition);
        
        if (searchCellPosition == startPointByCell)
        {
          Debug.Log("I search all cells on GameSpace but there isn't empty cell for create");
          
        }
      }

      return new Vector2Int();

    }
    
    private int IsCellValidForCreate(Vector2Int searchGameObjectSizeByCell,IGameSpaceOccupanter occupanter)
    {
      for (int i = occupanter.StartPositionByCell.x; i <= occupanter.EndPositionByCell.x; i++)
      {
        for (int j = occupanter.StartPositionByCell.y; j <= occupanter.EndPositionByCell.y; j++)
        {
          
          if (searchGameObjectSizeByCell == new Vector2Int(i, j))
          {
            return 1;
          }
          
        }
      }

      return 0;
    }
    
    private List<IGameSpaceOccupanter> GetAllProductByList(List<IGameSpaceOccupanter> buildingList)
    {
      List<IGameSpaceOccupanter> allProductsOnGameSpace = new List<IGameSpaceOccupanter>();
      if (buildingList.Count != 0)
      {
        foreach (var building in buildingList)
        {
          allProductsOnGameSpace.Add(building);
          if (building.Occupanters.Count != 0)
          {
            foreach (var buildingProduct in building.Occupanters)
            {
              allProductsOnGameSpace.Add(buildingProduct);
            }
          }
          
        }
      }



      return allProductsOnGameSpace;

    }
    
    
    private Vector2Int SearchCellCoordinateAdvanceFixer(Vector2Int searchPositionByCell,Vector2Int gameObjectSizeByCell)
    { 
      Vector2Int newSearchPositionByCell = new Vector2Int(searchPositionByCell.x + 1,searchPositionByCell.y);
      if (newSearchPositionByCell.x+gameObjectSizeByCell.x > GameSpaceEndAreaByCell.x)
      {
        newSearchPositionByCell = new Vector2Int(0, newSearchPositionByCell.y + 1);
      }

      if (searchPositionByCell.y+gameObjectSizeByCell.y > GameSpaceEndAreaByCell.y)
      {
        newSearchPositionByCell = new Vector2Int(0, 0);
      }
      return newSearchPositionByCell;
    }

    #endregion
    
    #region Position Operators

    private Vector3 SpawnPointFinder(Vector3 spawnPoint ,Vector2Int gameObjectSizeByCell)
    {
      Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
      return new Vector3(spawnPoint.x + ((floatVector2.x / 2) * GameSpaceCellSize.x), spawnPoint.y + ((floatVector2.y / 2) * GameSpaceCellSize.y), spawnPoint.z);

    }
    

    #endregion
    
   
    
    
    
    
    
    
   

    
   

    
    
    
    
    
    
    
    
    
  
  }


}

