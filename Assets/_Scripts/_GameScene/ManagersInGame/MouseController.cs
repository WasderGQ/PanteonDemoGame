using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._UI.Features;
using UnityEngine;


namespace _Scripts._GameScene.ManagersInGame
{
    public class MouseController : MonoBehaviour
    {
        private ClickRay _clickRay;
        private HeavySoldierCreater heavySoldierCreater;
        private MedimuSoldierCreater mediumSoldierCreater;
        private LightSoldierCreater lightSoldierCreater;
        private bool _isHoldingMouse0;
        private Vector3 _mouseWorldPositionOnDown;
        private Vector3 _mouseWorldPositionOnHold;
        private Vector3 _vectorsDistance;
        private Barracks _mouse0_SelectedBarracks;
        private IMovable Mouse0_SelectedMovable;
        private IVulnerable _mouse1_selectedVulnerable;
        private IPaneled _mouse0_SelectedPaneled;    // Dont use variable use Property.
        private IPaneled Mouse0_SelectedPaneled
        {
            get
            {
                return _mouse0_SelectedPaneled;
            }
            set
            {
                CloseSelectedPanel(_mouse0_SelectedPaneled);
                _mouse0_SelectedPaneled = value;
                OpenBarracksPanel(_mouse0_SelectedPaneled);
            }
        }
        private IAttacker _mouse0_SelectedAttacker;
        private Vector3 mousePositionOnWorld;


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
            Mouse0ClickDown();
            Mouse1ClickDown();
            //MoveMovable(Mouse0_SelectedMovable);
            

        }


