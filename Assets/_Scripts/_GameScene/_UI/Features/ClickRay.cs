using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts._GameScene._UI.Features
{
    public class ClickRay
    {

        public async Task<RaycastHit[]> ThrowRayTryCatchRaycastHits()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool result;
            RaycastHit[] hittedColliders;
            try
            {
                result = Physics.Raycast(ray);
                Assert.IsTrue(result);
            }
            catch
            { 
                //Debug.LogWarning("Rail no crash anything");

            }

            hittedColliders = Physics.RaycastAll(ray);
            return hittedColliders;
        }

        public async Task<Vector3> GetRayWorldPosition()
        {
            RaycastHit[] hittedRayLayers = await ThrowRayTryCatchRaycastHits();
            RaycastHit hittedlayer = new RaycastHit();
            

            if (true)
            {
                try
                {
                    foreach (var hittedLayer in hittedRayLayers)
                    {
                        if (hittedLayer.collider.gameObject.tag == "GameSpace")
                        {
                            hittedlayer = hittedLayer;
                        }
                    }
                }
                catch
                {
                    Debug.LogWarning("GameSpace cant found");
                }
                
            }
            return hittedlayer.point;
        }

        public async Task<(RaycastHit, bool)> TakeSpecificRaycastHitWithTaskBool(string gameObjectTag)
        {
            RaycastHit[] hittedRayLayers = await ThrowRayTryCatchRaycastHits();
            
                foreach (var hittedLayer in hittedRayLayers)
                {
                    if (hittedLayer.collider.gameObject.tag == gameObjectTag)
                    {
                        return (hittedLayer, true);
                    }
                }
                return (new RaycastHit(), false);
        }

        public async Task<(RaycastHit, bool)> TakeSpecificRaycastHitWithTaskBoolOnGivenList(string gameObjectTag,RaycastHit[] raycastHits)
        {
            foreach (var hittedLayer in raycastHits)
                {
                    if (hittedLayer.collider.gameObject.tag == gameObjectTag)
                    {
                        return (hittedLayer, true);
                    }
                }
                return (new RaycastHit(), false);
        }
        
        public RaycastHit TakeSpecificRaycastHit(string tag, RaycastHit[] raycastHitList)
        {
            try
            {
                foreach (var hittedLayer in raycastHitList)
                {
                    if (hittedLayer.collider.gameObject.tag == tag)
                    {
                        return (hittedLayer);
                    }
                }
            }
            catch
            {
                Debug.LogWarning("Raycast not found for the given tag name");
            }

            return (new RaycastHit());
            
        }

        public bool IsThereSpecificRaycastHit(string tag, RaycastHit[] raycastHitList)
        {
            try
            {
                foreach (var hittedLayer in raycastHitList)
                {
                    if (hittedLayer.collider.gameObject.tag == tag)
                    {
                        return (true);
                    }
                }
            }
            catch
            {
                Debug.LogWarning("There is nothing with the given tag name.");
            }

            return (false);
            
            
            
        }
        
        public async Task<bool> CheckMouseOnCollider(string gameObjectTag) //In my game this must be gameboard.
        {
            var result = await TakeSpecificRaycastHitWithTaskBool(gameObjectTag);
            if(result.Item2)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> IsOnlyGameBoardAndGameWindows()
        {
            int counter=0;
            RaycastHit[] raycastHits = await ThrowRayTryCatchRaycastHits();

            foreach (var hit in raycastHits)
            {
                if (hit.collider.gameObject.tag != "Canvas" || hit.collider.gameObject.tag != "GameBoard" || hit.collider.gameObject.tag != "GameSpace")
                {
                    counter++;
                }

                if (counter == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
    
}
  