
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Factorys
{
    public class Barracks : FactoryHave3Creater<Barracks> , IMovable
    {
        
            
        private List<IProduct> _lightSoldier;
        
        private List<IProduct> _mediumSoldier;
        
        private List<IProduct> _heavySoldier;

        private ICreater _lightSoldierCreater;

        private ICreater _mediumSoldierCreater;

        private ICreater _heavySoldierCreater;
        
        
        
        private Vector3 _myStartWorldPosition;
        
        private Vector3 _myEndWorldPosition;

        private Vector2 _mySizeOccupiedInSpace;
        public Vector3 MyStartWorldPosition { get => _myStartWorldPosition; }
        public Vector3 MyEndWorldPosition { get => _myEndWorldPosition; }
        public Vector3 MySizeOccupiedInSpace { get => _myEndWorldPosition; }
        
        
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
            _myStartWorldPosition = OffsetCreatePosition(transform.position);

        }
        
        private void GetMyEndWorldPosition()
        {
            Vector2 offsetSize = _mySizeOccupiedInSpace / 2;
            _myEndWorldPosition = new Vector3(transform.position.x - offsetSize.x, transform.position.y - offsetSize.y, transform.position.z);

        }

        #endregion
        
        
        
        public Vector3 OffsetCreatePosition(Vector3 CreatePosition)
        {
            Vector2 offsetSize = _mySizeOccupiedInSpace / 2;
            return new Vector3(CreatePosition.x - offsetSize.x, CreatePosition.y - offsetSize.y, CreatePosition.z);
        }
        
        
        
    }
}
