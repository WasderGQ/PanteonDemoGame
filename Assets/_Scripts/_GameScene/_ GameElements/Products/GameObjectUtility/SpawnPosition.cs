using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Products.GameObjectUtility
{
    public class SpawnPosition : IRealProduct
    {
        private List<IRealProduct> _spawnPotionList;
        
        private Vector2Int _spawnPositionByCell;
        public Transform MyTransform { get; }
        
        public Vector2Int StartPositionByCell { get => _spawnPositionByCell; }
        public Vector2Int EndPositionByCell { get => _spawnPositionByCell; }
        
        public List<IRealProduct> ProductList { get => _spawnPotionList; }
        
        public SpawnPosition(Vector2Int spawnPositionByCell)
        {
            _spawnPositionByCell = spawnPositionByCell;
            _spawnPotionList = new List<IRealProduct>();
        }


        
       
    }
}