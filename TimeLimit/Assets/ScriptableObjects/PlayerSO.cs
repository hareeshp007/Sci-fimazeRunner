using Game.player;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Player/NewPlayer")]
public class PlayerSO : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int Speed;
    public int RunSpeed;
    public int HealValue;
    public float jumpHeight;
    public float JumpHoveringValue;
    public PlayerView player;
    public int DamageValue;
}
