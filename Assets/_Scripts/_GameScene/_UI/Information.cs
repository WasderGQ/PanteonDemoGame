using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Scripts._GameScene._UI
{
    public class Information : MonoBehaviour
    {
        [SerializeField] private GameObject _infoBarracks;
        [SerializeField] private GameObject _infoPowerPlant;
        [SerializeField] private GameObject _infoLightSoldier;
        [SerializeField] private GameObject _infoMediumSoldier;
        [SerializeField] private GameObject _infoHeavySoldier;
        [SerializeField] private GameObject _infoElectric;
        [SerializeField] private GameObject _selectedInfo;
        
        public void InIt()
        {
            OnStartCloseAllPanels();
        }

        void OnStartCloseAllPanels()
        {
            _infoBarracks.SetActive(false);
            _infoPowerPlant.SetActive(false);
            _infoHeavySoldier.SetActive(false);
            _infoMediumSoldier.SetActive(false);
            _infoLightSoldier.SetActive(false);
            _infoElectric.SetActive(false);

        }
        
        
        
        
        public void SelectBarracksPanel()
        {
            SelectedInfoClose();
            _selectedInfo = _infoBarracks;
            SelectedInfoOpen();
        }
        public void SelectPowerPlantPanel()
        {
            SelectedInfoClose();
            _selectedInfo = _infoPowerPlant;
            SelectedInfoOpen();
        }
        
        public void SelectMediumSoldierPanel()
        {
            SelectedInfoClose();
            _selectedInfo = _infoMediumSoldier;
            SelectedInfoOpen();
        }
        public void SelectHeavySoldierPanel()
        {
            SelectedInfoClose();
            _selectedInfo = _infoHeavySoldier;
            SelectedInfoOpen();
        }
        public void SelectLightSoldierPanel()
        {
            SelectedInfoClose();
            _selectedInfo = _infoLightSoldier;
            SelectedInfoOpen();
        }
        public void SelectElectricPanel()
        {
            SelectedInfoClose();
            _selectedInfo = _infoElectric;
            SelectedInfoOpen();
        }

        private void SelectedInfoClose()
        {
            if (_selectedInfo != null)
            {
                _selectedInfo.SetActive(false);
            }
            
        }
        private void SelectedInfoOpen()
        { 
            if (_selectedInfo != null)
            {
                _selectedInfo.SetActive(true);
            }
            
        }
    }
}
