using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(TagConstants.PLAYER);
    }
}
