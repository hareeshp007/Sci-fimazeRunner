using Game.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerView>() != null)
        {
            Debug.Log("Game Over");
            PlayerView player=other.GetComponent<PlayerView>();
            player.Died();
        }
    }
}
