using System;
using System.Reflection;
using _Scripts._GameScene.__GameElements.Factorys;
using _Scripts._GameScene.__GameElements.Factorys.Creater;
using _Scripts._GameScene.__GameElements.Products;
using _Scripts._GameScene.__GameElements.Products.Soldiers;
using _Scripts._GameScene._Logic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

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
        
        public void InIt()
        {

            AddButtonToListener();
            

        }

        public void deneme()
        {
            
            Debug.Log("çalıştım.");
            
            
        }
        private void AddButtonToListener()
        {
            _barracks.onClick.AddListener(() => DoAboutTheGameObject(new Barracks().gameObject));
            _powerPlant.onClick.AddListener(() => DoAboutTheGameObject(new PowerPlant().gameObject));
            _heavySoldier.onClick.AddListener(() => DoAboutTheGameObject(new HeavySoldier().gameObject));
            _mediumSoldier.onClick.AddListener(() => DoAboutTheGameObject(new MediumSoldier().gameObject));
            _lightSoldier.onClick.AddListener(() => DoAboutTheGameObject(new LightSoldier().gameObject));
        }

        private void DoAboutTheGameObject(GameObject gameObject)
        {
            Debug.Log("did the game object ");
            if (gameObject is Factory)
            {
                DoAboutFactorys(gameObject);
                Debug.Log("this game object factory");
            }

            if (gameObject is IProduct)
            {
                DoAboutProducts(gameObject);
                Debug.Log("this game object product");
            }
            
        }
        
        
        
        
        private void DoAboutFactorys(GameObject gameObject)
        {
            Debug.Log("Did factory");
        }

        private void DoAboutProducts(GameObject gameObject)
        {
            Debug.Log("Did product");
            
            
        }


        private int HowMuchProductTypeYouHave(Factory factory)//Every ICreater creating one product type.
        {
            Type factorType = factory.GetType();
            int variableCount = 0;

            foreach (FieldInfo field in factorType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.FieldType == typeof(ICreater))
                {
                    variableCount++;
                }
            }

            return variableCount;
        }
        
        
        
        
        
        
        
    }
}