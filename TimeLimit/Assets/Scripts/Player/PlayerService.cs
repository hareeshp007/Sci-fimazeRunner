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
        
    }
    private void Start()
    {
        
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
