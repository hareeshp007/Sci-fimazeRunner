using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class LevelManeger : MonoSingletonGeneric<LevelManeger>
    {
        public String[] Levels;
        
        private void Start()
        {
            if (GetLevelStatus(Levels[0]) == LevelStatus.locked)
            {
                SetLevelStatus(Levels[0], LevelStatus.unlocked);
            }
        }
        public void LoadAnyLevel(int LevelNumber)
        {
            SceneManager.LoadScene(LevelNumber);
            Debug.Log("Loading Level :" + LevelNumber);
        }
        public void LoadNextLevel()
        {
            int Currscene = SceneManager.GetActiveScene().buildIndex;
            if (Currscene <= Levels.Length)
            {
                LoadAnyLevel(Currscene+1);
            }
            Debug.Log(Currscene+1);
            
        }
        public void MarkCurrentLevelCompleted()
        {
            UnityEngine.SceneManagement.Scene currentscene = SceneManager.GetActiveScene();
            SetLevelStatus(currentscene.name, LevelStatus.completed);
            int currentsceneIndex = Array.FindIndex(Levels, level => level == currentscene.name);
            int nextsceneindex = currentsceneIndex + 1;
            if (nextsceneindex < Levels.Length)
            {
                SetLevelStatus(Levels[nextsceneindex], LevelStatus.unlocked);
                Debug.Log(Levels[nextsceneindex] + "next scene name");
            }
            else if (nextsceneindex >= Levels.Length)
            {
                Debug.Log("All Levels Are completed");
            }
        }
        public void SetLevelStatus(string Level, LevelStatus levelStatus)
        {
            PlayerPrefs.SetInt(Level, (int)levelStatus);
            Debug.Log("level name :" + Level + " and its status is :" + levelStatus);
        }
        public LevelStatus GetLevelStatus(string level)
        {
            LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(level, 0);
            Debug.Log("level name :" + level + " and its status is :" + levelStatus);
            return levelStatus;
        }
        public void LevelReset()
        {
            for (int i = 1; i < Levels.Length; i++)
            {
                SetLevelStatus(Levels[i], LevelStatus.locked);
            }
            Debug.Log("Levels Has been reset");
        }
    }
}

