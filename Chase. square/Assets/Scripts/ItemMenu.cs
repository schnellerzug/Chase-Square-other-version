using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] private GameItemHolder holderPrefab;

    public List<GameItemHolder> itemHolders;
    public List<GameItemHolder> boosterHolders;

    [SerializeField] private float distance;

    private float startXposition;

    

    public void ShowItem()
    {
        print(itemHolders);
        if(itemHolders.Count == 0)
        {
            for (int i = 0; i < Storage.instance.playerItem.Length; i++)
            {
                var p = Instantiate(holderPrefab.gameObject, transform);
                p.GetComponent<GameItemHolder>().item = Storage.instance.playerItem[i];
                startXposition = p.transform.position.x;
                itemHolders.Add(p.GetComponent<GameItemHolder>());


            }
        }


        var n = 0;
        foreach (var item in itemHolders)
        {
            if(item.item.amount > 0)
            {
                
                var xposition = startXposition + distance * n;
                item.transform.position = new Vector3(xposition,item.transform.position.y,0);
                print(xposition);
                item.gameObject.SetActive(true);
                n++;
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            
        }
    }

    public void ShowBooster()
    {
        if (boosterHolders.Count == 0)
        {
            boosterHolders.Clear();
            for (int i = 0; i < Storage.instance.booster.Length; i++)
            {
                var p = Instantiate(holderPrefab.gameObject, transform);
                p.GetComponent<GameItemHolder>().item = Storage.instance.booster[i];
                startXposition = p.transform.position.x;
                boosterHolders.Add(p.GetComponent<GameItemHolder>());


            }
        }


        var n = 0;
        foreach (var item in boosterHolders)
        {
            if (item.item.amount > 0)
            {

                var xposition = startXposition + distance * n;
                item.transform.position = new Vector3(xposition, item.transform.position.y, 0);
                print(xposition);
                item.gameObject.SetActive(true);
                n++;
            }
            else
            {
                item.gameObject.SetActive(false);
            }

        }
    }


}
