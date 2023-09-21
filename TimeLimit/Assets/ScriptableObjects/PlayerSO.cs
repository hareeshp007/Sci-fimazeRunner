using Game.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Player/NewPlayer")]
public class PlayerSO : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int speed;
    public int RunSpeed;
    public int HealValue;
    public float jumpHeight;
    public float JumpHoveringValue;
    public PlayerView player;
}
