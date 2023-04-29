using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Features;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements
{
    public class SpawnPosition : MonoBehaviour ,IGameSpaceOccupanter
    {
        private List<IGameSpaceOccupanter> _occupanters;
        
        private Vector2Int _spawnPositionByCell;
        
        public Vector2Int StartPositionByCell { get => _spawnPositionByCell; }
        public Vector2Int EndPositionByCell { get => _spawnPositionByCell; }
        
        public List<IGameSpaceOccupanter> Occupanters
        {
            get => _occupanters;
        }
        
        public SpawnPosition(Vector2Int spawnPositionByCell)
        {
            _spawnPositionByCell = spawnPositionByCell;
            
            List<IGameSpaceOccupanter> _occupanters = new List<IGameSpaceOccupanter>();
        }


        
       
    }
}