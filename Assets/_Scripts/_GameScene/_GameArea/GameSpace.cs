using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.FactoryBuilder.FactoryCreater;
using _Scripts._GameScene.__GameElements.Products.RealProduct;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Factorys;
using UnityEngine;
using UnityEngine.Events;


namespace _Scripts._GameScene._GameArea
{
  public class GameSpace : MonoBehaviour //must be singleton
  {

    private FactoryBuilder _factoryBuilder;



    #region private Variable Vector,Point etc.

    #region Public Property Given GameSpace Start-End Position By Point

    public Vector3 GameSpaceStartAreaByPoint
    {
      get => _gameSpaceStartByPoint;
    }

    public Vector3 GameSpaceEndAreaByPoint
    {
      get => _gameSpaceEndByPoint;
    }

    #region Public Property's field  //Setting on Start

    private Vector3 _gameSpaceStartByPoint;
    private Vector3 _gameSpaceEndByPoint;

    #endregion

    #endregion

    #region Public Property Given GameSpace Start-End Position By Cell

    public static Vector2Int GameSpaceStartAreaByCell
    {
      get => _gameSpaceStartByCell;
    }

    public static Vector2Int GameSpaceEndAreaByCell
    {
      get => _gameSpaceEndByCell;
    }

    #region Public Property's field  //Setting on Start

    private static Vector2Int _gameSpaceEndByCell;
    private static Vector2Int _gameSpaceStartByCell;

    #endregion

    #endregion

    #region Static Propertys

    public static Vector2 CellSize
    {
      get => _cellSize;
    }

    public static List<IRealProduct> GameSpaceRealProduct
    {
      get => GetGameSpaceRealProducts();
    }

    private static List<IRealProduct> _gameSpaceRealProducts;


    #endregion





    #region Private Variable

    #endregion

    private readonly Vector2Int _powerPlantSizeByCell = new Vector2Int(2, 3);
    private readonly Vector2Int _barracksSizeByCell = new Vector2Int(4, 4);
    private static Vector3 _myPosition; //Using Static Convert Func. Inside , Take to my Pozition.z.
    private static Grid _grid; //Using Static Convert Func. Inside , Take to my grid Sizes Form GridGameObject.
    private static Vector2 _cellSize;
    private static Vector2Int _gameSpaceSizeByCell;
    [SerializeField] private Vector2Int _firstSearchCellPosition; //Editable Form Editor When Create something first looking on GameSpace

    #endregion

    #region TransForm Parents and Setable From GameObjects

    [SerializeField] private Transform _barracksStore;
    [SerializeField] private Transform _powerPlantStore;
    [SerializeField] private Grid _setFormEditorGrid;

    #endregion

    #region Public Property Store To CreatedGameObjects

    public Transform BarracksStore
    {
      get => _barracksStore;
    }

    public Transform PowerPlantStore
    {
      get => _powerPlantStore;
    }

    #endregion

    #region Public RealProductLists


    #endregion

    #region Public UnityEvents

    public UnityEvent<Barracks> RemoveBarracksFormList;
    public UnityEvent<PowerPlant> RemovePowerPlantsFormList;



    #endregion

    #region Start Func.

    public void InIt()
    {
      SetVariable();
      SetGameSpaceAreaCoordinate();
      AddListener();
    }

    private void SetGameSpaceAreaCoordinate()
    {
      _cellSize = new Vector2(_grid.cellSize.x, _grid.cellSize.y);
      _gameSpaceEndByPoint = new Vector3(500, 500, 100);
      _gameSpaceStartByPoint = transform.position;
      _gameSpaceSizeByCell = new Vector2Int(Convert.ToInt32((GameSpaceEndAreaByPoint.x - GameSpaceStartAreaByPoint.x) / CellSize.x), Convert.ToInt32((GameSpaceEndAreaByPoint.y - GameSpaceStartAreaByPoint.y) / CellSize.y));
      _gameSpaceStartByCell = ConvertPointToCell(GameSpaceStartAreaByPoint);
      _gameSpaceEndByCell = ConvertPointToCell(new Vector3(GameSpaceEndAreaByPoint.x - CellSize.x / 2, GameSpaceEndAreaByPoint.y - CellSize.y / 2, GameSpaceEndAreaByPoint.z));
    }

    private void SetVariable()
    {
      _grid = _setFormEditorGrid;
      _myPosition = transform.position;
      _factoryBuilder = new FactoryBuilder();
      _gameSpaceRealProducts = new List<IRealProduct>();
    }

    private void AddListener()
    {


    }

    #endregion


