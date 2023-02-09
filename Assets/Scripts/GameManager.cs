using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool seenTitle = false;
    void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
            instance = this;
            Load();
        }
    }

    public void toggleTitleSeen(){
        seenTitle = true;
    }


    public static void Quit(){
        Application.Quit();
    }

    SAVEFILE save = new SAVEFILE();

    public bool HasFinishedMission(string mission){
        return save.completedMissions.Contains(mission);
    }

    public void AddFinishedMission(string mission){
        if(!save.completedMissions.Contains(mission)){
            save.completedMissions.Add(mission);
            Save();
        }
    }
    public int GetCredits(){
        return save.credits;
    }

    public void AddCredits(int val){
        save.credits+=val;
        Save();
    }

    public bool HasEnoughCredits(int val){
        return save.credits >= val;
    }

    public Item[] items;

    public bool HasItem(string id){
        foreach(int index in save.itemsOwned){
            if(items[index].id==id){
                return true;
            }
        }
        return false;
    }

    public Item GetItem(string id){
        foreach(Item item in items){
            if(item.id==id){
                return item;
            }
        }
        return null;
    }


    public Item GetItem(int id){
        return items[id];
    }

    public void Buy(int index){
        save.credits-=items[index].cost;
        save.itemsOwned.Add(index);
        Save();
    }



    void Save(){
        FileManager.SaveJSON(FileManager.savPath + "save.txt",save);
    }

    void Load(){
        if(System.IO.File.Exists(FileManager.savPath + "save.txt")){
            save = FileManager.LoadJSON<SAVEFILE>(FileManager.savPath + "save.txt");
        }
    }
}


[System.Serializable]
public class Item{
    public string id;
    public string name;
    public int cost;
    public string description;
    public Sprite image;
}
