using System.Runtime.InteropServices.WindowsRuntime;
using _Scripts._GameScene._Logic;
using UnityEngine;

namespace _Scripts._GameScene.Manager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private RaycastHit2D _raycastHit;
        [SerializeField] private Vector3 _mouseWorldPositionOnDown;
        [SerializeField] private Vector3 _mouseWorldPositionOnHold;
        [SerializeField] private Vector3 _cameraAreaBorderStart;
        [SerializeField] private Vector3 _cameraAreaBorderEnd;

      
        #region UnityEvents

        private void Start()
        {
            CameraStartPosition();
            SetCameraBorder();
        }
        private void Update()
        {

            PanCamera();

        }

        #endregion

        
        #region Private func.
        private void SetCameraBorder()
        {
            Vector3 cameraViewSize = CalculateCameraViewSize(GetComponent<Camera>().fieldOfView,GetComponent<Camera>().aspect, _gameBoard.transform.position.z);
            _cameraAreaBorderStart = _gameSpace.GameSpaceStartArea + cameraViewSize/2;
            _cameraAreaBorderEnd =  _gameSpace.GameSpaceEndArea - cameraViewSize/2;
            Debug.Log($"start{_cameraAreaBorderStart}");
            Debug.Log($"end{_cameraAreaBorderEnd}");
            Debug.Log($"startgameboard{_gameSpace.GameSpaceStartArea}");
            Debug.Log($"endgameboard{_gameSpace.GameSpaceEndArea}");

        }
        private Vector3 CalculateCameraViewSize(float fov, float aspectRatio, float distanceToPlane)
        {
            float height = 2.0f * distanceToPlane * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
            float width = height * aspectRatio;
            return new Vector3(width, height,0);
        }
        private void CameraStartPosition()
        {
            
            transform.position = new Vector3(500, 500, 0);
            
        }
        private void GetMousePosition(Vector3 positionsavevalue)
        {
            positionsavevalue = Input.mousePosition;
        
        }
        
        private Vector3 GetTwoPointDistance()
        {
            return _mouseWorldPositionOnDown - _mouseWorldPositionOnHold;
        
        }
        private void PanCamera()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _gameBoard.IsMouseDownClickOnGameBoard)
            {
                GetMousePosition(_mouseWorldPositionOnDown);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && _gameBoard.IsMouseHoldClickOnGameBoard)
            {
                
                GetMousePosition(_mouseWorldPositionOnHold);
                Vector3 newcameraPosition = transform.position + GetTwoPointDistance();
                transform.position = KeeperOfCameraInGameArea(newcameraPosition);

            }
        }
        private Vector3 KeeperOfCameraInGameArea(Vector3 cameraposition)
        {
            
            
           
                if (_cameraAreaBorderStart.x > cameraposition.x )
                {
                    cameraposition.x = _cameraAreaBorderStart.x;
                    
                }
                if (_cameraAreaBorderStart.y > cameraposition.y)
                { 
                    cameraposition.y  = _cameraAreaBorderStart.y;
                    
                }
                if (_cameraAreaBorderEnd.x < cameraposition.x)
                { 
                    cameraposition.x = _cameraAreaBorderEnd.x;
                    
                }
                if (_cameraAreaBorderEnd.y < cameraposition.y)
                {
                    cameraposition.y = _cameraAreaBorderEnd.y;
                    
                }
            
            return cameraposition;
            
        }
        
        #endregion
        
        
        
        
        
        
    }
}