    #region Public Static CellToPosition - PositionToCell

    public static Vector2Int ConvertPointToCell(Vector3 worldPos)
    {
      Vector3Int cellPos = _grid.WorldToCell(worldPos);
      return new Vector2Int(cellPos.x, cellPos.y);
    }

    public static Vector3 ConvertCellToPoint(Vector2Int cellPos)
    {
      return new Vector3(cellPos.x * CellSize.x, cellPos.y * CellSize.y, _myPosition.z);
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
      Vector2Int barracksAndAttacmentSize = new Vector2Int(_barracksSizeByCell.x + 2, _barracksSizeByCell.y + 2);
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(barracksAndAttacmentSize, _firstSearchCellPosition);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), _barracksSizeByCell);
      Barracks barracks = _factoryBuilder.FactoryMethod1(spawnPositionByPoint, spawnCellPositionByCell, _barracksSizeByCell);
      barracks.transform.SetParent(BarracksStore);
      _gameSpaceRealProducts.Add(barracks);
    }

    private void TriggerPowerPlantCreater()
    {
      Vector2Int spawnCellPositionByCell = SearchEmptyCellForSpawn(_powerPlantSizeByCell, _firstSearchCellPosition);
      Vector3 spawnPositionByPoint = SpawnPointFinder(ConvertCellToPoint(spawnCellPositionByCell), _powerPlantSizeByCell);
      PowerPlant powerPlant = _factoryBuilder.FactoryMethod2(spawnPositionByPoint, spawnCellPositionByCell, _powerPlantSizeByCell);
      powerPlant.transform.SetParent(PowerPlantStore);
      _gameSpaceRealProducts.Add(powerPlant);
    }


    #endregion

    #region SearchEmpthyCellForCreate Func.

    public Vector2Int SearchEmptyCellForSpawn(Vector2Int gameObjectSizeByCell, Vector2Int startPointByCell)
    {

      bool IsSpawnable = false;
      int containsCounter = 0;
      Vector2Int searchCellPosition = startPointByCell;

      while (IsSpawnable == false)
      {
        containsCounter = 0;
        for (int i = searchCellPosition.x; i <= gameObjectSizeByCell.x + searchCellPosition.x; i++)
        {
          for (int j = searchCellPosition.y; j <= gameObjectSizeByCell.y + searchCellPosition.y; j++)
          {
            containsCounter += IsCellValidForCreate(new Vector2Int(i, j), GameSpaceRealProduct);
          }
        }

        if (containsCounter == 0)
        {
          IsSpawnable = true;
          return searchCellPosition;
        }

        searchCellPosition = SearchCellCoordinateAdvanceFixer(searchCellPosition, gameObjectSizeByCell);

        if (searchCellPosition == startPointByCell)
        {
          Debug.Log("I search all cells on GameSpace but there isn't empty cell for create");

        }
      }

      return new Vector2Int();

    }

    public static int IsCellValidForCreate(Vector2Int searchPositionByCell, List<IRealProduct> allProductsOnGameSpace)
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

    private Vector2Int SearchCellCoordinateAdvanceFixer(Vector2Int searchPositionByCell, Vector2Int gameObjectSizeByCell)
    {
      Vector2Int newSearchPositionByCell = new Vector2Int(searchPositionByCell.x + 1, searchPositionByCell.y);
      if (newSearchPositionByCell.x + gameObjectSizeByCell.x > GameSpaceEndAreaByCell.x)
      {
        newSearchPositionByCell = new Vector2Int(0, newSearchPositionByCell.y + 1);
      }

      if (searchPositionByCell.y + gameObjectSizeByCell.y > GameSpaceEndAreaByCell.y)
      {
        newSearchPositionByCell = new Vector2Int(0, 0);
      }

      return newSearchPositionByCell;
    }

    #endregion

    #region Position Operators

    public static Vector3 SpawnPointFinder(Vector3 spawnPoint, Vector2Int gameObjectSizeByCell)
    {
      Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
      return new Vector3(spawnPoint.x + ((floatVector2.x / 2) * CellSize.x), spawnPoint.y + ((floatVector2.y / 2) * CellSize.y), spawnPoint.z);

    }

    #endregion

    private static List<IRealProduct> GetGameSpaceRealProducts()
    {
      List<IRealProduct> allProducts = new List<IRealProduct>();
      foreach (var product in _gameSpaceRealProducts)
      {
        allProducts.Add(product);
        foreach (var product1 in product.ProductList)
        {
          allProducts.Add(product1);
        }
      }

      return allProducts;
    }
  }





}



