using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene._UI.Features;
using UnityEngine;

namespace _Scripts._GameScene.ManagersInGame
{
    public class MouseManager : MonoBehaviour
    {
        private ClickRay _clickRay;
        private GameObject _selectedGameObject;

    
        public void InIt()
        {
            SetVariable();
        }
        private void SetVariable()
        {
            _clickRay = new ClickRay();
        }
        private void Update()
        {
            Mouse0Click();
        }
        private async void Mouse0Click()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit[] _raycastHitList = await _clickRay.ThrowRayTryCatchGameColliders();
            
                if (await _clickRay.IsThereSpecificRaycastHit("GameBoard",_raycastHitList))
                {
                    if (await _clickRay.IsThereSpecificRaycastHit("Barracks", _raycastHitList))
                    {
                        RaycastHit _raycastHit = await _clickRay.TakeSpecificRaycastHit("Barracks",_raycastHitList);
                        SelectBarracks(_raycastHit);
                    }
                    if (await _clickRay.IsThereSpecificRaycastHit("PowerPlant", _raycastHitList))
                    {
                        RaycastHit _raycastHit = await _clickRay.TakeSpecificRaycastHit("PowerPlant", _raycastHitList);
                        SelectPowerPlant(_raycastHit);
                    }
                    if (await _clickRay.IsThereSpecificRaycastHit("Soldier", _raycastHitList))
                    {
                        RaycastHit _raycastHit = await _clickRay.TakeSpecificRaycastHit("Soldier", _raycastHitList);
                        SelectSoldier(_raycastHit);
                    }
                    if (await _clickRay.IsThereSpecificRaycastHit("HeavySoldierButton", _raycastHitList))
                    {
                        RaycastHit _raycastHit = await _clickRay.TakeSpecificRaycastHit("HeavySoldierButton", _raycastHitList);
                        CreateHeavySoldierButton(_raycastHit,_raycastHit.transform.gameObject.GetComponent<Barracks>());
                    }
                    if (await _clickRay.IsThereSpecificRaycastHit("MediumSoldierButton", _raycastHitList))
                    {
                        RaycastHit _raycastHit = await _clickRay.TakeSpecificRaycastHit("MediumSoldierButton", _raycastHitList);
                        CreateMediumSoldierButton(_raycastHit,_raycastHit.transform.gameObject.GetComponent<Barracks>());
                    }
                    if (await _clickRay.IsThereSpecificRaycastHit("LightSoldierButton", _raycastHitList))
                    {
                        RaycastHit _raycastHit = await _clickRay.TakeSpecificRaycastHit("LightSoldierButton", _raycastHitList);
                        CreateLightSoldierButton(_raycastHit,_raycastHit.transform.gameObject.GetComponent<Barracks>());
                    }
                }
            }
        }



        private void CreateHeavySoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
        
        
        
        }
        private void CreateMediumSoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
        
        
        
        }
        private void CreateLightSoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {

            ClickAnimation(raycastHit);


        }

        private async void ClickAnimation(RaycastHit raycastHit)
        {
            raycastHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.grey;
            await Task.Delay(100);
            raycastHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        }
    
    
    
        private void SelectBarracks(RaycastHit raycastHit)
        {
            GameObject selectedBarrack = raycastHit.transform.gameObject;
            CloseSelectedPanel(selectedBarrack);
            _selectedGameObject = selectedBarrack;
            OpenBarracksPanel(_selectedGameObject);
        }
    
        private void SelectPowerPlant(RaycastHit raycastHit)
        {
            GameObject selectedPowerPlant = raycastHit.transform.gameObject;
            CloseSelectedPanel(selectedPowerPlant);
            _selectedGameObject = selectedPowerPlant;

        }
    
        private void SelectSoldier(RaycastHit raycastHit)
        {
            GameObject selectedSoldier = raycastHit.transform.gameObject;
            CloseSelectedPanel(selectedSoldier);
            _selectedGameObject = selectedSoldier;
        }

   
    
    
        private void OpenBarracksPanel(GameObject selectedBarrack)
        {
            Transform barracksPanel = selectedBarrack.transform.GetChild(0);
            barracksPanel.gameObject.SetActive(true);
        
        
        }
        private void CloseSelectedPanel(GameObject givenGameObject)
        {
            if (_selectedGameObject != null  && _selectedGameObject != givenGameObject.transform.gameObject)
            {
                Transform Panel = new RectTransform();
                try
                {
                    Panel = _selectedGameObject.transform.GetChild(0);
                
                }
                catch 
                {
                    Debug.Log("Gameobject has no panel");
                }
                if (Panel != null && Panel.gameObject.activeSelf)
                {
                    Panel.gameObject.SetActive(false);
                
                }
            }
        }


    }
}

