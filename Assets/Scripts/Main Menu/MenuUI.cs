using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public static MenuUI instance;
    public Text title;
    public Text description;

    string current;

    public GameObject currentButton;

    public Material selectedColor;
    public Material normalColor;

    public float animationSpeed;

    public Image fadeImage;

    public GameObject menuTitle;

    void Start()
    {
        instance = this;  
        if(GameManager.instance.seenTitle){
            menuTitle.SetActive(false);
        } 
        StartCoroutine(WaitForCorrect()); 
    }

    IEnumerator WaitForCorrect(){
        while(UpdateMap.instance==null){
            yield return new WaitForEndOfFrame();
        }
        ShowBriefingForMission("1",currentButton);
    }

    public void ShowBriefingForMission(string mission,GameObject ob){

        currentButton.GetComponent<Renderer>().material = normalColor;
        ob.GetComponent<Renderer>().material = selectedColor;
        currentButton = ob;

        Mission ms = UpdateMap.instance.GetMission(mission);
        title.text = ms.missionName;
        description.text = ms.description;
        current = mission;
    }

    public void LoadMission(){
        StartCoroutine(AnimationBeforLaunch(current));
    }


    IEnumerator AnimationBeforLaunch(string scene){
        for(int i = 0;i<transform.childCount;i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
        fadeImage.gameObject.SetActive(true);
        Transform cam = Camera.main.transform;

        while(cam.rotation.x>0){
            cam.Rotate(new Vector3(-1,0,0)*Time.deltaTime*animationSpeed);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        while(fadeImage.color!=Color.black){
            fadeImage.color = Color.Lerp(fadeImage.color,Color.black,Time.deltaTime*5);
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(current);
    }
}
