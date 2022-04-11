using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemBuyList : MonoBehaviour
{
    public Inventory Customer;
    private Transform itemShop;
    private Image itemShopImage_Image;
    private TMP_InputField BuyAmountInput;
    private Button CancelBtn;
    private Button ComfirmBtn;
    private Item item;
    private Text messageTip;

    private UI_Shop UI_Shop;

    private void Awake()
    {
        UI_Shop = transform.GetComponentInParent<UI_Shop>();
        itemShop = transform.Find("ItemShop");
        itemShopImage_Image = itemShop.Find("ItemShopImage").Find("Image").GetComponent<Image>();
        BuyAmountInput = itemShop.Find("BuyAmountInput").GetComponent<TMP_InputField>();
        CancelBtn = itemShop.Find("CancelBtn").GetComponent<Button>();
        BuyAmountInput.text = "1";
        CancelBtn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        ComfirmBtn = itemShop.Find("ComfirmBtn").GetComponent<Button>();
        ComfirmBtn.onClick.AddListener(() =>
        {
            if (UI_Shop.GetShop().BuyItem(Customer, item, int.Parse(BuyAmountInput.text)))
            {
                messageTip.text = "ÒÑ¹ºÂò " + item.itemType.ToString() + "*" + BuyAmountInput.text;
            }
            else
            {
                messageTip.text = "¹ºÂòÊ§°Ü£¬½ð±Ò²»¹»»ò»õ´æ²»×ã";
            }
            messageTip.gameObject.SetActive(true);
            messageTip.DOFade(1, 1).onComplete += () =>
            {
                messageTip.DOFade(0, 3).onComplete += () =>
                {
                    messageTip.gameObject.SetActive(false);
                };
            };
        });

        BuyAmountInput.onValueChanged.AddListener((str) =>
        {
            if(str == "")
            {
                str = "1";
            }
            int num = int.Parse(str);
            num = Mathf.Clamp(num, 1, 99);
            BuyAmountInput.text = num.ToString();
        });
    }

    public void SetItem(Item item)
    {
        this.item = item;
        itemShopImage_Image.sprite = item.GetSprite();
    }
    public void SetMessageTip(Text text)
    {
        this.messageTip = text;
    }

}
