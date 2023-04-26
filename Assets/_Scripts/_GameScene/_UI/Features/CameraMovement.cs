using System.Threading.Tasks;
using _Scripts._GameScene._UI;
using _Scripts._GameScene._UI.Features;
using UnityEngine;

public class CameraMovement 
{
    private Vector3 _mouseWorldPositionOnDown;
    private Vector3 _mouseWorldPositionOnHold;
    private ClickRay _clickRay;
    private bool _isHoldingKey;
    private Vector3 _panDistance = new Vector3();

    public void InIt()
    {
        _clickRay = new ClickRay();
        _mouseWorldPositionOnDown = new Vector3();
        _mouseWorldPositionOnHold = new Vector3();
        
    }
    
    public async Task<Vector3> PanCamera()
    {
        
            if (Input.GetKeyDown(KeyCode.Mouse2) && _mouseWorldPositionOnDown == new Vector3() && await CheckMouseInTruePosition())
            {
                // Debug.Log("key down summoned.");
                _mouseWorldPositionOnDown = await GetMousePosition();
                // Debug.Log("press: " + _mouseWorldPositionOnDown);
                _isHoldingKey = true;
            }
            
            if (_isHoldingKey)
            {
                // Debug.Log(" key hold summoned.");
                _mouseWorldPositionOnHold = await GetMousePosition();
                // Debug.Log("hold: "+ _mouseWorldPositionOnHold);
                _panDistance = GetTwoPointDistance();
                // Debug.Log("new camera position: " + newcameraPosition);
            
            }
            
            
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                // Debug.Log(" key up summoned.");
                _isHoldingKey = false;
                _mouseWorldPositionOnDown = new Vector3();
                _mouseWorldPositionOnHold = new Vector3();
                _panDistance = Vector3.zero;
            }
            return _panDistance;
        

        return _panDistance = Vector3.zero;


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
        if (await CheckMouseInTruePosition())
        {
            float currentScrollWheelAxis = Input.GetAxis("Mouse ScrollWheel");
            if (currentScrollWheelAxis != 0f)
            {
                return camera.fieldOfView - currentScrollWheelAxis;
            }
            
        }
           return camera.fieldOfView;
    }
        
    private async Task<bool> CheckMouseInTruePosition() //In my game this must be gameboard.
    {
        var result = await _clickRay.TakeSpecificHit("GameBoard");
        if(result.Item2)
        {
            return true;
        }

        return false;

    }
        
        
}
