using _Scripts._GameScene._GameArea;
using _Scripts._GameScene._UI;
using _Scripts._Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts._GameScene.ManagersInGame
{
   public class GameSceneManager : Singleton<GameSceneManager>
   {
      [SerializeField] private GameUICanvas _gameUICanvas;
      [SerializeField] private GameSpace _gameSpace;
      [FormerlySerializedAs("_mouseManager")] [SerializeField] private MouseController mouseController;
      private void Start()
      {
         InIt();
      }

      private void InIt()
      {
         mouseController.InIt();
         _gameSpace.InIt();
         _gameUICanvas.InIt();
         
      }
   }
}
