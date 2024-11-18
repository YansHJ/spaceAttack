using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreGoodsMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private StoreGoodsDetail _parentGoodsDetail;

    private GameObject _goodsTemp;

    private void OnEnable()
    {
        _parentGoodsDetail = GameManager.Instance.TryGetComponetInHierarchy<StoreGoodsDetail>(gameObject);
        _goodsTemp = GameManager.Instance.TryGetComponetInHierarchy<StoreGoodsUI>(gameObject).goodsTemp;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("鼠标进入了：" + eventData.pointerEnter.name);
        GoodsTempActive();
        GameObject detailCard = eventData.pointerEnter;
        if (detailCard.TryGetComponent<Image>(out var image))
        {
            image.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标离开了：" + eventData.pointerEnter.name);
        GameObject detailCard = eventData.pointerEnter;
        if (detailCard.TryGetComponent<Image>(out var image))
        {
            if (_goodsTemp.TryGetComponent<StoreGoodsDetail>(out var _tempGoodsDetail))
            {
                image.color = _tempGoodsDetail.goodsIcon.color;
            }
        }
        GoodsTempHide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void GoodsTempActive()
    {
        if (_goodsTemp.TryGetComponent<StoreGoodsDetail>(out var _tempGoodsDetail))
        {
            _tempGoodsDetail.goodsNameText = _parentGoodsDetail.goodsNameText;
            _tempGoodsDetail.goodsTypeText = _parentGoodsDetail.goodsTypeText;
            _tempGoodsDetail.goodsDescribe = _parentGoodsDetail.goodsDescribe;
            _tempGoodsDetail.goodsIcon = _parentGoodsDetail.goodsIcon;
            _goodsTemp.transform.position = new Vector3(transform.position.x + 10, transform.position.y + 10, 0);
            _goodsTemp.SetActive(true);
        }
    }

    private void GoodsTempHide()
    {
        _goodsTemp.SetActive(false);
    }
}
