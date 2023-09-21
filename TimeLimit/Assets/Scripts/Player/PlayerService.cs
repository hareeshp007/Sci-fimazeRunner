using Game.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    //
    public Transform StartPos;
    public PlayerSO playerSO;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerView playerView;
    [SerializeField]
    private PlayerModel playerModel;
    protected override void Awake()
    {
        base.Awake();
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        this.playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate<PlayerView>(playerSO.player,StartPos);
        this.playerController = new PlayerController(playerView,playerModel,playerSO);

    }

    public PlayerView GetPlayer()
    {
        return this.playerView;
    }
}
