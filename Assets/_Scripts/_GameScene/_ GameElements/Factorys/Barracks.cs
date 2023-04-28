
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
        [SerializeField] private GameSpace _gameSpace;

        #region Public Property

        #region Statics

        public static Vector2Int GameObjectSizeByCell { get { return _staticGameObjectSizeByCell; } }

        #endregion
        
        public Transform Transform { get => this.transform; }
        
        

        #endregion
        
        
       
        
        public List<IProduct> BuildingsProductList
        {
            get => ShowAllProduct();
        }
        
        
        private Vector2Int _startPositionByCell;
        private Vector2Int _endPositionByCell;
        private Vector2Int _spawnStartPositionByCell;
        private Vector2Int _spawnEndPositionByCell;
        
        private List<Vector2Int> _spawnPointList;
        
        public UnityEvent CreateHeavySoldier;
        public UnityEvent CreateMediumSoldier;
        public UnityEvent CreateLightSoldier;
       
        private readonly static Vector2Int _staticGameObjectSizeByCell = new Vector2Int(4, 4);

        private List<IProduct> _lightSoldier;

        private List<IProduct> _mediumSoldier;

        private List<IProduct> _heavySoldier;

        private Creater_LightSoldier _lightSoldierCreater;

        private Creater_MediumSoldier _mediumSoldierCreater;

        private Creater_HeavySoldier _heavySoldierCreater;


        public void InIt(Vector2Int startPositionByCell ,Vector2Int endPositionByCell,Vector2Int spawnStartPositionByCell,Vector2Int spawnEndPositionByCell)
        {
            AddListener();
            OnStartSetSpawnPoints(spawnStartPositionByCell,spawnEndPositionByCell);
            OnStartSetPositionsByCell(startPositionByCell,endPositionByCell);
        }

        private void OnStartSetPositionsByCell(Vector2Int startPositionByCell ,Vector2Int endPositionByCell)
        {
            _startPositionByCell = startPositionByCell;
            _endPositionByCell = endPositionByCell;
        }

        private void OnStartSetSpawnPoints(Vector2Int spawnStartPositionByCell,Vector2Int spawnEndPositionByCell)
        {
            _spawnStartPositionByCell = spawnStartPositionByCell;
            _spawnEndPositionByCell = spawnEndPositionByCell;
        }
        
        private void AddListener()
        {
            CreateHeavySoldier.AddListener(TriggerHeavySoldierCreater);
            CreateMediumSoldier.AddListener(TriggerMediumSoldierCreater);
            CreateLightSoldier.AddListener(TriggerLightSoldierCreater);
        }

        private void TriggerHeavySoldierCreater()
        {
           IProduct product  =_heavySoldierCreater.FactoryMethod();
           _heavySoldier.Add(product);

        }
        private void TriggerMediumSoldierCreater()
        {
            IProduct product =_heavySoldierCreater.FactoryMethod();
            _heavySoldier.Add(product);

        }
        private void TriggerLightSoldierCreater()
        {
            IProduct product =_heavySoldierCreater.FactoryMethod();
            _heavySoldier.Add(product);

        }

        private void ShowAllProduct(List<IProduct> heavySoldierList, List<IProduct> mediumSoldierList, List<IProduct> lightSoldierList)
        {
            List<IProduct> AllSoldierInBarracks = new List<IProduct>();
            foreach (var soldier in heavySoldierList)
            {
                
            }


        }
        
        
       /* using UnityEngine;

        public class ExampleClass : MonoBehaviour
        {
            private delegate int MyDelegate(int a, int b);
            private event MyDelegate myEvent;

            void Start()
            {
                myEvent += MyAdd;
                int result = myEvent(3, 5);
                Debug.Log("Event result: " + result); // "Event result: 8" yazdırır
            }

            private int MyAdd(int a, int b)
            {
                return a + b;
            }
        }*/

    }
}
