using _Scripts._GameScene._GameArea;
using _Scripts._GameScene._UI.Features;
using UnityEngine;

namespace _Scripts._GameScene.ManagersInGame
{
    public class CameraManager : MonoBehaviour
    {
        
        #region Private Variable
        
        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private RaycastHit2D _raycastHit;
        [SerializeField] private Vector3 _cameraAreaBorderStart;
        [SerializeField] private Vector3 _cameraAreaBorderEnd;
        [SerializeField] private CameraMovement _cameraMovement;
        
        #endregion
        
        #region UnityEvents

        private void Start()
        {
            ObjectInstantiation();
            CameraStartPosition();
            SetCameraBorder(transform.position);
           
        }
        private void ObjectInstantiation()
        {
            _cameraMovement = new CameraMovement();
            _cameraMovement.InIt();
        }
        private void Update()
        {
            Pan();
            Zoom();
        }
        
        #endregion
        
        #region Private func.

        private void CameraStartPosition()
        {
            
            transform.position = new Vector3(250, 250, 0);
            
        }
        
        
        #region Zoom Func.
        private async void Zoom()
        {
            Camera.main.fieldOfView = CheckZoomValue(await _cameraMovement.ZoomCamera(Camera.main));
            SetCameraBorder(transform.position);
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

        #endregion
        
        #region Pan Func.

        private async void Pan()
        {
            Vector3 newposition = await _cameraMovement.PanMovement() + transform.position;
            SetCameraBorder(newposition);
            transform.position = KeeperOfCameraInGameArea(newposition,CalculateCameraViewSize(GetComponent<Camera>().fieldOfView,GetComponent<Camera>().aspect, _gameSpace.transform.position.z));
        }
        private void SetCameraBorder(Vector3 Position)
        {
            Vector3 cameraViewSize = CalculateCameraViewSize(GetComponent<Camera>().fieldOfView,GetComponent<Camera>().aspect, _gameSpace.transform.position.z);
            _cameraAreaBorderStart = new Vector3(Position.x - (cameraViewSize.x/2), Position.y - (cameraViewSize.y/2),0) ;
            _cameraAreaBorderEnd =  new Vector3(Position.x + (cameraViewSize.x/2), Position.y + (cameraViewSize.y/2),0) ;
        }

        
        private Vector3 CalculateCameraViewSize(float fov, float aspectRatio, float distanceToPlane)
        {
            float windowsfov = fov / 1.4f; // Camera cant see all plane because of canvas
            float height =  2.0f * distanceToPlane * Mathf.Tan(windowsfov * 0.5f * Mathf.Deg2Rad);
            float width = height * aspectRatio;
            return new Vector3(width, height,0);
        }
        
        private Vector3 KeeperOfCameraInGameArea(Vector3 newcameraposition, Vector3 cameraViewSize)
        {
            if (_gameSpace.GameSpaceStartAreaByPoint.x > newcameraposition.x-(cameraViewSize.x/2))
            {
                newcameraposition.x = _gameSpace.GameSpaceStartAreaByPoint.x+(cameraViewSize.x/2);
                    
            }
            if (_gameSpace.GameSpaceStartAreaByPoint.y > newcameraposition.y-(cameraViewSize.y/2))
            { 
                newcameraposition.y  = _gameSpace.GameSpaceStartAreaByPoint.y+(cameraViewSize.y/2);
                    
            }
            if (_gameSpace.GameSpaceEndAreaByPoint.x < newcameraposition.x+(cameraViewSize.x/2))
            { 
                newcameraposition.x = _gameSpace.GameSpaceEndAreaByPoint.x-(cameraViewSize.x/2);
                    
            }
            if (_gameSpace.GameSpaceEndAreaByPoint.y < newcameraposition.y+(cameraViewSize.y/2))
            {
                newcameraposition.y = _gameSpace.GameSpaceEndAreaByPoint.y-(cameraViewSize.y/2);
                    
            }
            return newcameraposition;
            
        }

        #endregion
        
        #endregion
        
    }
}