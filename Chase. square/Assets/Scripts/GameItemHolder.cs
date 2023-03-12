using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameItemHolder : MonoBehaviour
{
    public Item item;
    private CanvasGroup cG;

    private Text timer;

    private void Update()
    {
        SetTimer();
    }

    private void OnEnable()
    {
        cG = GetComponent<CanvasGroup>();  
        transform.Find("AmountText").GetComponent<Text>().text = item.amount.ToString();
        timer = transform.Find("NameText").GetComponent<Text>();
        cG.alpha = 0;
        StartCoroutine(Transparent(0.5f,cG,1.5f));

    }

    IEnumerator Transparent(float duration, CanvasGroup canvasGroup, float waitDuration)
    {
        var time = 0f;


        var startColor = canvasGroup.alpha;
        while (time < duration)
        {

            canvasGroup.alpha = startColor + (1 - startColor) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }
        canvasGroup.alpha = 1;
        transform.Find("AmountText").GetComponent<Text>().text = item.amount.ToString();
        yield return new WaitForSeconds(waitDuration);
        time = 0f;
        while (time < duration)
        {

            canvasGroup.alpha = 1 + (startColor - 1) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }
        canvasGroup.alpha = startColor;
        gameObject.SetActive(false);
       
    }

    void SetTimer()
    {
        if (item.active)
        {
            cG.interactable = false;
            timer.text = item.actuelDuration.ToString("F1");
        }
        else 
        { 
            if(item.actuelCooldown> 0)
            {
                cG.interactable = false;
                timer.text = item.actuelCooldown.ToString("F1");
            }
            else
            {
                cG.interactable = true;
                timer.text = "USE";

            }
            
        }

    }

    public void UseItem()
    {
        GameManager.instance.UseItem(item);
    }
}
