using Game.player;
using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    //
    public Transform StartPos;
    public PlayerSO playerSO;
    public UiManger uiManger;
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
        SetUIManager();
    }

    public PlayerView GetPlayer()
    {
        return this.playerView;
    }
    private void SetUIManager()
    {
        playerView.SetUIManager(uiManger);
    }
}
