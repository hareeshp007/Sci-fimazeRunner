using Game.player;
using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    public Transform StartPos;
    public PlayerSO PlayerSO;
    [SerializeField]
    private UiManger uiManger;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerView playerView;
    [SerializeField]
    private PlayerModel playerModel;

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
