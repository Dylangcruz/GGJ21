using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectorScript : MonoBehaviour
{
    public float setUpTime = 0;
    public float gameTime = 0;
    private bool setUp=true;
    private bool prep=true;
    private float timeRemaining=0;

    private int bugsFound;
    public int initialBugs;

    GameObject seeker;
    GameObject hider;
    public Text timerText;
    public Image timerVisual;

    void Start(){
        timeRemaining = setUpTime;

        seeker = GameObject.FindGameObjectWithTag("SeekerPlayer");
        hider = GameObject.FindGameObjectWithTag("HiderPlayer");
        seeker.SetActive(false);
    }

    void Update(){
        timerText.text = Mathf.Ceil(timeRemaining).ToString();

        //set up bugs
        if (setUp){
            timerFill(setUpTime);
            if((timeRemaining > 0) && (hider.GetComponent<PlayerInteractions>().getBugAmount() > 0)){
            //show that it is set up time
            //change lighting color :)
            timeRemaining -= Time.deltaTime;
            }else{
                timeRemaining=5;
                setUp=false;
            }



            //5 second wait
        }
        else if(prep){
            timerFill(5);
            if (timeRemaining>0){
                timeRemaining -= Time.deltaTime;
            }else{
                prep=false;
                timeRemaining = gameTime;
            }



        //game time
        }else{
            timerFill(gameTime);

            if (timeRemaining>0){
                seeker.SetActive(true);
                if(initialBugs==bugsFound){
                    seekerWins();
                }
                timeRemaining -= Time.deltaTime;
            }else{
                if(hider.GetComponent<PlayerInteractions>().getBugAmount() == 0){
                    hiderWins();
                }else{
                    seekerWins();
                }
            }
        }
    }

    public void foundBug(){
        bugsFound++;
    }
    private void seekerWins(){
        //seeker wins hooray!
        hider.SetActive(false);
        seeker.GetComponentInChildren<Light>().enabled = true;
        //fix camera
    }
    private void hiderWins(){
        //hider wins hooray?
        seeker.SetActive(false);
        hider.GetComponentInChildren<Light>().enabled = true;
        //fix camera
    }


    private void timerFill(float gameMoment){
        timerVisual.fillAmount = timeRemaining / gameMoment;
        if(timerVisual.fillAmount<= .25f)
        {
            timerVisual.color = Color.red;
        }
        else
        {
            timerVisual.color = Color.green;
        }

    }
}
