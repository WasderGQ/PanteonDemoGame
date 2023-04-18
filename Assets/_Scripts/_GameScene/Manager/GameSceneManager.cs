using UnityEngine;
using WasderGQ.PanteonDemo.Generic;

public class GameSceneManager : Singleton<GameSceneManager>
{
   [SerializeField] private GameBoard _gameBoard;

   private void Start()
   {
      init();
   }

   private void init()
   {
      _gameBoard.InIt();


   }
}
