using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts._GameScene.__GameElements.Creater.RealCreater.BarackCreaters;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Features;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._GameArea;
using _Scripts._GameScene._PlayerControl;
using _Scripts._GameScene._UI.Features;
using Codice.Client.BaseCommands;
using UnityEngine;

namespace _Scripts._GameScene.ManagersInGame
{
    public class MouseController : MonoBehaviour
    {
        private ClickRay _clickRay;
        private HeavySoldierCreater heavySoldierCreater;
        private MedimuSoldierCreater mediumSoldierCreater;
        private LightSoldierCreater lightSoldierCreater;
        private GameObject _Mouse0_selectedGameObject;
        private Barracks _mouse0_selectedBarracks;
        private PowerPlant _mouse0_selectedPowerPlant;
        private HeavySoldier _mouse0_selectedHeavySoldier;
        private MediumSoldier _mouse0_selectedMediumSoldier;
        private LightSoldier _mouse0_selectedLightSoldier;
        private bool _isHoldingMouse0;
        private Vector3 _mouseWorldPositionOnDown;
        private Vector3 _mouseWorldPositionOnHold;
        private Vector3 _vectorsDistance;
        private IMovable Mouse0_SelectedMovable;
        private IVulnerable _mouse1_selectedVulnerable;
        private IPaneled _mouse0_SelectedPaneled;
        private IPaneled Mouse0_SelectedPaneled
        {
            get
            {
                return _mouse0_SelectedPaneled;
            }
            set
            {
             CloseSelectedPanel(_mouse0_SelectedPaneled);
             OpenBarracksPanel(value);
             _mouse0_SelectedPaneled = value;
            }
        }

        private IAttacker Mouse0_SelectedAttacker;
        
        
        
        
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
            MoveMovable(Mouse0_SelectedMovable);

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
                        Barracks barrack = barrackResult.Result.Item1.transform.gameObject.GetComponent<Barracks>();
                        if (barrack is IVulnerable && Mouse0_SelectedAttacker is IAttacker)
                        {
                            Debug.Log("doğru");
                        }
                        Debug.Log(Mouse0_SelectedAttacker.Damage);
                        TriggerAttackEvent<Barracks,IAttacker>(barrack,Mouse0_SelectedAttacker);
                    }

                    if (powerPlantResult.Result.Item2 && !soldierCreateMenuResult.Result.Item2)
                    {
                        PowerPlant powerPlant = barrackResult.Result.Item1.transform.gameObject.GetComponent<PowerPlant>();
                        TriggerAttackEvent<PowerPlant,IAttacker>(powerPlant,Mouse0_SelectedAttacker);
                    }

                    if (heavyResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        HeavySoldier heavySoldier = barrackResult.Result.Item1.transform.gameObject.GetComponent<HeavySoldier>();
                        TriggerAttackEvent<HeavySoldier,IAttacker>(heavySoldier,Mouse0_SelectedAttacker);
                    }

                    if (mediumResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        MediumSoldier mediumSoldier = barrackResult.Result.Item1.transform.gameObject.GetComponent<MediumSoldier>();
                        TriggerAttackEvent<MediumSoldier,IAttacker>(mediumSoldier,Mouse0_SelectedAttacker);
                    }

                    if (lightResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        LightSoldier lightSoldier = barrackResult.Result.Item1.transform.gameObject.GetComponent<LightSoldier>();
                        TriggerAttackEvent<LightSoldier,IAttacker>(lightSoldier,Mouse0_SelectedAttacker);
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
                    if(await _clickRay.CheckMouseInTruePosition("GameBoard"))
                        
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
                        
                        _Mouse0_selectedGameObject = barrackResult.Result.Item1.transform.gameObject;
                        SetToMyInheritanceOnMouse0<Barracks>(barrackResult.Result.Item1.transform.gameObject.GetComponent<Barracks>());

                    }

