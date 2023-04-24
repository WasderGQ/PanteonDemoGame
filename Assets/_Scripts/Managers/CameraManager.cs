using _Scripts._GameScene._Logic;
using _Scripts._GameScene._UI;
using UnityEngine;

namespace _Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        
        #region Private Variable
        
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private RaycastHit2D _raycastHit;
        [SerializeField] private Vector3 _cameraAreaBorderStart;
        [SerializeField] private Vector3 _cameraAreaBorderEnd;
        [SerializeField] private Pan _pan;
        
        #endregion
        
        #region UnityEvents

        private void Start()
        {
            ObjectInstantiation();
            CameraStartPosition();
            SetCameraBorder();
            
        }
        private void ObjectInstantiation()
        {
            _pan = new Pan();
            _pan.InIt();
        }
        private void Update()
        {
            UsePanFunction();
            Zoom();
        }
        
        #endregion
        
        #region Private func.


        private void Zoom()
        {
           float currentScrollWheelAxis = Input.GetAxis("Mouse ScrollWheel");
            if (currentScrollWheelAxis != 0f)
            {
               float tempAxis = Camera.main.fieldOfView - currentScrollWheelAxis;
               Camera.main.fieldOfView = CheckZoomValue(tempAxis);
               SetCameraBorder();
            }
        }

        private float CheckZoomValue(float fov)
        {
            if(fov < 60)
            {
                return 60;
            }
            if(120 < fov)
            {
                return 120;
            }

            return fov;
        }
        
        private async void UsePanFunction()
        {
          transform.position = KeeperOfCameraInGameArea(await _pan.PanCamera() + transform.position);
          
        }
        
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
        private Vector3 KeeperOfCameraInGameArea(Vector3 newcameraposition)
        {
            if (_cameraAreaBorderStart.x > newcameraposition.x )
            {
                newcameraposition.x = _cameraAreaBorderStart.x;
                    
            }
            if (_cameraAreaBorderStart.y > newcameraposition.y)
            { 
                newcameraposition.y  = _cameraAreaBorderStart.y;
                    
            }
            if (_cameraAreaBorderEnd.x < newcameraposition.x)
            { 
                newcameraposition.x = _cameraAreaBorderEnd.x;
                    
            }
            if (_cameraAreaBorderEnd.y < newcameraposition.y)
            {
                newcameraposition.y = _cameraAreaBorderEnd.y;
                    
            }
            return newcameraposition;
            
        }
        
        #endregion
        
    }
}