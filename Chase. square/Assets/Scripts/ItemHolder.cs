using UnityEngine.UI;
using System;
using UnityEngine;
using System.Collections;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private ShopItem shopItem;
    [SerializeField] private Text text;
    [SerializeField] private Text coins;

    private float subtractamount;


    // Start is called before the first frame update
    void OnEnable()
    {
        transform.Find("ItemImage").GetComponent<Image>().sprite = shopItem.icon;
        transform.Find("ItemName").GetComponent <Text>().text = shopItem.name;
        transform.Find("BuyButton").GetComponentInChildren<Text>().text = shopItem.cost.ToString();

        if(coins != null)
            coins.text = Storage.instance.coins.ToString();

        text.color = Color.clear;
        coins.color = Color.white;
    }

    public void Buy()
    {
        StopAllCoroutines();
        if(Storage.instance.coins >= shopItem.cost)
        {
            StartCoroutine(StartSubtracting(Storage.instance.coins, 1f));
            subtractamount += shopItem.cost;
            Storage.instance.coins -= shopItem.cost;
            Storage.instance.SaveGame();
            
            text.text = "You bought " + shopItem.name;
            StartCoroutine(ChangeColorAndBack(0.3f, text,Color.clear, Color.green, 1f));

            shopItem.item.OnBuy();
        }
        else
        {
           text.text = "You need " + (shopItem.cost - Storage.instance.coins).ToString() + " more coins";
            StartCoroutine(ChangeColorAndBack(0.3f, text,Color.clear, Color.red, 1f));
            StartCoroutine(ChangeColorAndBack(0.3f, coins,Color.white, Color.red, 1f));
        }
       

    }

    IEnumerator StartSubtracting(float startValue, float lerpDuration)
    {
        float timeElapsed = 0;
        float valueToLerp;
        
            while(timeElapsed < lerpDuration)
            {
                valueToLerp = Mathf.Lerp(startValue, startValue-subtractamount, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                coins.text = valueToLerp.ToString("F0");
                yield return null;
            }
        coins.text = (startValue - subtractamount).ToString("F0");
        subtractamount= 0;

        
    }

    IEnumerator ChangeColorAndBack(float duration, Text text,Color startColor, Color endColor, float waitDuration)
    {
        var time = 0f;


        
        while (time < duration)
        {

            text.color = startColor + (endColor - startColor) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }
        text.color = endColor;
        yield return new WaitForSeconds(waitDuration);
        time = 0f;
        while (time < duration)
        {

            text.color = endColor + (startColor - endColor) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }
        text.color = startColor;
    }

    [Serializable]
    public class ShopItem
    {
        public Item item;

        public string name;
        public string description;
        public Sprite icon;

        public int cost;

    }
}