        private async void Mouse1ClickDown()
        {
            if (Input.GetMouseButtonDown(1))
            {
                
                RaycastHit[] _raycastHitList = await _clickRay.ThrowRayTryCatchRaycastHits();

                var gameBoardResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("GameBoard", _raycastHitList);
                if (gameBoardResult.Result.Item2)
                {
                    var soldierCreateMenuResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("SoldierCreateMenu", _raycastHitList);
                    var barrackResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("Barracks", _raycastHitList);
                    var powerPlantResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("PowerPlant", _raycastHitList);
                    var heavyResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("HeavySoldier", _raycastHitList);
                    var mediumResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("MediumSoldier", _raycastHitList); 
                    var lightResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("LightSoldier", _raycastHitList);
                    
                    
                    if (barrackResult.Result.Item2 && !soldierCreateMenuResult.Result.Item2)
                    {
                        Barracks barracks = barrackResult.Result.Item1.transform.gameObject.GetComponent<Barracks>();
                        TriggerAttackEvent<Barracks,IAttacker>(barracks,_mouse0_SelectedAttacker);
                    }

                    if (powerPlantResult.Result.Item2 && !soldierCreateMenuResult.Result.Item2)
                    {
                        PowerPlant powerPlant = barrackResult.Result.Item1.transform.gameObject.GetComponent<PowerPlant>();
                        TriggerAttackEvent<PowerPlant,IAttacker>(powerPlant,_mouse0_SelectedAttacker);
                    }

                    if (heavyResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        HeavySoldier heavySoldier = barrackResult.Result.Item1.transform.gameObject.GetComponent<HeavySoldier>();
                        TriggerAttackEvent<HeavySoldier,IAttacker>(heavySoldier,_mouse0_SelectedAttacker);
                    }

                    if (mediumResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        MediumSoldier mediumSoldier = barrackResult.Result.Item1.transform.gameObject.GetComponent<MediumSoldier>();
                        TriggerAttackEvent<MediumSoldier,IAttacker>(mediumSoldier,_mouse0_SelectedAttacker);
                    }

                    if (lightResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        LightSoldier lightSoldier = barrackResult.Result.Item1.transform.gameObject.GetComponent<LightSoldier>();
                        TriggerAttackEvent<LightSoldier,IAttacker>(lightSoldier,_mouse0_SelectedAttacker);
                    }
                }
            }
        }
        void TriggerAttackEvent<TVulnerable,TAttarker>(TVulnerable vulnerable, TAttarker attacker) where TVulnerable : IVulnerable where TAttarker : IAttacker
        {
            try
            {
                vulnerable.TakeDamage(attacker);
            }
            catch 
            {
                Debug.Log("the attacker did not take damage");
            }
        }
        public async void MoveMovable(IMovable movableobject)
        {
            if (movableobject != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse2) && _mouseWorldPositionOnDown == new Vector3())
                {
                    if(await _clickRay.CheckMouseOnCollider("GameBoard"))
                        
                        _mouseWorldPositionOnDown = await GetMousePosition();
                        _isHoldingMouse0 = true;
                        await Task.Delay(100);
                }
                if (_isHoldingMouse0)
                {
                    _mouseWorldPositionOnHold = await GetMousePosition();
                    _vectorsDistance = GetTwoPointDistance();
                    movableobject.Move(_vectorsDistance);
                }
                if (Input.GetKeyUp(KeyCode.Mouse2))
                {
                    _isHoldingMouse0 = false;
                    _mouseWorldPositionOnDown = new Vector3();
                    _mouseWorldPositionOnHold = new Vector3();
                    _vectorsDistance = Vector3.zero;
                }
            }
           
            
            
        }
        private Vector3 GetTwoPointDistance()
        {
            return _mouseWorldPositionOnDown - _mouseWorldPositionOnHold;

        }
        private async void Mouse0ClickDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHoldingMouse0 = true;
                RaycastHit[] _raycastHitList = await _clickRay.ThrowRayTryCatchRaycastHits();

                var gameBoardResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("GameBoard", _raycastHitList);
                if (gameBoardResult.Result.Item2)
                {
                    var soldierCreateMenuResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("SoldierCreateMenu", _raycastHitList);
                    var barrackResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("Barracks", _raycastHitList);
                    var powerPlantResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("PowerPlant", _raycastHitList);
                    var heavyResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("HeavySoldier", _raycastHitList);
                    var mediumResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("MediumSoldier", _raycastHitList);
                    var lightResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("LightSoldier", _raycastHitList);
                    var heavyButtonResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("HeavySoldierButton", _raycastHitList);
                    var mediumButtonResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("MediumSoldierButton", _raycastHitList);
                    var lightButtonResult = _clickRay.TakeSpecificRaycastHitWithTaskBoolOnGivenList("LightSoldierButton", _raycastHitList);

                    if (barrackResult.Result.Item2 && !soldierCreateMenuResult.Result.Item2)
                    {
                        _mouse0_SelectedBarracks = barrackResult.Result.Item1.transform.gameObject.GetComponent<Barracks>();
                        SetToMyInheritanceOnMouse0<Barracks>(_mouse0_SelectedBarracks);
                        
                    }

                    if (powerPlantResult.Result.Item2 && !soldierCreateMenuResult.Result.Item2)
                    {
                        SetToMyInheritanceOnMouse0<PowerPlant>(powerPlantResult.Result.Item1.transform.gameObject.GetComponent<PowerPlant>());

                    }

                    if (heavyResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        
                        SetToMyInheritanceOnMouse0<HeavySoldier>(heavyResult.Result.Item1.transform.gameObject.GetComponent<HeavySoldier>());

                    }

                    if (mediumResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        
                        SetToMyInheritanceOnMouse0<MediumSoldier>(mediumResult.Result.Item1.transform.gameObject.GetComponent<MediumSoldier>());
                    }

                    if (lightResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        
                        SetToMyInheritanceOnMouse0<LightSoldier>(mediumResult.Result.Item1.transform.gameObject.GetComponent<LightSoldier>());
                    }

                    if (heavyButtonResult.Result.Item2)
                    {
                        CreateHeavySoldierButton(heavyButtonResult.Result.Item1, _mouse0_SelectedBarracks);
                    }

                    if (mediumButtonResult.Result.Item2)
                    {
                        CreateMediumSoldierButton(mediumButtonResult.Result.Item1, _mouse0_SelectedBarracks);
                    }

                    if (lightButtonResult.Result.Item2)
                    {
                        CreateLightSoldierButton(lightButtonResult.Result.Item1, _mouse0_SelectedBarracks);
                    }
                }
            }
        }
        private void SetToMyInheritanceOnMouse0<T1>(T1 sameobject) 
        {
            if (sameobject is IMovable)
            {
                Mouse0_SelectedMovable = (IMovable)sameobject;
            }

            if (sameobject is IAttacker)
            {
                _mouse0_SelectedAttacker = (IAttacker)sameobject;
            }
            
            if (sameobject is IPaneled)
            {
                Mouse0_SelectedPaneled = (IPaneled)sameobject;
            }
            
        }
        private void CreateHeavySoldierButton(RaycastHit raycastHit, Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
            try
            {
                selectedBarracks.EventCreateHeavySoldier.Invoke();
            }
            catch (Exception e)
            {
                Debug.Log("Select first barracks");
            }
        }
        
        private void CreateMediumSoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
            if (selectedBarracks != null)
            {
                selectedBarracks.EventCreateMediumSoldier.Invoke();
            }
            else
            {
                Debug.Log("Select first barracks");
            }
            
        }
        private void CreateLightSoldierButton(RaycastHit raycastHit,Barracks selectedBarracks)
        {
            ClickAnimation(raycastHit);
            if (selectedBarracks != null)
            {
                selectedBarracks.EventCreateLightSoldier.Invoke();
            }
            else
            {
                Debug.Log("Select first barracks");
            }
            
        }
        private void OpenBarracksPanel(IPaneled newSelectedPanel)
        {
            newSelectedPanel.MyPanel.SetActive(true);
        }
        private void CloseSelectedPanel(IPaneled oldSelectedPanel)
        {
            try
            {
                oldSelectedPanel.MyPanel.SetActive(false);
            }
            catch
            {
                _mouse0_SelectedPaneled = null;
                Debug.Log("I can't close panel because object is empty");
            }
            
        }
        private async void ClickAnimation(RaycastHit raycastHit)
        {
            raycastHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.grey;
            await Task.Delay(100);
            raycastHit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        }
        private async Task<Vector3> GetMousePosition()
        {
            return await _clickRay.GetRayWorldPosition();


        }
       
        

       
        private Vector3 RoundingtoModCellSize(Vector3 distance)
        {
            float xValue = distance.x;
            float xremaining = xValue % 10f;
            xValue = xValue - xremaining;
            float yValue = distance.y;
            float yremaining = yValue % 10f;
            yValue = yValue - yremaining;
            return new Vector3(xValue, yValue, distance.z);


        }
        public IEnumerable PutBuildingOnSpaceBoard(Transform buildingTransform)
        {
            bool isPutBuilding = false;
            while (!isPutBuilding)
            {
                
                buildingTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (_clickRay.CheckMouseOnCollider("GameBoard").Result)
                {
                    buildingTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    buildingTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }


                if (Input.GetMouseButtonDown(0))
                {
                    isPutBuilding = true;
                }
            }
            yield break;
        }
        

    }
}

