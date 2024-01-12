using Game.player;
using UnityEngine;

public class PlayerService 
{
    private Transform StartPos;
    private PlayerSO PlayerSO;
    private UiManger uiManger;
    private PlayerController playerController;
    private PlayerView playerView;
    private PlayerModel playerModel;

    public PlayerService( Transform StartPos,PlayerSO PlayerSO)
    {
        this.StartPos = StartPos;
        this.PlayerSO = PlayerSO;
    }
    private void CreatePlayer()
    {
        
        this.playerModel = new PlayerModel();
        this.playerView = GameObject.Instantiate<PlayerView>(PlayerSO.player,StartPos);
        Debug.Log("PlayerSpawned");
        this.playerController = new PlayerController(playerView,playerModel, PlayerSO);
        SetUIManager();
    }

    public PlayerView GetPlayer()
    {
        return this.playerView;
    }
    public void SetUIManager(UiManger _uiManger)
    {
        uiManger = _uiManger;
        CreatePlayer();
    }
    public void SetUIManager()
    {
        this.playerView.SetUIManager(uiManger);
    }
}
