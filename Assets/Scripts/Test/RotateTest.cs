using UnityEngine;

public class RotateTest : MonoBehaviour
{
    //通过拖拽赋值的子对象，在编译时就已经拖拽成为子对象（当父对象旋转或移动时，该对象的transform可以跟随改变）
    public Transform sonTrans;
    //通过拖拽赋值的预制件，在脚本里通过代码实例化，并设置父级位上面的
    public Transform son2Trans;

    private GameObject _son2Obj;

    private void Start()
    {
        _son2Obj = Instantiate(son2Trans.gameObject);
        /*son2Obj.transform.parent = sonTrans;*/
        _son2Obj.transform.SetParent(sonTrans, false);
        /*son2Obj.transform.localPosition = Vector3.zero;
        son2Obj.transform.localRotation = Quaternion.identity;*/
    }

    private void Update()
    {
        //获取世界鼠标坐标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //当前父级位置与鼠标位置的向量
        Vector3 mouseDir = (mousePos - transform.position).normalized;
        //转化为角度
        float angle = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg;
        //父级旋转
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Debug.Log("son2:" + _son2Obj.transform.position);
        Debug.Log("son2 local:" + _son2Obj.transform.localPosition);
        Debug.Log("son1:" + sonTrans.position);
        Debug.Log("son1 local:" + sonTrans.localPosition);
    }
}
