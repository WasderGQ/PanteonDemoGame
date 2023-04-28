using System;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene.GameObjectPools;
using UnityEngine;

namespace _Scripts._GameScene.__GameElements.Creater.BarackCreaters
{
    public class Creater_HeavySoldier : SoldierCreater
    {
        private GameSpace _gameSpace;
        public override IProduct FactoryMethod()
        {
          /*  HeavySoldier heavySoldier = HeavySoldierPool.SharedInstance.GetPooledObject();
            if (heavySoldier != null)
            {
                heavySoldier.InIt();
                heavySoldier.transform.position = new Vector3();
                heavySoldier.transform.rotation = Quaternion.identity;
                heavySoldier.gameObject.SetActive(true);
                heavySoldier.transform.SetParent(_gameSpace.);
                GameSpace.SaveMyPlace.Invoke();
                SaveFilledCells(spawnCellPosition, new Vector2Int(spawnCellPosition.x + Barracks.GameObjectSizeByCell.x, spawnCellPosition.y + Barracks.GameObjectSizeByCell.y));
            }
            */
         return new HeavySoldier();
        }
    }
}
