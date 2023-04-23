using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using _Scripts._GameScene._Logic;
using _Scripts._GameScene._UI;
using UnityEngine;
using UnityEngine.EventSystems;

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
        [SerializeField] private ClickRay _clickRay;
        [SerializeField] private bool isHoldingKey;
        #region UnityEvents

        private void Start()
        {
            ObjectInstantiation();
            CameraStartPosition();
            SetCameraBorder();
            
        }

        private void ObjectInstantiation()
        {
            _clickRay = new ClickRay();
            _mouseWorldPositionOnDown = new Vector3();
            _mouseWorldPositionOnHold = new Vector3();
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
        private async Task<Vector3> GetMousePosition()
        {
             return await _clickRay.GetRayWorldPosition();
            

        }
        
        private Vector3 GetTwoPointDistance()
        {
            return _mouseWorldPositionOnDown - _mouseWorldPositionOnHold;
        
        }
        private async void PanCamera()
        {
            
            
            if (Input.GetKeyDown(KeyCode.Mouse2) && _mouseWorldPositionOnDown == new Vector3())
            {
               // Debug.Log("key down cagrıldım.");
                _mouseWorldPositionOnDown = await GetMousePosition();
               // Debug.Log("press: " + _mouseWorldPositionOnDown);
                isHoldingKey = true;
            }
            
            if (isHoldingKey)
            {
               // Debug.Log(" key hold cagrıldım.");
                _mouseWorldPositionOnHold = await GetMousePosition();
               // Debug.Log("hold: "+ _mouseWorldPositionOnHold);
                Vector3 newcameraPosition = transform.position + GetTwoPointDistance();
               // Debug.Log("new camera position: " + newcameraPosition);
                transform.position = KeeperOfCameraInGameArea(newcameraPosition);
               // Debug.Log("setted new position: " +transform.position);
            }
            
            
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
               // Debug.Log(" key up cagrıldım.");
                isHoldingKey = false;
                _mouseWorldPositionOnDown = new Vector3();
                _mouseWorldPositionOnHold = new Vector3();
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