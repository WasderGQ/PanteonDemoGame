using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts._GameScene._UI.Features
{
    public class ClickRay
    {

        private async Task<RaycastHit[]> ThrowRayTryCatchGameColliders()
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
                Debug.Log("Rail no crash anything");

            }

            hittedColliders = Physics.RaycastAll(ray);
            return hittedColliders;

        }

        public async Task<Vector3> GetRayWorldPosition()
        {
            RaycastHit[] hittedRayLayers = await ThrowRayTryCatchGameColliders();
            RaycastHit hittedlayer = new RaycastHit();
            bool IsGetPosition;
            try
            {
                foreach (var hittedLayer in hittedRayLayers)
                {
                    if (hittedLayer.collider.gameObject.tag == "GameBoard")
                    {
                        IsGetPosition = true;
                    }

                }
            }
            catch
            {
                Debug.LogWarning("Mouse isn't on GameBoard");
            }

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

    }
}
  