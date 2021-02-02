using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectorScript : MonoBehaviour
{
    //default game settings
    public float setUpTime = 30;
    public float prepTime = 5;
    public float gameTime = 90;
    public enum RoundState {SetUp,Prep,Game};
    RoundState currentRound;

    //internal timer
    private float setUpRemaining;
    private float timeRemaining;

    private int bugsFound;
    public int initialBugs;

    GameObject seeker;
    GameObject hider;
    private HiderInteractions hScript;

    public Text timerText;
    public Image timerVisual;

/** TODO:
Make timer its own script
Create array to hold objects that have been bugged
highlight bugged objects for hider for eventual multiplayer
**/
    void Start(){
        setUpRemaining = setUpTime;
        timeRemaining = setUpTime;
        seeker = GameObject.FindGameObjectWithTag("SeekerPlayer");
        hider = GameObject.FindGameObjectWithTag("HiderPlayer");
        hScript = hider.GetComponent<HiderInteractions>();
        currentRound = RoundState.SetUp;
        seeker.SetActive(false);
    }

    void Update(){
        timerText.text = Mathf.Ceil(timeRemaining).ToString();

        //set up bugs
        if (currentRound==RoundState.SetUp){
            hScript.isSetUp = true;
            timerFill(setUpTime);
            //fix this condition move it down
            if((timeRemaining > 0) && (hScript.bugAmount > 0)){
                //show that it is set up time
                //change lighting color :)
                timeRemaining -= Time.deltaTime;
                //cut to prep time
            }else{
                setUpRemaining -= (setUpTime-timeRemaining);
                if (setUpRemaining<5){setUpRemaining=0;}
                    timeRemaining = prepTime;
                    currentRound=RoundState.Prep;
            }
        }

        //prep wait
        if(currentRound==RoundState.Prep){
            timerFill(prepTime);
            //start countdown
            if(timeRemaining>0){
                timeRemaining -= Time.deltaTime;
                if(setUpRemaining>prepTime && hScript.bugAmount!=0){
                    currentRound=RoundState.SetUp;
                    setUpRemaining -= (prepTime-timeRemaining);
                    setUpTime = setUpRemaining;
                    timeRemaining = setUpTime;
                }
            //handle not hiding lives and punish
            }else{
                //punishment, remove all bugs except one
                if(hScript.bugAmount>0){
                    bugsFound = hScript.bugAmount-1;
                    hScript.bugAmount = 1;
                }

                currentRound=RoundState.Game;
                timeRemaining = gameTime;
                seeker.SetActive(true);
            }
        }

        //game time
        if(currentRound==RoundState.Game){
            hScript.isSetUp=false;
            timerFill(gameTime);
            if (timeRemaining>0){
                if(initialBugs==bugsFound){
                    seekerWins();
                }
                timeRemaining -= Time.deltaTime;
            }else{
                if(hScript.bugAmount == 0){
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
        if(gameMoment<1){gameMoment=1;}
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
