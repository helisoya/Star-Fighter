using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateMap : MonoBehaviour
{

    public static UpdateMap instance;


    public Mission[] missions;

    void Start(){
        instance = this;
        UpdateBriefingMap();
    }

    void UpdateBriefingMap(){
        foreach(Mission mission in missions){
            if(GameManager.instance.HasFinishedMission(mission.id)
            && mission.trailsIfCompleted.Length != 0){
                foreach(GameObject trail in mission.trailsIfCompleted){
                   trail.SetActive(true);
                }       
            }
            if(CheckMissionRequirement(mission)){
                mission.missionMarker.SetActive(true);
            }
        }
    }


    bool CheckMissionRequirement(Mission mission){
        if(mission.requirement.Length==0){
            return true;
        }

        foreach(string exclusive in mission.exclusiveWith){
            if(GameManager.instance.HasFinishedMission(exclusive)){
                if(mission.exclusiveTrail != null){
                    mission.exclusiveTrail.SetActive(false);
                }
                return false;
            }
        }

        foreach(string requirement in mission.requirement){
            if(GameManager.instance.HasFinishedMission(requirement)){
                return true;
            }
        }
        return false;
    }


    public Mission GetMission(string id){
        foreach(Mission mission in missions){
            if(mission.id == id){
                return mission;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Mission{

    public string id;
    public string missionName;
    public string description;

    public string[] requirement;

    public string[] exclusiveWith;

    public GameObject exclusiveTrail;

    public GameObject[] trailsIfCompleted;

    public GameObject missionMarker; 
}
