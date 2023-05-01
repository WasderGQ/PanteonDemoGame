using System;
using System.ComponentModel;
using System.Reflection;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Factorys;
using _Scripts._GameScene.__GameElements.Products.RealProduct.Soldiers;
using _Scripts._GameScene.__GameElements.Products.VirtualProduct;
using _Scripts._GameScene._GameArea;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = System.Object;

namespace _Scripts._GameScene._UI
{
    public class ProductMenu : MonoBehaviour
    { 
        [SerializeField] private GameSpace _gameSpace;
        [SerializeField] private Information _information;
        [SerializeField] private Button _btn_Barracks;
        [SerializeField] private Button _btn_PowerPlant;
        [SerializeField] private Button _btn_HeavySoldier;
        [SerializeField] private Button _btn_MediumSoldier;
        [SerializeField] private Button _btn_LightSoldier;
        
        
        public void InIt()
        {

            AddButtonToListener();
           

        }

        
        private void AddButtonToListener()
        {
            _btn_Barracks.onClick.AddListener(() => DoAboutTheGameObject(new Barracks()));
            _btn_PowerPlant.onClick.AddListener(() => DoAboutTheGameObject(new PowerPlant()));
            _btn_HeavySoldier.onClick.AddListener(() => DoAboutTheGameObject(new HeavySoldier()));
            _btn_MediumSoldier.onClick.AddListener(() => DoAboutTheGameObject(new MediumSoldier()));
            _btn_LightSoldier.onClick.AddListener(() => DoAboutTheGameObject(new LightSoldier()));
            
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
        
    }
}