using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts._GameScene._UI
{
    public class StaticText : MonoBehaviour
    {
        [SerializeField] private Text _InfoContent;
        
        public void InIt()
        {
            WriteTheText();
        }

        private void WriteTheText()
        {
            _InfoContent.text = "1 Cell = 32 x 32 px \n 1 Soldier = 1 x 1 cell \n Barracks = 4 x 4 cell \n Power Plant = 2 x 3 cell";




        }
        
    }
}