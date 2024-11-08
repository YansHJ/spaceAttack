using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _popover;

    private Image _popoverImage;

    private TextMeshProUGUI _popoverText;

    private Color _transparentColor = new Color(1, 1, 1, 0);

    private Color _displayColor = new Color(1, 1, 1, 1);

    private void Awake()
    {
        _popoverText = _popover.GetComponentInChildren<TextMeshProUGUI>();
        _popoverImage = _popover.GetComponent<Image>();
        //初始化隐藏显示
        if (_popoverImage != null)
        {
            _popoverImage.color = _transparentColor;
            _popoverText.text = "";
        }
    }

    private void OnEnable()
    {
        EventManager.Popover += OnPopover;
    }

    private void OnDisable()
    {
        EventManager.Popover -= OnPopover;
    }

    private void OnPopover(string message)
    {
        Pop(message);
    }

    private void Pop(string message)
    {
        //提示框弹出动效 TODO
        StartCoroutine(ImageFadeIn());
        //文字显示
        _popoverText.text = message;
    }

    IEnumerator ImageFadeIn()
    {
        _popoverImage.DOColor(_displayColor, 0.1f);
        yield return new WaitForSeconds(3f);
        _popoverImage.DOColor(_transparentColor, 1f);
        _popoverText.text = "";
    }
}
