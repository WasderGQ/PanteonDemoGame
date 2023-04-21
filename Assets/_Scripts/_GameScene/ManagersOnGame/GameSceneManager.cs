using _Scripts._GameScene._Logic;
using _Scripts._GameScene._UI;
using _Scripts._Generic;
using UnityEngine;

namespace _Scripts._GameScene.Manager
{
   public class GameSceneManager : Singleton<GameSceneManager>
   {
      [SerializeField] private GameBoard _gameBoard;
      [SerializeField] private GameSpace _gameSpace;
      private void Start()
      {
         init();
      }

      private void init()
      {
         _gameSpace.InIt();
         _gameBoard.InIt();


      }
   }
}
