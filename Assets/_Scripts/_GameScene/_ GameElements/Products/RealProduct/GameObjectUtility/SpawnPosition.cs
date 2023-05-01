using System.Collections.Generic;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.RealProduct.GameObjectUtility
{
    public class SpawnPosition : MonoBehaviour , IRealProduct
    {
        public bool IsUsable
        {
            get => _isUsable;
        }

        [SerializeField] private bool _isUsable;
        
        private List<IRealProduct> _spawnPotionList;
        private Vector2Int _spawnPositionByCell;
        

        public Vector2Int StartPositionByCell { get => _spawnPositionByCell; }
        public Vector2Int EndPositionByCell { get => _spawnPositionByCell; }

        public List<IRealProduct> ProductList { get => _spawnPotionList; }

        public void InIt(Vector2Int spawnPositionByCell)
        {
            _spawnPositionByCell = spawnPositionByCell;
            _isUsable = true;
            
        }
        
        public void ChangeUsableStat(bool stat)
        {
            _isUsable = stat;
        }
        
        

        
       
    }
}