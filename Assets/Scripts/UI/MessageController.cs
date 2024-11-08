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

    private bool _isDisplaing = false;

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
        if (!_isDisplaing)
        {
            //开始弹窗状态
            _isDisplaing = true;
            StartCoroutine(ImageFadeIn(message));
        }
            
    }

    IEnumerator ImageFadeIn(string message)
    {
        yield return _popoverImage.DOColor(_displayColor, 0.1f).WaitForCompletion();
        char[] chars = message.ToCharArray();
        for (int i = 0;i< chars.Length; i++)
        {
            //文字显示
            _popoverText.text += chars[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(5f);
        _popoverImage.DOColor(_transparentColor, 1f);
        _popoverText.text = "";
        //解除正在弹窗的状态
        _isDisplaing = false;
    }
}
