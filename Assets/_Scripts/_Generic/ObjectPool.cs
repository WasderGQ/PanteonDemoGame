using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts._Generic
{
public class GObjectPool<T> : GObjectPool where T : MonoBehaviour
{
    public static GObjectPool<T> SharedInstance;
    public List<T> pooledObjects;
    public T objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<T>();
        T tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public T GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}

public class GObjectPool : MonoBehaviour
{
    
}


}
