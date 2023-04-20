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
        [SerializeField] private Vector3 _mouseWorldPositionOnUp;
        [SerializeField] private Vector3 _cameraAreaBorderStart;
        [SerializeField] private Vector3 _cameraAreaBorderEnd;

        private void Start()
        {
            CameraStartPosition();
            SetCameraBorder();
        }

        private void Update()
        {

            PanCamera();

        }


        private void SetCameraBorder()
        {
            Vector3 cameraViewSize = CalculateCameraViewSize(GetComponent<Camera>().fieldOfView,GetComponent<Camera>().aspect, _gameBoard.transform.position.z);
            _cameraAreaBorderStart = _gameSpace.GameSpaceStartArea + cameraViewSize/2;
            _cameraAreaBorderEnd =  _gameSpace.GameSpaceEndArea - cameraViewSize/2;




        }
        
        
        
        public Vector3 CalculateCameraViewSize(float fov, float aspectRatio, float distanceToPlane)
        {
            float height = 2.0f * distanceToPlane * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
            float width = height * aspectRatio;
            return new Vector3(width, height,0);
        }
        
        
        void CameraStartPosition()
        {
            
            transform.position = new Vector3(500, 500, 0);
            
        }
    
        void GetOnClickDownMousePosition()
        {
            _mouseWorldPositionOnDown = Input.mousePosition;
        
        }

        void GetOnClickUpMousePosition()
        {
            _mouseWorldPositionOnUp = Input.mousePosition;
        
        }

        Vector3 GetTwoPointDistance()
        {
            return _mouseWorldPositionOnDown - _mouseWorldPositionOnUp;
        
        }
    
    
        void PanCamera()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _gameBoard.IsMouseDownClickOnGameBoard)
            {
                GetOnClickDownMousePosition();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0) && _gameBoard.IsMouseUpClickOnGameBoard)
            {
                
                GetOnClickUpMousePosition();
                Vector3 newcameraPosition = transform.position + GetTwoPointDistance();
                if (IsAreaRange(newcameraPosition))
                {
                    transform.position = newcameraPosition;
                }
            }
        }
        
        bool IsAreaRange(Vector3 cameraposition)
        {
            if (_cameraAreaBorderStart.x < cameraposition.x && _cameraAreaBorderStart.y < cameraposition.y && _cameraAreaBorderEnd.x > cameraposition.x && _cameraAreaBorderEnd.y > cameraposition.y)
            { return true;}

            return false;

        }
        
        
    }
}