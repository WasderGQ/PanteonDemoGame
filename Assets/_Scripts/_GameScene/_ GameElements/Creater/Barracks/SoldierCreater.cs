using System.Collections;
using System.Collections.Generic;
using _Scripts._GameScene.__GameElements.Creater;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using UnityEngine;

public abstract class SoldierCreater<T> :ICreater where T : Soldier
{
    public abstract List<T> CreatedSoldierList { get;}
    public abstract T FactoryMethod(Vector3 spawnPositionByPoint, Vector2Int startPositionByCell, Vector2Int productSizeByCell);
}
