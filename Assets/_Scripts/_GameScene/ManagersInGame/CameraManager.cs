using _Scripts._GameScene._Logic;
using _Scripts._GameScene._UI;
using UnityEngine;

namespace _Scripts.Managers
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
            GameSpace.SaveMyPlace.Invoke(Vector2Int.zero, new Vector2Int(5,5));
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
            Vector3 newposition = await _cameraMovement.PanCamera() + transform.position;
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
            float windowsfov = fov / 1.5f; // Camera cant see all plane because of canvas
            float height =  2.0f * distanceToPlane * Mathf.Tan(windowsfov * 0.5f * Mathf.Deg2Rad);
            float width = height * aspectRatio;
            return new Vector3(width, height,0);
            
        }
        
        private Vector3 KeeperOfCameraInGameArea(Vector3 newcameraposition, Vector3 cameraViewSize)
        {
            if (_gameSpace.GameSpaceStartArea.x > newcameraposition.x-(cameraViewSize.x/2))
            {
                newcameraposition.x = _gameSpace.GameSpaceStartArea.x+(cameraViewSize.x/2);
                    
            }
            if (_gameSpace.GameSpaceStartArea.y > newcameraposition.y-(cameraViewSize.y/2))
            { 
                newcameraposition.y  = _gameSpace.GameSpaceStartArea.y+(cameraViewSize.y/2);
                    
            }
            if (_gameSpace.GameSpaceEndArea.x < newcameraposition.x+(cameraViewSize.x/2))
            { 
                newcameraposition.x = _gameSpace.GameSpaceEndArea.x-(cameraViewSize.x/2);
                    
            }
            if (_gameSpace.GameSpaceEndArea.y < newcameraposition.y+(cameraViewSize.y/2))
            {
                newcameraposition.y = _gameSpace.GameSpaceEndArea.y-(cameraViewSize.y/2);
                    
            }
            return newcameraposition;
            
        }

        #endregion
        
        #endregion
        
    }
}