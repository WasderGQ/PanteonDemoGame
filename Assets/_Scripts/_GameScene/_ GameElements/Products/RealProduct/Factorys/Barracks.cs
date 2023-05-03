using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater.Barracks;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products.RealProduct.GameObjectUtility;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using _Scripts.Data.Enums;
using _Scripts.Data.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene.__GameElements.Products.RealProduct.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks>, IVulnerable, IMovable, IPaneled,IRealProduct
    {

        #region ReadOnly Variable

        private readonly Vector2Int _heavySoldierSizeByCell = new Vector2Int(1, 1);
        private readonly Vector2Int _mediumSoldierSizeByCell = new Vector2Int(1, 1);
        private readonly Vector2Int _lightSoldierSizeByCell = new Vector2Int(1, 1);
        public readonly Vector2Int SpawnPositionSizeByCell = new Vector2Int(1, 1);

        #endregion

        #region Data

        [SerializeField] private BuildingTypeData _buildingTypeData;
        [SerializeField] private GameObject _myPanel;
        
        #endregion

        #region Public Property

        #region MyRegion

        public List<IRealProduct> RealProductList
        {
            get => _realProducts;
        }
        private List<IRealProduct> _realProducts;
        #endregion
        
        
        #region Events

        public UnityEvent<IRealProduct> RemoveMe { get; }
        
        
        public UnityEvent EventCreateHeavySoldier { get => _eventCreateHeavySoldier; }
        public UnityEvent EventCreateMediumSoldier { get => _eventCreateMediumSoldier; }
        public UnityEvent EventCreateLightSoldier { get => _eventCreateLightSoldier; }
        public UnityEvent<IAttacker> EventTakeDamage { get => _eventTakeDamage; }

        #endregion

        #region Barracks Objects

        public Transform HeavySoldierBarrack { get; }
        public Transform MediumSoldierBarrack { get; }
        public Transform LightSoldierBarrrack { get; } 
        public Transform SpawnPositionHolder { get; }
        
        #endregion
        
        #region Regular
        
        public GameObject MyPanel
        {
            get => _myPanel;
        }
        
        public Vector2Int StartPositionByCell
        {
            get => _startPositionByCell;
        }

        public Vector2Int EndPositionByCell
        {
            get => _endPositionByCell;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
        }

        public GameObject MyGameObject
        {
            get => gameObject;
        }

        public List<IRealProduct> ProductList
        {
            get => ShowAllIRealProductList(_heavySoldierList, _mediumSoldierList, _lightSoldierList, SpawnPositionList);
        }


        public List<SpawnPosition> SpawnPositionList
        {
            get => _spawnPositionList;
        }

        #endregion

        #endregion

        #region Private Variable
        
        #region Events

        private UnityEvent _eventCreateHeavySoldier;
        private UnityEvent _eventCreateMediumSoldier;
        private UnityEvent _eventCreateLightSoldier;
        private UnityEvent<Vector2Int> _eventMove;
        private UnityEvent<IAttacker> _eventTakeDamage;

        #endregion
        
        #region Barracks && SpawnPositions

        [SerializeField] private Transform _spawnPositions;
        [SerializeField] private Transform _heavySoldierBarracks;
        [SerializeField] private Transform _mediumSoldierBarracks;
        [SerializeField] private Transform _lightSoldierBarracks;

        #endregion

        #region Regular

        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private List<IRealProduct> _lightSoldierList;
        private List<IRealProduct> _mediumSoldierList;
        private List<IRealProduct> _heavySoldierList;
        [SerializeField] private List<SpawnPosition> _spawnPositionList;
        private LightSoldierCreater _lightSoldierCreater;
        private MediumSoldierCreater mediumSoldierCreater;
        private HeavySoldierCreater _heavySoldierCreater;
        private int _maxHealth;
        private int _currentHealth;
        private Vector3 _mementoSpawnedPosition;
        #endregion


        #endregion

        #region OnStart Func.

        public void InIt(Vector2Int startPositionByCell, Vector2Int endPositionByCell, List<SpawnPosition> spawnPositionList)
        {
            SetVariable();
            AddListener();
            OnStartSetPositionsByCell(startPositionByCell, endPositionByCell);
            OnStartSetSpawnPositionList(spawnPositionList);

        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell, Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }

        private void OnStartSetSpawnPositionList(List<SpawnPosition> spawnPositionList)
        {
            _spawnPositionList = spawnPositionList;
            foreach (var product in spawnPositionList)
            {
                ProductList.Add(product);
            }
            
        }

        private void AddListener()
        {
            EventTakeDamage.AddListener(TakeDamage);
            EventCreateHeavySoldier.AddListener(TriggerHeavySoldierCreater);
            EventCreateMediumSoldier.AddListener(TriggerMediumSoldierCreater);
            EventCreateLightSoldier.AddListener(TriggerLightSoldierCreater);

        }

        private void SetVariable()
        {
            
            _eventMove = new UnityEvent<Vector2Int>();
            _eventCreateHeavySoldier = new UnityEvent();
            _eventCreateMediumSoldier = new UnityEvent();
            _eventCreateLightSoldier = new UnityEvent();
            _eventTakeDamage = new UnityEvent<IAttacker>();
            _maxHealth = _buildingTypeData._buildingTypeList[(int)EnumBuildingType.Barracks].Health;
            _currentHealth = _maxHealth;
            _lightSoldierList = new List<IRealProduct>();
            _mediumSoldierList = new List<IRealProduct>();
            _heavySoldierList = new List<IRealProduct>();
            _spawnPositionList = new List<SpawnPosition>();
            _lightSoldierCreater = new LightSoldierCreater();
            mediumSoldierCreater = new MediumSoldierCreater();
            _heavySoldierCreater = new HeavySoldierCreater();
            _mementoSpawnedPosition = transform.position;
            _realProducts = new List<IRealProduct>();
        }

        #endregion

        #region EventTrigger Func.

        private  void  TriggerHeavySoldierCreater()
        {

            Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
            if (spawnCellPositionByCell != new Vector2Int())
            {
                Vector3 spawnPositionByPoint = CreatePointFixer(GameSpace.ConvertCellToPoint(spawnCellPositionByCell), Soldier.GameSpaceSizeByCell);
                HeavySoldier heavySolider = _heavySoldierCreater.FactoryMethod(spawnPositionByPoint, spawnCellPositionByCell, _heavySoldierSizeByCell);
                heavySolider.transform.SetParent(_heavySoldierBarracks);
                RealProductList.Add(heavySolider);
            }
            else
            {
                Debug.Log("I can't create HeavySoldier beacuse all spawnPositions full");
            }

        }

        private  void TriggerMediumSoldierCreater()
        {
            Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
            if (spawnCellPositionByCell != new Vector2Int())
            {
                Vector3 spawnPositionByPoint = CreatePointFixer(GameSpace.ConvertCellToPoint(spawnCellPositionByCell), Soldier.GameSpaceSizeByCell);
                MediumSoldier mediumSolider =  mediumSoldierCreater.FactoryMethod(spawnPositionByPoint, spawnCellPositionByCell, _mediumSoldierSizeByCell);
                mediumSolider.transform.SetParent(_mediumSoldierBarracks);
                RealProductList.Add(mediumSolider);
                
            }
            else
            {
                Debug.Log("I can't create HeavySoldier beacuse all spawnPositions full");
            }
        }

        private  void TriggerLightSoldierCreater()
        {
            Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
            if (spawnCellPositionByCell != new Vector2Int())
            {
                Vector3 spawnPositionByPoint = CreatePointFixer(GameSpace.ConvertCellToPoint(spawnCellPositionByCell), Soldier.GameSpaceSizeByCell);
                LightSoldier lightSoldier =  _lightSoldierCreater.FactoryMethod(spawnPositionByPoint, spawnCellPositionByCell, _lightSoldierSizeByCell);
                lightSoldier.transform.SetParent(_lightSoldierBarracks);
                RealProductList.Add(lightSoldier);
            }
            else
            {
                Debug.Log("I can't create HeavySoldier beacuse all spawnPositions full");
            }
        }

        #endregion

        #region SpawnPoints Func.

        private Vector2Int RandomlyGiveSpawnPositionFromList(List<SpawnPosition> spawnPositionList)
        {
            List<SpawnPosition> spawnableSpawnPositions = GetSpawnableSpawnPositions(spawnPositionList);
            SpawnPosition choosedSpawnPosition = spawnableSpawnPositions[0];
            ChangeChoosedSpawnPointStatuOnList(false, choosedSpawnPosition, spawnPositionList);
            return choosedSpawnPosition.StartPositionByCell;
        }

        private List<SpawnPosition> GetSpawnableSpawnPositions(List<SpawnPosition> spawnPositionList)
        {
            List<SpawnPosition> spawnableSpawnPositions = new List<SpawnPosition>();
            foreach (var spawnPosition in spawnPositionList)
            {
                if (spawnPosition.IsUsable)
                {
                    spawnableSpawnPositions.Add(spawnPosition);
                }
            }

            return spawnableSpawnPositions;
        }



        private void ChangeChoosedSpawnPointStatuOnList(bool statu, SpawnPosition choosedSpawnPosition, List<SpawnPosition> spawnPositionList)
        {
            int index = spawnPositionList.FindIndex(x => x.StartPositionByCell == choosedSpawnPosition.StartPositionByCell);
            spawnPositionList[index].ChangeUsableStat(false);
        }

        public void SetBackSpawnPosition(Vector2Int givenVector2Int)
        {
            int index = SpawnPositionList.FindIndex(x => x.StartPositionByCell == givenVector2Int);
            SpawnPositionList[index].ChangeUsableStat(true);
        }

        #endregion

        #region Property Func.

        private List<IRealProduct> ShowAllIRealProductList(List<IRealProduct> heavySoldierList, List<IRealProduct> mediumSoldierList, List<IRealProduct> lightSoldierList, List<SpawnPosition> spawnPotionsList)
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

            /*foreach (var VARIABLE in AllSoldierInBarracks)
            {
                for (int i = VARIABLE.StartPositionByCell.x; i <= VARIABLE.EndPositionByCell.x; i++)
                {
                    for (int j = VARIABLE.StartPositionByCell.y; j <= VARIABLE.EndPositionByCell.y; j++)
                    {
                        Vector2Int temp = new Vector2Int(i, j);
                        Debug.Log(temp);
                    }
                    
                }
            }*/
            return AllSoldierInBarracks;

        }

        #endregion

        #region Vulnerable Func.

        public void TakeDamage(IAttacker attacker)
        {
            Debug.Log(_currentHealth);
            _currentHealth = _currentHealth - attacker.Damage;
            Debug.Log(_currentHealth);
            CheckAmIDead();
        }

        private void CheckAmIDead()
        {
            if (_currentHealth <= 0)
            {
                transform.SetParent(BarracksPool.SharedInstance.ParentPoolObject);
                BarracksPool.SharedInstance.PooledObjects.Add(this);
                gameObject.SetActive(false);
                
                
            }
        }



        #endregion

        #region Move Func.

        public void TrueMove(Vector3 mousePosition)
        {
            _mementoSpawnedPosition = mousePosition;
            
        }
        
        
        public void Move(Vector3 mousePosition)
        {
            transform.position = mousePosition;
            
        }

        public void MoveDefault()
        {
            transform.position = _mementoSpawnedPosition;
        }

        #endregion

        private Vector3 CreatePointFixer(Vector3 spawnPoint, Vector2Int gameObjectSizeByCell)
        {
            Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
            return new Vector3(spawnPoint.x + ((floatVector2.x / 2) * GameSpace.CellSize.x), spawnPoint.y + ((floatVector2.y / 2) * GameSpace.CellSize.y), spawnPoint.z);

        }


        

        

        

        
    }
}




    

