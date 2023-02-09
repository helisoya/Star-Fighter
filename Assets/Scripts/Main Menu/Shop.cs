using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public GameObject shopHolder;

    public GameObject[] buttons;

    public Text creditsText;
    
    public void OpenShop()
    {
        shopHolder.SetActive(true);
        UpdateShop();
    }

    void UpdateShop()
    {
        creditsText.text = "Credits : " + GameManager.instance.GetCredits().ToString();
        for(int i = 0;i<buttons.Length;i++){
            Item item = GameManager.instance.GetItem(i+1);
            buttons[i].transform.Find("title").GetComponent<Text>().text = item.name;
            buttons[i].transform.Find("desc").GetComponent<Text>().text = item.description;
            buttons[i].transform.Find("price").GetComponent<Text>().text = item.cost.ToString()+" credits";
            buttons[i].transform.Find("Panel").GetComponent<Image>().sprite = item.image;
            
            buttons[i].transform.Find("price").gameObject.SetActive(!GameManager.instance.HasItem(item.id));
            buttons[i].GetComponent<Button>().interactable = !GameManager.instance.HasItem(item.id);
        }
    }

    public void TryBuy(int index){
        Item item = GameManager.instance.GetItem(index);

        if(GameManager.instance.HasEnoughCredits(item.cost)){
            GameManager.instance.Buy(index);
            UpdateShop();
        }
    }
}
