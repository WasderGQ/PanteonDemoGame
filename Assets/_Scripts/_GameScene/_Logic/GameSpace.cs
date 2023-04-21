using System;
using UnityEngine;

namespace _Scripts._GameScene._Logic
{
  public class GameSpace : MonoBehaviour
  {
    [SerializeField]private Vector3 _gameSpaceStartArea;
    [SerializeField]private Vector3 _gameSpaceEndArea;
  
    public Vector3 GameSpaceStartArea
    {
      get => _gameSpaceStartArea;
    }
    public Vector3 GameSpaceEndArea
    {
      get => _gameSpaceEndArea;
    }

    public void InIt()
    {
      SetGameSpaceAreaCoordinate();
    }

    private void SetGameSpaceAreaCoordinate()
    {
      _gameSpaceEndArea = new Vector3(1000, 1000,100);
      _gameSpaceStartArea = new Vector3(0, 0,100);
    
    }

    private Tuple<GameObject,Vector3> ThrowRayCatchGameObjectAndPosition()
    {
      
      if (Input.GetButtonDown("Fire1"))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
          
          Debug.Log("Ray hit object: " + hit.collider.gameObject.name);
          return new Tuple<GameObject, Vector3>(hit.collider.gameObject,hit.collider.gameObject.transform.position);
        }

        
      }
      return new Tuple<GameObject, Vector3>(null,Vector3.zero);;
    }

    
    


  }
}
