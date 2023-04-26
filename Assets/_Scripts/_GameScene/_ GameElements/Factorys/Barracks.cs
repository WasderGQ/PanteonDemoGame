
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks>, IPortable, IGameObject
    {
        private readonly string _uniqueID = Guid.NewGuid().ToString("N");
        
        public string ID
        {
            get { return _uniqueID; }
        }

        public Vector3Int CellSize
        {
            get { return StaticCellSize; }
        }
        
        
        public static readonly Vector3Int StaticCellSize = new Vector3Int(4, 4);

        private List<IProduct> _lightSoldier;

        private List<IProduct> _mediumSoldier;

        private List<IProduct> _heavySoldier;

        private ICreater _lightSoldierCreater;

        private ICreater _mediumSoldierCreater;

        private ICreater _heavySoldierCreater;



        
    

        [SerializeField]private Vector3 _myStartWorldPosition;
        
        [SerializeField]private Vector3 _myEndWorldPosition;

        [SerializeField]private Vector2 _mySizeOccupiedInSpace;
        public Vector3 MyStartWorldPosition { get => _myStartWorldPosition; }
        public Vector3 MyEndWorldPosition { get => _myEndWorldPosition; }
        public Vector3 MySizeOccupiedInSpace { get => _myEndWorldPosition; }

        
        
        
        
        public Vector3 PositionChanger
        {//use when you move object to another cell
            get => transform.position;
            set
            {
                transform.position = OffsetSpawnPositionBySize(value);
                OffsetSpawnPositionBySize(value);
                GetMyStartWorldPosition();
                GetMyEndWorldPosition();
            }
        }

        

        public void InIt()
        {
            GetMyStartWorldPosition();
            GetMyEndWorldPosition();
            GetMyWorldSize();
            
            
        }

        
        #region Start Func.

        private void GetMyWorldSize()
        {
            _mySizeOccupiedInSpace = GetComponent<BoxCollider2D>().size;
        }
        
        private void GetMyStartWorldPosition()
        {
            Vector2 offsetSize = _mySizeOccupiedInSpace / 2;
            _myEndWorldPosition = new Vector3(transform.position.x - offsetSize.x, transform.position.y - offsetSize.y, transform.position.z);

        }
        
        private void GetMyEndWorldPosition()
        {
            Vector2 offsetSize = _mySizeOccupiedInSpace / 2;
            _myEndWorldPosition = new Vector3(transform.position.x + offsetSize.x, transform.position.y + offsetSize.y, transform.position.z);

        }

        #endregion
        
        
        

        private Vector3 OffsetSpawnPositionBySize(Vector3 value)
        {
            
            return new Vector3(value.x - MySizeOccupiedInSpace.x/2, value.y - MySizeOccupiedInSpace.y/2, value.z);
            
            
            
        }
        
    }
}
