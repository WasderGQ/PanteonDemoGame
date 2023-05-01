using System.Threading.Tasks;
using _Scripts._GameScene._GameArea;
using UnityEngine;

namespace _Scripts._GameScene._UI
{
    public class GameUICanvas : MonoBehaviour
    {


        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private Information _information;
        [SerializeField] private ProductMenu _productMenu;


        public void InIt()
        {
            
        _gameSpace.InIt();
        _information.InIt(); 
        _productMenu.InIt();
        
        }
    }
}