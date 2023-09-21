using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Game.UI
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        


        private void Start()
        {
            
        }
        private void Update()
        {

            
        }

        

        

        internal  void GameWon()
        {
            Debug.Log("Game Won");
        }

        internal void GameOver()
        {
            Debug.Log("Game Over");
        }
        
    }
}

