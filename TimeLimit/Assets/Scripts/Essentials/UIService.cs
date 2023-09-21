using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Game.UI
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        public Image HealthImage;
        public TextMeshProUGUI HealthText;

        [SerializeField]
        private float health;
        [SerializeField]
        private float maxHealth =100;
        [SerializeField]
        private float lerpSpeed;
        [SerializeField]
        private float HealthDecreaseRate = 5f;

        private void Start()
        {
            health = maxHealth;
            StartCoroutine(DealConstantDamage());
        }
        private void Update()
        {

            lerpSpeed = 3f * Time.deltaTime;
        }

        public IEnumerator DealConstantDamage()
        {
            health--;
            HealthUpdateGui();
            yield return new WaitForSeconds(HealthDecreaseRate);
            if(health > 0)
            {
                StartCoroutine(DealConstantDamage());
            }
        }

        public void Heal( int HealValue)
        {
            if (HealValue < maxHealth) health += HealValue;
            else health = maxHealth;
            health += HealValue;
            HealthUpdateGui();
        }

        public void DamageHealth(int Damage)
        {
            if(health>0) health-=Damage;
            else health = 0;
            HealthUpdateGui();
        }


        public void HealthSet(int Health)
        {
            health = Health;
            HealthUpdateGui();
        }
        private void HealthBarFiller()
        {
            HealthImage.fillAmount = (health / maxHealth);
            ColorChange();
            
        }
        private void ColorChange()
        {
            Color healthColor = Color.Lerp(Color.red, Color.blue, (health / maxHealth));
            HealthImage.color = healthColor;
        }
        public void HealthUpdateGui()
        {
            if (health > maxHealth) health = maxHealth;
            if (health < 0) health = 0;
            HealthText.text =  health + "%";
            
            HealthBarFiller();
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

