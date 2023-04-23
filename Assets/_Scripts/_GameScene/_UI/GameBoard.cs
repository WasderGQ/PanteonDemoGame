using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts._GameScene._UI
{
    public class GameBoard : MonoBehaviour
    {
        
        [SerializeField] private bool _isMouseDownClickOnGameBoard;
        [SerializeField] private bool _isMouseUpClickOnGameBoard;
        [SerializeField] private Grid _grid;
        [SerializeField] private bool _isMouseHoldClickOnGameBoard;
        
        
        public bool IsMouseDownClickOnGameBoard
        {
            get => _isMouseDownClickOnGameBoard;
        }
        public bool IsMouseUpClickOnGameBoard
        {
            get => _isMouseUpClickOnGameBoard;
        }
        public bool IsMouseHoldClickOnGameBoard
        {
            get => _isMouseHoldClickOnGameBoard;
        }
        
        
        public void InIt()
        {
           
            

        }


        public async void OnMouseDown()
        {
            _isMouseDownClickOnGameBoard = true;
            await Task.Delay(200);
            _isMouseDownClickOnGameBoard = false;
            _isMouseHoldClickOnGameBoard = true;
        }

        public async void OnMouseUp()
        {
            _isMouseHoldClickOnGameBoard = false;
            _isMouseUpClickOnGameBoard = true;
            await Task.Delay(200);
            _isMouseUpClickOnGameBoard = false;
        }

        /*private void GenerateGrid(int width , int height)
        {
            var ranges = GetPositionRange(_tile);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var spawnTitles = Instantiate(_tile, new Vector3(x,y), Quaternion.identity,this.transform);
                    spawnTitles.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
                    spawnTitles.GetComponentInChildren<Tile>().GetMyCoordinate(new Vector2(x,y));
                    RectTransform temp = spawnTitles.GetComponent<RectTransform>();
                    temp.anchoredPosition = new Vector2(x * ranges.x, y * ranges.y);
                    spawnTitles.name = $"Title {x},{y}";
                    
               
                }
            }
        }

        private Vector2 GetPositionRange(Tile positionObject)
        {
            Vector2 ranges = Vector2.zero;
            ranges.x = Convert.ToInt32(positionObject.GetComponent<RectTransform>().sizeDelta.x) * positionObject.GetComponent<RectTransform>().localScale.x;
            ranges.y = Convert.ToInt32(positionObject.GetComponent<RectTransform>().sizeDelta.y) * positionObject.GetComponent<RectTransform>().localScale.y;
            return ranges;

        }
    */
    
    
    }
}
