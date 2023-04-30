using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

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

    private BarracksCreater barracksCreater;
    private PowerPlantCreater powerPlantCreater;

    #endregion
    
    #region Changeable On Editor

    [SerializeField] private Vector2Int _firstSearchCellPosition; //Enter from Editor

    #endregion

    #region Regular

    private Vector3 _gameSpaceStartByPoint;
    private Vector3 _gameSpaceEndByPoint;
    private List<IRealProduct> _barracksList;
    private List<IRealProduct> _powerPlantsList;

    #endregion
    
    #endregion

    #region Public Propert (Only Get)

    public List<IRealProduct> BuildingList { get => ShowAllBuildingList(_barracksList,_powerPlantsList); }
    public Vector3 GameSpaceStartAreaByPoint { get => _gameSpaceStartByPoint; }
    public Vector3 GameSpaceEndAreaByPoint { get => _gameSpaceEndByPoint; }
    
    
    #region Static
   
    public static Vector2Int UnValidVector = new Vector2Int(GameSpaceStartAreaByCell.x -1 , GameSpaceEndAreaByCell.y +1);
    public static Vector2Int GameSpaceStartAreaByCell { get => _gameSpaceStartByCell; }
    public static Vector2Int GameSpaceEndAreaByCell { get => _gameSpaceEndByCell; }
    public static Vector2 GameSpaceCellSize { get => _cellSize; }
    
    #endregion
    
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
      _barracksList = new List<IRealProduct>();
      _powerPlantsList = new List<IRealProduct>();
      _grid = _editableGrid;
      _myPosition = transform.position;
      barracksCreater = new BarracksCreater();
      powerPlantCreater = new PowerPlantCreater();
      
      
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
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(Barracks.GameObjectSizeByCell, _firstSearchCellPosition);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), Barracks.GameObjectSizeByCell);
      IRealProduct barracks = barracksCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
      barracks.MyTransform.SetParent(BarracksStore);
      _barracksList.Add(barracks);
    }
    
    private void TriggerPowerPlantCreater()
    {
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(PowerPlant.GameObjectSizeByCell, _firstSearchCellPosition);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), PowerPlant.GameObjectSizeByCell);
      IRealProduct powerPlant = powerPlantCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
      powerPlant.MyTransform.SetParent(PowerPlantStore);
      _powerPlantsList.Add(powerPlant);
    }

    
    #endregion
    
    #region SearchEmpthyCellForCreate Func.

    public Vector2Int SearchEmptyCellForSpawn(Vector2Int gameObjectSizeByCell, Vector2Int startPointByCell)
    {

      bool IsSpawnable = false;
      int containsCounter = 0;
      Vector2Int searchCellPosition = startPointByCell;
      List<IRealProduct> allProductsOnGameSpace = GetAllProductByList(BuildingList);
      foreach (var product in allProductsOnGameSpace)
      {Debug.Log(product.GetHashCode());
        Debug.Log(product.StartPositionByCell.x);
        Debug.Log(product.StartPositionByCell.y);
        Debug.Log(product.EndPositionByCell.x);
        Debug.Log(product.EndPositionByCell.y);
      }
      while (IsSpawnable == false)
      {
        containsCounter = 0;
        for (int i = searchCellPosition.x; i < gameObjectSizeByCell.x + searchCellPosition.x; i++)
        {
          for (int j = searchCellPosition.y; j < gameObjectSizeByCell.y + searchCellPosition.y; j++)
            {
              foreach (var product in allProductsOnGameSpace)
              {
                containsCounter += IsCellValidForCreate(new Vector2Int(i,j),product);
              }
            }
        }
        if (containsCounter == 0)
        {
          IsSpawnable = true;
          return searchCellPosition;
        }
        
        searchCellPosition = SearchCellCoordinateAdvanceFixer( searchCellPosition, gameObjectSizeByCell);

        if (searchCellPosition == startPointByCell)
        {
          Debug.Log("I search all cells on GameSpace but there isn't empty cell for create");
          
        }
      }

      return new Vector2Int();

    }
    
    private int IsCellValidForCreate(Vector2Int searchGameObjectSizeByCell,IRealProduct product)
    {
      for (int i = product.StartPositionByCell.x; i <= product.EndPositionByCell.x; i++)
      {
        for (int j = product.StartPositionByCell.y; j <= product.EndPositionByCell.y; j++)
        {
          
          if (searchGameObjectSizeByCell == new Vector2Int(i, j))
          {
            return 1;
          }
          
        }
      }

      return 0;
    }
    
    private List<IRealProduct> GetAllProductByList(List<IRealProduct> buildingList)
    {
      List<IRealProduct> allProductsOnGameSpace = new List<IRealProduct>();

      foreach (var building in buildingList)
      {
        allProductsOnGameSpace.Add(building);
        foreach (var buildingProduct in building.ProductList)
        {
          allProductsOnGameSpace.Add(buildingProduct);
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
    
    private List<IRealProduct> ShowAllBuildingList (List<IRealProduct> barracksList, List<IRealProduct> powerPlantList)
    {
      List<IRealProduct> allBuildingsOnGameSpace = new List<IRealProduct>();
      foreach (var heavySoldier in barracksList)
      {
        allBuildingsOnGameSpace.Add(heavySoldier);
      }
      foreach (var mediumSoldier in powerPlantList)
      {
        allBuildingsOnGameSpace.Add(mediumSoldier);
      }
      
      return allBuildingsOnGameSpace;
            
    }
    
    
    
    
    
    
   

    
   

    
    
    
    
    
    
    
    
    
  
  }


}

