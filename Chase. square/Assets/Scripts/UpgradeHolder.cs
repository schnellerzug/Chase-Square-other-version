using UnityEngine.UI;
using System;
using UnityEngine;
using System.Collections;

public class UpgradeHolder : MonoBehaviour
{
    [SerializeField] private ShopUpgrade shopUpgrade;
    [SerializeField] private Text text;
    [SerializeField] private Text coins;


    // Start is called before the first frame update
    void OnEnable()
    {
        transform.Find("ItemImage").GetComponent<Image>().sprite = shopUpgrade.icon;
        transform.Find("ItemName").GetComponent<Text>().text = shopUpgrade.name;
        transform.Find("UpgradeLevel").GetComponent<Text>().text = shopUpgrade.upgrade.actuelLevel.ToString() + "/" + shopUpgrade.upgrade.maxLevel;
        transform.Find("BuyButton").GetComponentInChildren<Text>().text = shopUpgrade.upgrade.actuelLevel < shopUpgrade.upgrade.maxLevel ? shopUpgrade.cost[shopUpgrade.upgrade.actuelLevel].ToString() : "MAX";

        if (coins != null)
            coins.text = Storage.instance.coins.ToString();

        text.color = Color.clear;
        coins.color = Color.white;
    }

    public void Buy()
    {
        StopAllCoroutines();
        if (shopUpgrade.upgrade.actuelLevel == shopUpgrade.upgrade.maxLevel)
        {
            text.text = "Is already Max Level";
            StartCoroutine(ChangeColorAndBack(0.3f, text,Color.clear, Color.red, 1f));
            return;
        }
            
        if (Storage.instance.coins >= shopUpgrade.cost[shopUpgrade.upgrade.actuelLevel])
        {
            StartCoroutine(StartSubtracting(Storage.instance.coins, Storage.instance.coins - shopUpgrade.cost[shopUpgrade.upgrade.actuelLevel], 1f));
            Storage.instance.coins -= shopUpgrade.cost[shopUpgrade.upgrade.actuelLevel];
            Storage.instance.SaveGame();

            text.text = "You bought " + shopUpgrade.name + " " + (shopUpgrade.upgrade.actuelLevel + 1);
            StartCoroutine(ChangeColorAndBack(0.3f, text, Color.clear, Color.green, 1f));

            shopUpgrade.upgrade.Increase();
            transform.Find("UpgradeLevel").GetComponent<Text>().text = shopUpgrade.upgrade.actuelLevel.ToString() + "/" + shopUpgrade.upgrade.maxLevel;
            transform.Find("BuyButton").GetComponentInChildren<Text>().text = shopUpgrade.upgrade.actuelLevel < shopUpgrade.upgrade.maxLevel ? shopUpgrade.cost[shopUpgrade.upgrade.actuelLevel].ToString() : "MAX";
        }
        else
        {
            text.text = "You need " + (shopUpgrade.cost[shopUpgrade.upgrade.actuelLevel] - Storage.instance.coins).ToString() + " more coins";
            StartCoroutine(ChangeColorAndBack(0.3f, text,Color.clear, Color.red, 1f));
            StartCoroutine(ChangeColorAndBack(0.3f, coins,Color.white, Color.red, 1f));
        }


    }

    IEnumerator StartSubtracting(float startValue, float endValue, float lerpDuration)
    {
        float timeElapsed = 0;
        float valueToLerp;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            coins.text = valueToLerp.ToString("F0");
            yield return null;
        }
        coins.text = endValue.ToString("F0");


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
    public class ShopUpgrade
    {
        public Upgrade upgrade;

        public string name;
        public string description;
        public Sprite icon;

        public int[] cost;

    }
}
