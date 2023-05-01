using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.BuildingCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts._GameScene._GameArea
{
  public class GameSpace : MonoBehaviour //must be singleton
  {
    
    #region ReadOnly Values

    private readonly Vector2Int _powerPlantSizeByCell = new Vector2Int(2, 3);
    private readonly Vector2Int _barracksSizeByCell = new Vector2Int(4, 4);

    #endregion
    
    #region UnityObjects (Set From Editor)

    #region Static

    private static Grid _grid;
    
    #endregion
    
    #region BuildingDepos //Set Form Editor

    [SerializeField] private Transform _barracksStore;
    [SerializeField] private Transform _powerPlantStore;
    [SerializeField] private Grid _setFormEditorGrid;
    
    #endregion

    #endregion
    
    #region Public Propertys

    #region Static

    

    #endregion
    
    
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
    private static List<IRealProduct> _barracksList;
    private static List<IRealProduct> _powerPlantsList;
    
    #endregion
    
    #region BuildCreaters

    private BarracksCreater barracksCreater;
    private PowerPlantCreater powerPlantCreater;

    #endregion
    
    #region Changeable Value //Set From Editor

    [SerializeField] private Vector2Int _firstSearchCellPosition; 

    #endregion

    #region Regular

    private Vector3 _gameSpaceStartByPoint;
    private Vector3 _gameSpaceEndByPoint;
    

    #endregion
    
    #endregion

    #region Public Propert (Only Get)

    
    public Vector3 GameSpaceStartAreaByPoint { get => _gameSpaceStartByPoint; }
    public Vector3 GameSpaceEndAreaByPoint { get => _gameSpaceEndByPoint; }
    
    
    #region Static
   
    public static List<IRealProduct> BuildingList { get => ShowAllBuildingList(_barracksList,_powerPlantsList); }
    
    public static Vector2Int UnValidVector = new Vector2Int(GameSpaceStartAreaByCell.x -1 , GameSpaceEndAreaByCell.y +1);
    public static Vector2Int GameSpaceStartAreaByCell { get => _gameSpaceStartByCell; }
    public static Vector2Int GameSpaceEndAreaByCell { get => _gameSpaceEndByCell; }
    public static Vector2 CellSize { get => _cellSize; }
    
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
      _gameSpaceSizeByCell = new Vector2Int( Convert.ToInt32((GameSpaceEndAreaByPoint.x - GameSpaceStartAreaByPoint.x) / CellSize.x),Convert.ToInt32((GameSpaceEndAreaByPoint.y - GameSpaceStartAreaByPoint.y) / CellSize.y));
      _gameSpaceStartByCell = ConvertPointToCell(GameSpaceStartAreaByPoint);
      _gameSpaceEndByCell = ConvertPointToCell(new Vector3(GameSpaceEndAreaByPoint.x-CellSize.x/2,GameSpaceEndAreaByPoint.y-CellSize.y/2,GameSpaceEndAreaByPoint.z));
    }
    private void SetVariable()
    {
      _barracksList = new List<IRealProduct>();
      _powerPlantsList = new List<IRealProduct>();
      _grid = _setFormEditorGrid;
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
      return new Vector3(cellPos.x * CellSize.x,cellPos.y*CellSize.y,_myPosition.z);
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
    
    private  void TriggerBarracksCreater()
    {
      Vector2Int barracksAndAttacmentSize = new Vector2Int(_barracksSizeByCell.x + 2, _barracksSizeByCell.y + 2);
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(barracksAndAttacmentSize, _firstSearchCellPosition);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), _barracksSizeByCell);
      IRealProduct barracks =  barracksCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell,_barracksSizeByCell);
      barracks.MyTransform.SetParent(BarracksStore);
      _barracksList.Add(barracks);
    }
    
    private  void TriggerPowerPlantCreater()
    {
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(_powerPlantSizeByCell, _firstSearchCellPosition);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), _powerPlantSizeByCell);
      IRealProduct powerPlant =  powerPlantCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell,_powerPlantSizeByCell);
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
      while (IsSpawnable == false)
      {
        containsCounter = 0;
        for (int i = searchCellPosition.x; i <= gameObjectSizeByCell.x + searchCellPosition.x; i++)
        {
          for (int j = searchCellPosition.y; j <= gameObjectSizeByCell.y + searchCellPosition.y; j++)
            {
              containsCounter += IsCellValidForCreate(new Vector2Int(i,j),allProductsOnGameSpace);
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
    
    public static int IsCellValidForCreate(Vector2Int searchPositionByCell,List<IRealProduct> allProductsOnGameSpace)
    {
      foreach (var product in allProductsOnGameSpace)
      {
        for (int i = product.StartPositionByCell.x; i <= product.EndPositionByCell.x; i++)
        {
          for (int j = product.StartPositionByCell.y; j <= product.EndPositionByCell.y; j++)
          {
            if (searchPositionByCell == new Vector2Int(i, j))
            {
              return 1;
            }
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

    public static Vector3 SpawnPointFinder(Vector3 spawnPoint ,Vector2Int gameObjectSizeByCell)
    {
      Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
      return new Vector3(spawnPoint.x + ((floatVector2.x / 2) * CellSize.x), spawnPoint.y + ((floatVector2.y / 2) * CellSize.y), spawnPoint.z);

    }
    

    #endregion
    
    private static List<IRealProduct> ShowAllBuildingList (List<IRealProduct> barracksList, List<IRealProduct> powerPlantList)
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

