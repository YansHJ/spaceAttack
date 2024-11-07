using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField]
    private int _moneyCnt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("玩家接触金币：" + collision.name);
        if (collision.CompareTag(TagConstants.PLAYER_WEAPON) 
            || collision.CompareTag(TagConstants.PLAYER) 
            || collision.CompareTag(TagConstants.PLAYER_BODY))
        {
            EventManager.CallPlayerGetMoney(_moneyCnt);
            Destroy(gameObject);
        }
    }

    public void SetMoneyCnt(int cnt)
    {
        this._moneyCnt = cnt;
    }
}
