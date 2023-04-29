using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BuildingCreaters
{
    public class PowerPlantCreater : BuildingCreater
    {
        public override IProduct FactoryCreate(Vector3 spawnPositionByPoint,Vector2Int startPositionByCell)
        {
            return new PowerPlant();
        }

    }
}
