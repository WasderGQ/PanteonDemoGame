using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts._Generic
{
public class GObjectPool<T> : GObjectPool where T : MonoBehaviour
{
    private static GObjectPool<T> _sharedInstance;
    [SerializeField]private List<T> _pooledobjects;
    [SerializeField]private T _objectToPool;
    [SerializeField]private int _amountToPool;
    [SerializeField]protected Transform _parentPoolObject;
    public List<T> PooledObjects
    {
        get => _pooledobjects;
    }
    public static GObjectPool<T> SharedInstance
    {
        get => _sharedInstance;
    }
    void Awake()
    {
        _sharedInstance = this;
    }

    void Start()
    {
        _pooledobjects = new List<T>();
        FilledList();
    }
    public T GetPooledObject()
    {
        for(int i = 0; i < _amountToPool; i++)
        {
            if(!_pooledobjects[i].gameObject.activeInHierarchy)
            {
                return _pooledobjects[i];
            }
        }
        return null;
    }

    public void FilledList()
    {
        T tmp;
        for (int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_objectToPool);
            tmp.transform.SetParent(_parentPoolObject);
            tmp.gameObject.SetActive(false);
            _pooledobjects.Add(tmp);
        }
    }

}

public class GObjectPool : MonoBehaviour
{
    
    
}


}
