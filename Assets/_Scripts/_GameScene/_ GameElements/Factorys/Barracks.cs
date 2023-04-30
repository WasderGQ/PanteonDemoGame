
using System;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products.GameObjectUtility;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts.Data.Enums;
using _Scripts.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks>, IPortable, IGameObject,IRealProduct,IVulnerable
    {
        #region Data

        [SerializeField] private BuildingTypeData _buildingTypeData;

        #endregion
        #region Public Property
        
        #region Statics

        public static Vector2Int GameObjectSizeByCell { get { return _GameObjectSizeByCell; } }

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
            get => transform;
        }
        public List<IRealProduct> ProductList { get => ShowAllIRealProductList(_heavySoldierList,_mediumSoldierList,_lightSoldierList,_spawnPositionList); }

        #endregion
        
        #endregion
        
        #region Private Variable

        #region Static

        private readonly static Vector2Int _GameObjectSizeByCell = new Vector2Int(4, 4);

        #endregion
      
        #region Barracks
        
        [SerializeField]private Transform _heavySoldierBarracks;
        [SerializeField]private Transform _mediumSoldierBarracks;
        [SerializeField]private Transform _lightSoldierBarracks;
        
        #endregion

        
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private List<IRealProduct> _lightSoldierList;
        private List<IRealProduct> _mediumSoldierList;
        private List<IRealProduct> _heavySoldierList;
        private List<IRealProduct> _spawnPositionList;
        private LightSoldierCreater _lightSoldierCreater;
        private MediumSoldierCreater _mediumSoldierCreater;
        private HeavySoldierCreater _heavySoldierCreater;

        #endregion

        #region OnStart Func.

        public void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell,List<IRealProduct> spawnPositionList)
        {
            SetVariable();
            AddListener();
            OnStartSetSpawnPositionList(spawnPositionList);
            OnStartSetPositionsByCell(startPositionByCell,endPositionByCell);
            
            
        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }

        private void OnStartSetSpawnPositionList(List<IRealProduct> spawnPositionList)
        {
            foreach (var spawnPosition in spawnPositionList)
            {
                _spawnPositionList.Add(spawnPosition);
            }
            
        }
        
        private void AddListener()
        {   
            EventTakeDamage.AddListener(TakeDamage);
            CreateHeavySoldier.AddListener(TriggerHeavySoldierCreater);
            CreateMediumSoldier.AddListener(TriggerMediumSoldierCreater);
            CreateLightSoldier.AddListener(TriggerLightSoldierCreater);
        }

        private void SetVariable()
        {
            _maxHealth= _buildingTypeData._buildingTypeList[(int)EnumBuildingType.Barracks].Health;
            _currentHealth = _maxHealth;
            _lightSoldierList = new List<IRealProduct>();
            _mediumSoldierList = new List<IRealProduct>();
            _heavySoldierList = new List<IRealProduct>();
            _spawnPositionList = new List<IRealProduct>();
            _lightSoldierCreater = new LightSoldierCreater();
            _mediumSoldierCreater = new MediumSoldierCreater();
            _heavySoldierCreater = new HeavySoldierCreater();
        }

        #endregion

        #region EventTrigger Func.

        private void TriggerHeavySoldierCreater()
        {
            
                Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
                if (spawnCellPositionByCell != new Vector2Int())
                {
                    Vector3 spawnPositionByPoint = SpawnPointFinder(GameSpace.ConvertCellToPoint(spawnCellPositionByCell),Soldier.GameSpaceSizeByCell);
                    IRealProduct heavySolider = _heavySoldierCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
                    if (heavySolider != null)
                    {
                        heavySolider.MyTransform.SetParent(_heavySoldierBarracks);
                        _heavySoldierList.Add(heavySolider);
                        _spawnPositionList.Remove(new SpawnPosition(spawnCellPositionByCell));
                    }
                    else
                    {
                        Debug.Log("I cant use created HeavySoldier beacuse inside empty ");
                    }
                    
                    
                }
                else
                {
                    Debug.Log("I can't create HeavySoldier beacuse all spawnPositions full");
                }
                
        }
            
                
            
            
        
            

        
        private void TriggerMediumSoldierCreater()
        {
            try
            {
                Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
                if (spawnCellPositionByCell != new Vector2Int())
                {
                    Vector3 spawnPositionByPoint = SpawnPointFinder(GameSpace.ConvertCellToPoint(spawnCellPositionByCell),Soldier.GameSpaceSizeByCell);
                    IRealProduct mediumSoldier = _mediumSoldierCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
                    mediumSoldier.MyTransform.SetParent(_mediumSoldierBarracks);
                    _mediumSoldierList.Add(mediumSoldier);
                }
                else
                {
                    Debug.Log("I can't create Mediumsoldier beacuse all spawnPositions full"); 
                }
            }
            catch 
            {
                Debug.Log("I cant create MediumSoldier because Pool Empty");
            }
            
            
            
            
        }
        private void TriggerLightSoldierCreater()
        {
            try
            {
                Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
                if (spawnCellPositionByCell != new Vector2Int())
                {
                    Vector3 spawnPositionByPoint = SpawnPointFinder(GameSpace.ConvertCellToPoint(spawnCellPositionByCell),Soldier.GameSpaceSizeByCell);
                    IRealProduct lightSolider = _lightSoldierCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
                    lightSolider.MyTransform.SetParent(_lightSoldierBarracks);
                    _lightSoldierList.Add(lightSolider);
                }
                else
                {
                    Debug.Log("I can't create Lightsoldier beacuse all spawnPositions full");
                }
            }
            catch
            {
                Debug.Log("I cant create LightSoldier because Pool Empty");
            }
        }
        #endregion
        

        private List<IRealProduct> ShowAllIRealProductList (List<IRealProduct> heavySoldierList, List<IRealProduct> mediumSoldierList, List<IRealProduct> lightSoldierList,List<IRealProduct> spawnPotionsList)
        {
            List<IRealProduct> AllSoldierInBarracks = new List<IRealProduct>();
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
            foreach (var spawnPotion in spawnPotionsList)
            {
                AllSoldierInBarracks.Add(spawnPotion);
            }
            return AllSoldierInBarracks;
            
        }

        private Vector2Int RandomlyGiveSpawnPositionFromList(List<IRealProduct> spawnPositionList)
        {
            try
            {
                IRealProduct RandomlyChoosedSpawnPosition = spawnPositionList[Convert.ToInt32(Random.Range(0, spawnPositionList.Count))];
                EraseSpawnPointOnList(RandomlyChoosedSpawnPosition, spawnPositionList);
                return RandomlyChoosedSpawnPosition.StartPositionByCell;
            }
            catch
            {
                
                Debug.Log("SpawnPosition All Full");
                
            }

            return new Vector2Int();
        }
            
        
        

        private void EraseSpawnPointOnList(IRealProduct spawnPosition,List<IRealProduct> spawnPositionList)
        {
            int index = spawnPositionList.FindIndex(x => x.StartPositionByCell == spawnPosition.StartPositionByCell);
            spawnPositionList.RemoveAt(index);
        }
        private Vector3 SpawnPointFinder(Vector3 spawnPoint ,Vector2Int gameObjectSizeByCell)
        {
            Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
            return new Vector3(spawnPoint.x + ((floatVector2.x / 2) * GameSpace.GameSpaceCellSize.x), spawnPoint.y + ((floatVector2.y / 2) * GameSpace.GameSpaceCellSize.y), spawnPoint.z);

        }

        private int _maxHealth;
        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
        }
        public UnityEvent<IAttacker> EventTakeDamage { get; }
        public void TakeDamage(IAttacker attacker)
        {
            _currentHealth = -attacker.Damage;
            Debug.Log($"Taken {attacker.Damage}");
        }
    }
}
