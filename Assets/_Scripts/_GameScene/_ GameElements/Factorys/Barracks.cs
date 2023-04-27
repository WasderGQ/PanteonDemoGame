
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene._Logic;
using Third_Party_Packages.Helpers.WasderGQ.CustomAttributes;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks>, IPortable, IGameObject
    {
        [SerializeField] private GameSpace _gameSpace;
       
        public static Vector2Int GameObjectSizeByCell
        {
            get { return _staticGameObjectSizeByCell; }
        }
        
        
        private readonly static  Vector2Int _staticGameObjectSizeByCell = new Vector2Int(4, 4);

        private List<IProduct> _lightSoldier;

        private List<IProduct> _mediumSoldier;

        private List<IProduct> _heavySoldier;

        private ICreater _lightSoldierCreater;

        private ICreater _mediumSoldierCreater;

        private ICreater _heavySoldierCreater;


        public void InIt(/*Vector2Int startPointByCell ,Vector2Int endPoint*/)
        {
            
            
            
            
        }
    }
}
