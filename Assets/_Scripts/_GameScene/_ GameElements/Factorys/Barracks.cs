
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Creater.BarackCreaters;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks>, IPortable, IGameObject,IProduct
    {

        #region Public Property
        
        #region Statics

        public static Vector2Int GameObjectSizeByCell { get { return _staticGameObjectSizeByCell; } }

        #endregion

        #region Events
        
        public UnityEvent CreateHeavySoldier;
        public UnityEvent CreateMediumSoldier;
        public UnityEvent CreateLightSoldier;

        #endregion

        #region Barracks

        public Transform HeavySoldierBarrack { get => _heavySoldierBarracks; }
        public Transform MediumSoldierBarrack { get => _mediumSoldierBarracks; }
        public Transform LightSoldierBarrrack { get => _lightSoldierBarracks; }

        #endregion

        #region Regular

        public Vector2Int StartPositionByCell { get => _startPositionByCell; }
        
        public Vector2Int EndPositionByCell { get => _endPositionByCell; }
        public Transform MyTransform
        {
            get => this.transform;
        }
        public List<IGameSpaceOccupanter> Occupanters
        {
            get => _occupanters;
        }
        public List<IProduct> BuildingProductList { get => ShowAllProduct(_heavySoldierList,_mediumSoldierList,_lightSoldierList); }

        #endregion
        
        #endregion
        
        #region Private Variable

        #region Static

        private readonly static Vector2Int _staticGameObjectSizeByCell = new Vector2Int(4, 4);

        #endregion
      
        #region Barracks
        
        [SerializeField]private Transform _heavySoldierBarracks;
        [SerializeField]private Transform _mediumSoldierBarracks;
        [SerializeField]private Transform _lightSoldierBarracks;
        
        #endregion

        
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private List<Vector2Int> _spawnPositionListByCell;
        private List<Vector2Int> _spawnPointList;
        private List<IProduct> _lightSoldierList;
        private List<IProduct> _mediumSoldierList;
        private List<IProduct> _heavySoldierList;
        [SerializeField]private List<IGameSpaceOccupanter> _occupanters;
        private LightSoldierCreater lightSoldierCreater;
        private MediumSoldierCreater _mediumSoldierCreater;
        private HeavySoldierCreater _heavySoldierCreater;

        #endregion

        #region OnStart Func.

        public void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell,List<Vector2Int> spawnPositionListByCell)
        {
            SetVariable();
            AddListener();
            OnStartSetSpawnPositionList(spawnPositionListByCell);
            OnStartSetPositionsByCell(startPositionByCell,endPositionByCell);
            
            
        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }

        private void OnStartSetSpawnPositionList(List<Vector2Int> spawnPositionListByCell)
        {
            foreach (var spawnPositionByCell in spawnPositionListByCell)
            {
                _occupanters.Add(new SpawnPosition(spawnPositionByCell));
            }
            
        }
        
        private void AddListener()
        {
            CreateHeavySoldier.AddListener(TriggerHeavySoldierCreater);
            CreateMediumSoldier.AddListener(TriggerMediumSoldierCreater);
            CreateLightSoldier.AddListener(TriggerLightSoldierCreater);
        }

        private void SetVariable()
        {
            _lightSoldierList = new List<IProduct>();
            _mediumSoldierList = new List<IProduct>();
            _heavySoldierList = new List<IProduct>();
            _occupanters = new List<IGameSpaceOccupanter>();
        }

        #endregion

        #region EventTrigger Func.

        private void TriggerHeavySoldierCreater()
        {
            IProduct product  =_heavySoldierCreater.FactoryMethod();
            _heavySoldierList.Add(product);
        }
        private void TriggerMediumSoldierCreater()
        {
            IProduct product =_heavySoldierCreater.FactoryMethod();
            _mediumSoldierList.Add(product);
        }
        private void TriggerLightSoldierCreater()
        {
            IProduct product =_heavySoldierCreater.FactoryMethod();
            _lightSoldierList.Add(product);
        }

        #endregion
        
        private List<IProduct> ShowAllProduct(List<IProduct> heavySoldierList, List<IProduct> mediumSoldierList, List<IProduct> lightSoldierList)
        {
            List<IProduct> AllSoldierInBarracks = new List<IProduct>();
            foreach (var heavySoldier in heavySoldierList)
            {
                AllSoldierInBarracks.Add(heavySoldier);
            }
            foreach (var mediumSoldier in mediumSoldierList)
            {
                AllSoldierInBarracks.Add(mediumSoldier);
            }
            foreach (var lightSoldier in lightSoldierList)
            {
                AllSoldierInBarracks.Add(lightSoldier);
            }

            return AllSoldierInBarracks;
        }

        private List<IGameSpaceOccupanter> ShowAllOccupanters(List<IProduct> heavySoldierList, List<IProduct> mediumSoldierList, List<IProduct> lightSoldierList)
        {
            List<IGameSpaceOccupanter> AllSoldierInBarracks = new List<IGameSpaceOccupanter>();
            foreach (var heavySoldier in heavySoldierList)
            {
                AllSoldierInBarracks.Add(heavySoldier);
            }
            foreach (var mediumSoldier in mediumSoldierList)
            {
                AllSoldierInBarracks.Add(mediumSoldier);
            }
            foreach (var lightSoldier in lightSoldierList)
            {
                AllSoldierInBarracks.Add(lightSoldier);
            }

            return AllSoldierInBarracks;
            
        }



    }
}
