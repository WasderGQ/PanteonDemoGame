
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

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks>,IRealProduct,IVulnerable,IMovable,IPaneled
    {
        
        #region Data

        [SerializeField] private BuildingTypeData _buildingTypeData;
        [SerializeField] private SpawnPosition _spawnPosition;
        [SerializeField] private Transform _gameSpace;
        [SerializeField] private GameObject _myPanel;
        #endregion
        
        #region Public Property
        
        public GameObject MyPanel
        {
            get { return _myPanel; }
        }
        
        
        #region Statics
       
        public static Vector2Int GameObjectSizeByCell { get { return _GameObjectSizeByCell; } }
       
        #endregion

        #region Events
        
        public UnityEvent EventCreateHeavySoldier { get => _eventCreateHeavySoldier; }
        public UnityEvent EventCreateMediumSoldier { get => _eventCreateMediumSoldier; }
        public UnityEvent EventCreateLightSoldier { get => _eventCreateLightSoldier; }
        public UnityEvent<Vector2Int> EventMove { get => _eventMove; }
        public UnityEvent<IAttacker> EventTakeDamage { get => _eventTakeDamage; }
        
        #endregion

        #region Barracks

        public Transform HeavySoldierBarrack { get => _heavySoldierBarracks; }
        public Transform MediumSoldierBarrack { get => _mediumSoldierBarracks; }
        public Transform LightSoldierBarrrack { get => _lightSoldierBarracks; }

        #endregion

        #region Regular

        public Vector2Int StartPositionByCell { get => _startPositionByCell; }
        public Vector2Int EndPositionByCell { get => _endPositionByCell; }
        public Transform MyTransform { get => transform; }
        public int CurrentHealth { get => _currentHealth; }
        public GameObject MyGameObject { get => gameObject;}
        public List<IRealProduct> ProductList { get => ShowAllIRealProductList(_heavySoldierList,_mediumSoldierList,_lightSoldierList,SpawnPositionList); }

        public List<SpawnPosition> SpawnPositionList { get => _spawnPositionList; }
        
        #endregion

        #endregion
        
        #region Private Variable

        #region Static

        private readonly static Vector2Int _GameObjectSizeByCell = new Vector2Int(4, 4);

        #endregion

        #region Events
        
        private UnityEvent _eventCreateHeavySoldier;
        private UnityEvent _eventCreateMediumSoldier;
        private UnityEvent _eventCreateLightSoldier;
        private UnityEvent<Vector2Int> _eventMove;
        private UnityEvent<IAttacker> _eventTakeDamage;
        
        #endregion
        
        
        #region Barracks && SpawnPositions
        
        [SerializeField]private Transform _spawnPositions;
        [SerializeField]private Transform _heavySoldierBarracks;
        [SerializeField]private Transform _mediumSoldierBarracks;
        [SerializeField]private Transform _lightSoldierBarracks;
        
        #endregion

        #region Regular

        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private List<IRealProduct> _lightSoldierList;
        private List<IRealProduct> _mediumSoldierList;
        private List<IRealProduct> _heavySoldierList;
        [SerializeField]private List<SpawnPosition> _spawnPositionList;
        private LightSoldierCreater _lightSoldierCreater;
        private MedimuSoldierCreater _medimuSoldierCreater;
        private HeavySoldierCreater _heavySoldierCreater;
        private int _maxHealth;
        private int _currentHealth;

        #endregion
       
        
        #endregion

        #region OnStart Func.

        public void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            SetVariable();
            AddListener();
            OnStartSetPositionsByCell(startPositionByCell,endPositionByCell);
            OnStartSetSpawnPositionList();
            


        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }

        private void OnStartSetSpawnPositionList()
        {
            List<Vector2Int> spawmPositionListByCell = FindSpawnPositionsByCell(StartPositionByCell,EndPositionByCell);

          foreach (var SpawnPositionByCell in spawmPositionListByCell)
          {
              Vector3 transfomPosition =CreatePointFixer(GameSpace.ConvertCellToPoint(SpawnPositionByCell),new Vector2Int(1,1));
              SpawnPosition spawnPosition =Instantiate(_spawnPosition,transfomPosition,Quaternion.identity, _spawnPositions);
              spawnPosition.InIt(SpawnPositionByCell);
             _spawnPositionList.Add(spawnPosition);
          }
          
        }

        private void AddListener()
        {   
            EventMove.AddListener(MoveBarracks);
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
            _maxHealth= _buildingTypeData._buildingTypeList[(int)EnumBuildingType.Barracks].Health;
            _currentHealth = _maxHealth;
            _lightSoldierList = new List<IRealProduct>();
            _mediumSoldierList = new List<IRealProduct>();
            _heavySoldierList = new List<IRealProduct>();
            _spawnPositionList = new List<SpawnPosition>();
            _lightSoldierCreater = new LightSoldierCreater();
            _medimuSoldierCreater = new MedimuSoldierCreater();
            _heavySoldierCreater = new HeavySoldierCreater();
        }

        #endregion

        #region EventTrigger Func.

        private void TriggerHeavySoldierCreater()
        {
            
                Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
                if (spawnCellPositionByCell != new Vector2Int())
                {
                    Vector3 spawnPositionByPoint = CreatePointFixer(GameSpace.ConvertCellToPoint(spawnCellPositionByCell),Soldier.GameSpaceSizeByCell);
                    IRealProduct heavySolider = _heavySoldierCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
                    if (heavySolider != null)
                    {
                        heavySolider.MyTransform.SetParent(_heavySoldierBarracks);
                        _heavySoldierList.Add(heavySolider);
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
            Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
            if (spawnCellPositionByCell != new Vector2Int())
            {
                Vector3 spawnPositionByPoint = CreatePointFixer(GameSpace.ConvertCellToPoint(spawnCellPositionByCell),Soldier.GameSpaceSizeByCell);
                IRealProduct mediumSolider = _medimuSoldierCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
                if (mediumSolider != null)
                {
                    mediumSolider.MyTransform.SetParent(_mediumSoldierBarracks);
                    _mediumSoldierList.Add(mediumSolider);
                        
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
        private void TriggerLightSoldierCreater()
        {
            Vector2Int spawnCellPositionByCell = RandomlyGiveSpawnPositionFromList(_spawnPositionList);
            if (spawnCellPositionByCell != new Vector2Int())
            {
                Vector3 spawnPositionByPoint = CreatePointFixer(GameSpace.ConvertCellToPoint(spawnCellPositionByCell),Soldier.GameSpaceSizeByCell);
                IRealProduct lightSoldier = _lightSoldierCreater.FactoryMethod(spawnPositionByPoint,spawnCellPositionByCell);
                if (lightSoldier != null)
                {
                    lightSoldier.MyTransform.SetParent(_lightSoldierBarracks);
                    _heavySoldierList.Add(lightSoldier);
                        
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
        
        #endregion
        
        #region SpawnPoints Func.

        private Vector2Int RandomlyGiveSpawnPositionFromList(List<SpawnPosition> spawnPositionList)
        {
            List<SpawnPosition> spawnableSpawnPositions = GetSpawnableSpawnPositions(spawnPositionList);
            SpawnPosition choosedSpawnPosition = spawnableSpawnPositions[0];
            ChangeChoosedSpawnPointStatuOnList(false,choosedSpawnPosition,spawnPositionList);
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
        private List<Vector2Int> FindSpawnPositionsByCell(Vector2Int startPositionByCell, Vector2Int endPositionByCell)
        {
                List<Vector2Int> spawnPositionList = new List<Vector2Int>();
                Vector2Int spawnStartPositionByCell = new Vector2Int(startPositionByCell.x - 1, startPositionByCell.y - 1);
                Vector2Int spawnEndPositionByCell = new Vector2Int(endPositionByCell.x + 1, endPositionByCell.y + 1);

                for (int i = spawnStartPositionByCell.x; i < spawnEndPositionByCell.x; i++)
                {
                    if (i >= GameSpace.GameSpaceStartAreaByCell.x && i <= GameSpace.GameSpaceEndAreaByCell.x && spawnStartPositionByCell.y >= GameSpace.GameSpaceStartAreaByCell.y)
                    {
                        Vector2Int newSpawnPositionByCell = new Vector2Int(i, spawnStartPositionByCell.y);
                        int samePositionCount = GameSpace.IsCellValidForCreate(newSpawnPositionByCell, GameSpace.BuildingList);
                        if (samePositionCount == 0)
                        {
                            spawnPositionList.Add(newSpawnPositionByCell);
                        }

                    }
                }

                for (int i = spawnStartPositionByCell.y + 1; i <= spawnEndPositionByCell.y; i++)
                {
                    if (i >= GameSpace.GameSpaceStartAreaByCell.y && i <= GameSpace.GameSpaceEndAreaByCell.y && spawnStartPositionByCell.x >= GameSpace.GameSpaceStartAreaByCell.x)
                    {
                        Vector2Int newSpawnPositionByCell = new Vector2Int(spawnStartPositionByCell.x, i);
                        int samePositionCount = GameSpace.IsCellValidForCreate(newSpawnPositionByCell, GameSpace.BuildingList);
                        if (samePositionCount == 0)
                        {
                            spawnPositionList.Add(newSpawnPositionByCell);
                        }

                    }
                }

                for (int i = spawnStartPositionByCell.y; i < spawnEndPositionByCell.y; i++)
                {
                    if (i >= GameSpace.GameSpaceStartAreaByCell.y && i <= GameSpace.GameSpaceEndAreaByCell.y && spawnEndPositionByCell.x <= GameSpace.GameSpaceEndAreaByCell.x)
                    {
                        Vector2Int newSpawnPositionByCell = new Vector2Int(spawnEndPositionByCell.x, i);
                        int samePositionCount = GameSpace.IsCellValidForCreate(newSpawnPositionByCell, GameSpace.BuildingList);
                        if (samePositionCount == 0)
                        {
                            spawnPositionList.Add(newSpawnPositionByCell);
                        }
                    }
                }

                for (int i = spawnStartPositionByCell.x + 1; i <= spawnEndPositionByCell.x; i++)
                {
                    if (i >= GameSpace.GameSpaceStartAreaByCell.x && i <= GameSpace.GameSpaceEndAreaByCell.x && spawnEndPositionByCell.y <= GameSpace.GameSpaceEndAreaByCell.y)
                    {
                        Vector2Int newSpawnPositionByCell = new Vector2Int(i, spawnEndPositionByCell.y );
                        int samePositionCount = GameSpace.IsCellValidForCreate(newSpawnPositionByCell, GameSpace.BuildingList);
                        if (samePositionCount == 0)
                        {
                            spawnPositionList.Add(newSpawnPositionByCell);
                        }
                    }
                }
                return spawnPositionList;
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
        private List<IRealProduct> ShowAllIRealProductList (List<IRealProduct> heavySoldierList, List<IRealProduct> mediumSoldierList, List<IRealProduct> lightSoldierList,List<SpawnPosition> spawnPotionsList)
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
        #endregion
        
        #region Vulnerable Func.
        
        public void TakeDamage(IAttacker attacker)
        {
            Debug.Log(_currentHealth);
            _currentHealth = _currentHealth - attacker.Damage;
            Debug.Log(_currentHealth);
            CheckAmIDead();
        }

        private void  CheckAmIDead()
        {
            if(_currentHealth <= 0)
            {
               _heavySoldierBarracks.SetParent(_gameSpace); 
               _mediumSoldierBarracks.SetParent(_gameSpace);
               _lightSoldierBarracks.SetParent(_gameSpace);
               
               Destroy(this.gameObject);
                
            }
        }
        
        
        
        #endregion

        #region Move Func.

        private void MoveBarracks(Vector2Int MovedPositionByCell)
        {
            




        }

        #endregion

        private Vector3 CreatePointFixer(Vector3 spawnPoint ,Vector2Int gameObjectSizeByCell)
        {
            Vector2 floatVector2 = new Vector2(gameObjectSizeByCell.x, gameObjectSizeByCell.y);
            return new Vector3(spawnPoint.x + ((floatVector2.x / 2) * GameSpace.CellSize.x), spawnPoint.y + ((floatVector2.y / 2) * GameSpace.CellSize.y), spawnPoint.z);

        }


        public void Move(Vector3 Distance)
        {
            Vector3 mod10Distance = RoundingtoModCellSize(Distance);
            transform.position = mod10Distance + transform.position;
            
        }

        private Vector3 RoundingtoModCellSize(Vector3 distance)
        {
            float xValue = distance.x;
            float xremaining = xValue  % 10f ;
            xValue = xValue - xremaining;
            float yValue = distance.y;
            float yremaining = yValue % 10f;
            yValue = yValue - yremaining;
            return new Vector3(xValue, yValue, distance.z);


        }


        
    }
    
}
