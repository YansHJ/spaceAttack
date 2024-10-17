using UnityEngine;

public class RotateTest : MonoBehaviour
{
    //ͨ����ק��ֵ���Ӷ����ڱ���ʱ���Ѿ���ק��Ϊ�Ӷ��󣨵���������ת���ƶ�ʱ���ö����transform���Ը���ı䣩
    public Transform sonTrans;
    //ͨ����ק��ֵ��Ԥ�Ƽ����ڽű���ͨ������ʵ�����������ø���λ�����
    public Transform son2Trans;

    private void Start()
    {
        //����ʵ����
        GameObject son2Obj = Instantiate(son2Trans.gameObject);
        /*son2Obj.transform.parent = sonTrans;*/
        //���ø���
        son2Obj.transform.SetParent(sonTrans, false);
        /*son2Obj.transform.localPosition = Vector3.zero;
        son2Obj.transform.localRotation = Quaternion.identity;*/
    }

    private void Update()
    {
        //��ȡ�����������
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //��ǰ����λ�������λ�õ�����
        Vector3 mouseDir = (mousePos - transform.position).normalized;
        //ת��Ϊ�Ƕ�
        float angle = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg;
        //������ת
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //��ӡ����ʵ������������
        Debug.Log("son2:" + son2Trans.position);
        Debug.Log("son2 local:" + son2Trans.localPosition);
        //��ӡ��ק��Ϊ������
        Debug.Log("son1:" + sonTrans.position);
        Debug.Log("son1 local:" + sonTrans.localPosition);
    }
}
