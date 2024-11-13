using UnityEngine;

public class DropDown : MonoBehaviour
{
    public BuffData effectBuff; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.TryGetRootParent(collision.gameObject).TryGetComponent<BuffHandler>(out var buffHandler))
        {
            BuffInfo buffInfo = new()
            {
                buffData = effectBuff,
                target = collision.gameObject
            };
            buffHandler.AddBuff(buffInfo);
        }
        Destroy(gameObject);
    }
}   
