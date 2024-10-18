using UnityEngine;

public class RotateTest : MonoBehaviour
{
    public Transform sonTrans;

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
       
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Debug.Log("son2:" + _son2Obj.transform.position);
        Debug.Log("son2 local:" + _son2Obj.transform.localPosition);
        Debug.Log("son1:" + sonTrans.position);
        Debug.Log("son1 local:" + sonTrans.localPosition);
    }
}
