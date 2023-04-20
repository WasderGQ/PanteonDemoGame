using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  private void Start()
  {
    _gameSpaceEndArea = new Vector3(1000, 1000,100);
    _gameSpaceStartArea = new Vector3(0, 0,100);
  }
}
