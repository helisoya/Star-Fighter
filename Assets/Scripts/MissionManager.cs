using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public bool canMove = true;
    public List<EnnemyHealth> targets;

    public static MissionManager instance;

    public string currentMission;

    public Text victoryText;

    public Color colorVictoryText;

    public Image fadeImage;

    void Start(){
        instance = this;
        foreach(EnnemyHealth target in targets){
            target.target = true;
        }
        StartCoroutine(FadeStart());
    }


    public void RemoveTarget(EnnemyHealth target){
        targets.Remove(target);
        if(targets.Count==0){
            StartCoroutine(AnimationEndMission());
        }
    }

    IEnumerator FadeStart(){
        canMove = false;

        victoryText.color = colorVictoryText;
        victoryText.gameObject.SetActive(true);
        victoryText.text = "PRET ?";

        Color target = new Color(0,0,0,0);
        while(fadeImage.color!=target){
            fadeImage.color = Color.Lerp(fadeImage.color,target,Time.deltaTime*2.5f);
            yield return new WaitForEndOfFrame();
        }

        victoryText.color = target;
        victoryText.text = "VICTOIRE";
        victoryText.gameObject.SetActive(false);
        
        canMove = true;
    }

    IEnumerator AnimationEndMission(){
        victoryText.gameObject.SetActive(true);

        while(victoryText.color!=colorVictoryText){
            victoryText.color = Color.Lerp(victoryText.color,colorVictoryText,Time.deltaTime*5);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2);

        GameManager.instance.AddFinishedMission(currentMission);
        SceneManager.LoadScene("MainMenu");
    }
}
