using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene._UI.Features;
using Codice.Client.BaseCommands;
using UnityEngine;

namespace _Scripts._GameScene.ManagersInGame
{
    public class MouseManager : MonoBehaviour
    {
        private ClickRay _clickRay;
        private GameObject _selectedGameObject;
        private Barracks _selectedBarracks;
        private PowerPlant _selectedPowerPlant;
        private Soldier _selectedSoldier;
        private int _doubleclick;
        private Task<bool> _isDoubleClickCounterDone;
        [SerializeField] private GameSpace _gameSpace;
        private HeavySoldierCreater heavySoldierCreater;
        private MediumSoldierCreater mediumSoldierCreater;
        private LightSoldierCreater lightSoldierCreater;


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
                if (await _clickRay.CheckMouseInTruePosition("Canvas"))
                {
                    RaycastHit[] _raycastHitList = await _clickRay.ThrowRayTryCatchGameColliders();
                    bool IsThereAnyMenu = _clickRay.IsThereSpecificRaycastHit("SoldierCreateMenu", _raycastHitList);
                    if (_clickRay.IsThereSpecificRaycastHit("GameBoard", _raycastHitList))
                    {
                        if (_clickRay.IsThereSpecificRaycastHit("Barracks", _raycastHitList) && !IsThereAnyMenu)
                        {
                            RaycastHit _raycastHit = _clickRay.TakeSpecificRaycastHit("Barracks", _raycastHitList);
                            SelectBarracks(_raycastHit);
                        }

                        if (_clickRay.IsThereSpecificRaycastHit("PowerPlant", _raycastHitList) && !IsThereAnyMenu)
                        {
                            RaycastHit _raycastHit = _clickRay.TakeSpecificRaycastHit("PowerPlant", _raycastHitList);
                            SelectPowerPlant(_raycastHit);
                        }

                        if (_clickRay.IsThereSpecificRaycastHit("Soldier", _raycastHitList) && !IsThereAnyMenu)
                        {
                            RaycastHit _raycastHit = _clickRay.TakeSpecificRaycastHit("Soldier", _raycastHitList);
                            SelectSoldier(_raycastHit);
                        }

                        if (_clickRay.IsThereSpecificRaycastHit("HeavySoldierButton", _raycastHitList))
                        {
                            RaycastHit _raycastHit = _clickRay.TakeSpecificRaycastHit("HeavySoldierButton", _raycastHitList);
                            CreateHeavySoldierButton(_raycastHit, _raycastHit.transform.gameObject.GetComponent<Barracks>());
                        }

                        if (_clickRay.IsThereSpecificRaycastHit("MediumSoldierButton", _raycastHitList))
                        {
                            RaycastHit _raycastHit = _clickRay.TakeSpecificRaycastHit("MediumSoldierButton", _raycastHitList);
                            CreateMediumSoldierButton(_raycastHit, _raycastHit.transform.gameObject.GetComponent<Barracks>());
                        }

                        if (_clickRay.IsThereSpecificRaycastHit("LightSoldierButton", _raycastHitList))
                        {
                            RaycastHit _raycastHit = _clickRay.TakeSpecificRaycastHit("LightSoldierButton", _raycastHitList);
                            CreateLightSoldierButton(_raycastHit, _raycastHit.transform.gameObject.GetComponent<Barracks>());
                        }
                    }
                }
            }
        }






        private void CreateHeavySoldierButton(RaycastHit raycastHit, Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
            _selectedBarracks.CreateHeavySoldier.Invoke();
        }

        private void CreateMediumSoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
            _selectedBarracks.CreateMediumSoldier.Invoke();
        }
        private void CreateLightSoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
            _selectedBarracks.CreateLightSoldier.Invoke();
        }
        
        private void SelectBarracks(RaycastHit raycastHit)
        {
            
            CloseSelectedPanel(_selectedGameObject);
            GameObject selectedBarrack = raycastHit.transform.gameObject;
            _selectedGameObject = selectedBarrack;
            _selectedBarracks = selectedBarrack.GetComponent<Barracks>();
            OpenBarracksPanel(_selectedGameObject);
        }
        private void SelectPowerPlant(RaycastHit raycastHit)
        {
            CloseSelectedPanel(_selectedGameObject);
            GameObject selectedPowerPlant = raycastHit.transform.gameObject;
            _selectedGameObject = selectedPowerPlant;
            _selectedPowerPlant = selectedPowerPlant.GetComponent<PowerPlant>();

        }
        private void SelectSoldier(RaycastHit raycastHit)
        {
            CloseSelectedPanel(_selectedGameObject);
            GameObject selectedSoldier = raycastHit.transform.gameObject;
            _selectedGameObject = selectedSoldier;
            if (selectedSoldier.tag == "HeavySoldier")
            {
                _selectedSoldier = selectedSoldier.GetComponent<HeavySoldier>();
            }

            if (selectedSoldier.tag == "MediumSoldier")
            {
                _selectedSoldier = selectedSoldier.GetComponent<MediumSoldier>();
            }
            if (selectedSoldier.tag == "LightSoldier")
            {
                _selectedSoldier = selectedSoldier.GetComponent<LightSoldier>();
            }
        }
        
        
        
        
        
       
        
        private void OpenBarracksPanel(GameObject selectedBarrack)
        {
            Transform barracksPanel = selectedBarrack.transform.GetChild(0);
            barracksPanel.gameObject.SetActive(true);
        
        
        }
        private void CloseSelectedPanel(GameObject givenGameObject)
        {
            if (_selectedGameObject != null )
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

        private async void ClickAnimation(RaycastHit raycastHit)
        {
            raycastHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.grey;
            await Task.Delay(100);
            raycastHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        }

        private void TriggerHeavySoldierCreater()
        {
           


        }
        private void TriggerMediumSoldierCreater()
        {
            
            
            
        }
        private void TriggerLightSoldierCreater()
        {
            
            
            
        }

        
        
        
        

    }
}