                    if (powerPlantResult.Result.Item2 && !soldierCreateMenuResult.Result.Item2)
                    {
                       
                        SetToMyInheritanceOnMouse0<PowerPlant>(powerPlantResult.Result.Item1.transform.gameObject.GetComponent<PowerPlant>());
                        _Mouse0_selectedGameObject = powerPlantResult.Result.Item1.transform.gameObject;
                        
                    }

                    if (heavyResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        
                        SetToMyInheritanceOnMouse0<HeavySoldier>(heavyResult.Result.Item1.transform.gameObject.GetComponent<HeavySoldier>());
                        _Mouse0_selectedGameObject = heavyResult.Result.Item1.transform.gameObject;
                        
                    }

                    if (mediumResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                       
                        _Mouse0_selectedGameObject = mediumResult.Result.Item1.transform.gameObject;
                        SetToMyInheritanceOnMouse0<MediumSoldier>(mediumResult.Result.Item1.transform.gameObject.GetComponent<MediumSoldier>());
                    }

                    if (lightResult.Result.Item2  && !soldierCreateMenuResult.Result.Item2)
                    {
                        
                        _Mouse0_selectedGameObject = lightResult.Result.Item1.transform.gameObject;
                        SetToMyInheritanceOnMouse0<LightSoldier>(mediumResult.Result.Item1.transform.gameObject.GetComponent<LightSoldier>());
                    }

                    if (heavyButtonResult.Result.Item2)
                    {
                        CreateHeavySoldierButton(heavyButtonResult.Result.Item1, _Mouse0_selectedGameObject);
                    }

                    if (mediumButtonResult.Result.Item2)
                    {
                        CreateMediumSoldierButton(mediumButtonResult.Result.Item1, _Mouse0_selectedGameObject);
                    }

                    if (lightButtonResult.Result.Item2)
                    {
                        CreateLightSoldierButton(lightButtonResult.Result.Item1, _Mouse0_selectedGameObject);
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
                Mouse0_SelectedAttacker = (IAttacker)sameobject;
            }
            
            if (sameobject is IPaneled)
            {
                Mouse0_SelectedPaneled = (IPaneled)sameobject;
            }
        }
        
        
        
        
        

        private void CreateHeavySoldierButton(RaycastHit raycastHit, GameObject oldSelectedGameObject)
        {
            ClickAnimation(raycastHit);
            
            if (oldSelectedGameObject != null && oldSelectedGameObject.TryGetComponent<Barracks>(out Barracks barracks))
            {
              oldSelectedGameObject.GetComponent<Barracks>().EventCreateHeavySoldier.Invoke();
            }
            else
            {
                Debug.Log("Select first barracks");
            }
            
        }
        private void CreateMediumSoldierButton(RaycastHit raycastHit,GameObject oldSelectedGameObject)
        {
            ClickAnimation(raycastHit);
            if (oldSelectedGameObject != null && oldSelectedGameObject.TryGetComponent<Barracks>(out Barracks barracks))
            {
                oldSelectedGameObject.GetComponent<Barracks>().EventCreateMediumSoldier.Invoke();
            }
            else
            {
                Debug.Log("Select first barracks");
            }
            
        }
        private void CreateLightSoldierButton(RaycastHit raycastHit,GameObject oldSelectedGameObject)
        {
            ClickAnimation(raycastHit);
            if (oldSelectedGameObject != null && oldSelectedGameObject.TryGetComponent<Barracks>(out Barracks barracks))
            {
                oldSelectedGameObject.GetComponent<Barracks>().EventCreateLightSoldier.Invoke();
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
            if (oldSelectedPanel != null)
            {
                oldSelectedPanel.MyPanel.SetActive(true);
            }
            else
            {
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
        
        private Vector3 GetTwoPointDistance()
        {
            return _mouseWorldPositionOnDown - _mouseWorldPositionOnHold;

        }

        
        
        
        

    }
}

