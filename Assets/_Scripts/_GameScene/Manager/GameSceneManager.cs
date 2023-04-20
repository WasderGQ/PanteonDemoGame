using _Scripts._GameScene._Logic;
using _Scripts._Generic;
using UnityEngine;

namespace _Scripts._GameScene.Manager
{
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
}
