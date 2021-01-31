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
    private float timeRemaining;

    private int bugsFound;
    public int initialBugs;

    GameObject seeker;
    GameObject hider;
    GameObject seekerCam;
    GameObject hiderCam;
    public Text timerText;
    public Image timerVisual;

    // Update is called once per frame
    void Start(){
        bugsFound = 0;
        timeRemaining = setUpTime;

        seeker = GameObject.Find("SeekerPlayer");
        hider = GameObject.Find("HiderPlayer");
        seekerCam = GameObject.Find("SeekerCamera");
        hiderCam = GameObject.Find("HiderCamera");
        

        seeker.GetComponentInChildren<Light>().enabled = false;
        hider.GetComponentInChildren<Light>().enabled = false;
        seekerState(false);
    }
    void Update()
    {
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

                Camera cam = hiderCam.GetComponent<Camera>();
                cam.rect = new Rect(0,0,1,0.5f);
                seekerState(true);
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
        hiderState(false);
        seeker.GetComponentInChildren<Light>().color = Color.blue;
        seeker.GetComponentInChildren<Light>().enabled = true;
        seekerCam.GetComponent<Camera>().rect = new Rect(0,0,1,1);
    }
    private void hiderWins(){
        //hider wins hooray?
        seekerState(false);
        hider.GetComponentInChildren<Light>().color = Color.red;
        hider.GetComponentInChildren<Light>().enabled = true;
        hiderCam.GetComponent<Camera>().rect = new Rect(0,0,1,1);
    }
    private void hiderState(bool state){
        hider.SetActive(state);
        hiderCam.SetActive(state);
    }
    private void seekerState(bool state){
        seeker.SetActive(state);
        seekerCam.SetActive(state);

    }


    private void timerFill(float gameMoment) 
    {
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
