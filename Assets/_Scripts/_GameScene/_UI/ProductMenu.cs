using System;
using System.ComponentModel;
using System.Reflection;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.ProducibleValuables;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._Logic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace _Scripts._GameScene._UI
{
    public class ProductMenu : MonoBehaviour
    { 
        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private Information _information;
        [SerializeField] private Button _barracks;
        [SerializeField] private Button _powerPlant;
        [SerializeField] private Button _heavySoldier;
        [SerializeField] private Button _mediumSoldier;
        [SerializeField] private Button _lightSoldier;
        [SerializeField] private Button _electric;
        
        public void InIt()
        {

            AddButtonToListener();
            

        }

        
        private void AddButtonToListener()
        {
            _barracks.onClick.AddListener(() => DoAboutTheGameObject(new Barracks()));
            _powerPlant.onClick.AddListener(() => DoAboutTheGameObject(new PowerPlant()));
            _heavySoldier.onClick.AddListener(() => DoAboutTheGameObject(new HeavySoldier()));
            _mediumSoldier.onClick.AddListener(() => DoAboutTheGameObject(new MediumSoldier()));
            _lightSoldier.onClick.AddListener(() => DoAboutTheGameObject(new LightSoldier()));
            _electric.onClick.AddListener(() => DoAboutTheGameObject(new Electric()));
        }

        private void DoAboutTheGameObject(object obj)
        {
            switch (obj)
            {
                case Factory:
                    DoAboutFactorys(obj);
                    break;
                case Soldier:
                    DoAboutSoldiers(obj);
                    break;
                case IProducibleVariable:
                    DoAboutProductiableVariable(obj);
                    break;
            }


        }




        private void DoAboutFactorys(object obj)
        {
            _gameSpace.CreateFactory(obj);
            switch (obj)
            {
                case Barracks:
                    DoAboutBarracks();
                    break;
                case PowerPlant:
                    DoAboutPowerPlant();
                    break;
            }
        }

        private void DoAboutSoldiers(object obj)
        {
            switch (obj)
            {
                case HeavySoldier:
                    DoAboutHeavySolder();
                    break;
                case MediumSoldier:
                    DoAboutMediumSolder();
                    break;
                case LightSoldier:
                    DoAboutLightSolder();
                    break;
            }
            
        }
        private void DoAboutProductiableVariable(object obj)
        {
            switch (obj)
            {
                case Electric:
                    DoAboutElectric();
                    break;
                
            }
            
            
        }

        private void DoAboutBarracks()
        {
            _information.SelectBarracksPanel();
        }
        private void DoAboutPowerPlant()
        {
            _information.SelectPowerPlantPanel();
        }
        private void DoAboutLightSolder()
        {
            _information.SelectLightSoldierPanel();
        }
        private void DoAboutMediumSolder()
        {
            _information.SelectMediumSoldierPanel();
        }
        private void DoAboutHeavySolder()
        {
            _information.SelectHeavySoldierPanel();
        }
        private void DoAboutElectric()
        {
            _information.SelectElectricPanel();
        }
    }
}