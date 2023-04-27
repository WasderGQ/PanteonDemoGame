using _Scripts._GameScene._Logic;
using _Scripts._GameScene._UI;
using _Scripts._Generic;
using UnityEngine;

namespace _Scripts._GameScene.ManagersInGame
{
   public class GameSceneManager : Singleton<GameSceneManager>
   {
      [SerializeField] private GameUICanvas _gameUICanvas;
      [SerializeField] private GameSpace _gameSpace;
      [SerializeField] private MouseManager _mouseManager;
      private void Start()
      {
         InIt();
      }

      private void InIt()
      {
         _mouseManager.InIt();
         _gameSpace.InIt();
         _gameUICanvas.InIt();
         
      }
   }
}
