using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts._GameScene._UI.Features
{
    public class CameraMovement 
    {
        private Vector3 _mouseWorldPositionOnDown;
        private Vector3 _mouseWorldPositionOnHold;
        private ClickRay _clickRay;
        private bool _isHoldingKey;
        private Vector3 _panDistance;

        public void InIt()
        {
            _clickRay = new ClickRay();
            _mouseWorldPositionOnDown = new Vector3();
            _mouseWorldPositionOnHold = new Vector3();
        
        }
    
        public async Task<Vector3> PanMovement()
        {
            
            if (Input.GetKeyDown(KeyCode.Mouse2) && _mouseWorldPositionOnDown == new Vector3())
            {
                if(await _clickRay.CheckMouseOnCollider("GameBoard"))
                
                    _mouseWorldPositionOnDown = await GetMousePosition();
                    _isHoldingKey = true;
            }
            
            if (_isHoldingKey)
            {
                _mouseWorldPositionOnHold = await GetMousePosition();
                _panDistance = GetTwoPointDistance();

            }
            
            
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                _isHoldingKey = false;
                _mouseWorldPositionOnDown = new Vector3();
                _mouseWorldPositionOnHold = new Vector3();
                _panDistance = Vector3.zero;
            }
            return _panDistance;
            
        }


        private async Task<Vector3> GetMousePosition()
        {
            return await _clickRay.GetRayWorldPosition();


        }

        private Vector3 GetTwoPointDistance()
        {
            return _mouseWorldPositionOnDown - _mouseWorldPositionOnHold;

        }


        public async Task<float> ZoomCamera(Camera camera)
        {
            if (await _clickRay.CheckMouseOnCollider("GameBoard"))
            {
                float currentScrollWheelAxis = Input.GetAxis("Mouse ScrollWheel");
                if (currentScrollWheelAxis != 0f)
                {
                    return camera.fieldOfView - currentScrollWheelAxis;
                }
            
            }
            return camera.fieldOfView;
        }
        
    
    
        
        
        
    }
}
