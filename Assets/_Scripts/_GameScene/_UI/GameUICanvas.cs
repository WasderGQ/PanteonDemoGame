using System.Threading.Tasks;
using _Scripts._GameScene._Logic;
using UnityEngine;

namespace _Scripts._GameScene._UI
{
    public class GameUICanvas : MonoBehaviour
    {


        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private StaticText _staticText;
        [SerializeField] private Information _information;
        [SerializeField] private ProductMenu _productMenu;


        public void InIt()
        {
        _gameSpace.InIt();
        _staticText.InIt(); 
        _information.InIt(); 
        _productMenu.InIt(); 
            
            
        }


    }
}