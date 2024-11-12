using UnityEngine;

public class DropDown : MonoBehaviour
{
    public BuffData effectBuff; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("获取到了治疗");
        if (GameManager.Instance.TryGetRootParent(collision.gameObject).TryGetComponent<BuffHandler>(out var buffHandler))
        {
            BuffInfo buffInfo = new()
            {
                buffData = effectBuff,
                target = collision.gameObject
            };
            buffHandler.AddBuff(buffInfo);
        }
    }
}   
