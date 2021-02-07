using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour{
    //default game settings
    public bool setUpReady;
    public bool prepReady;
    private DirectorScript dScript;

    //internal timer
    private float setUpRemaining;
    public float timeRemaining;

    void Start(){
        setUpReady = true;
        prepReady = false;
        dScript = gameObject.GetComponent<DirectorScript>();
        setUpRemaining = dScript.setUpTime;
        timeRemaining = dScript.setUpTime;
    }

    void Update(){
        //set up bugs
        if(dScript.currentRound==DirectorScript.RoundState.SetUp){
            setUpTimer(setUpReady);
        }else if(dScript.currentRound==DirectorScript.RoundState.Prep){
            prepTimer(prepReady);
        }else if(dScript.currentRound==DirectorScript.RoundState.Game){
            gameTimer();
        }
    }

    //Set up timer breaks if ready (All bugs are planted)
    private void setUpTimer(bool notReady){
        if((timeRemaining > 0) && notReady){
            //show that it is set up time
            //change lighting color :)
            timeRemaining -= Time.deltaTime;
            //cut to prep time
        }else{
            //Save remaining time
            setUpRemaining -= (dScript.setUpTime-timeRemaining);
            if (setUpRemaining<dScript.prepTime){setUpRemaining=0;}
                timeRemaining = dScript.prepTime;
                dScript.currentRound = DirectorScript.RoundState.Prep;
        }
    }
    //Prep timer breaks if unreadied (A bug has been picked up)
    private void prepTimer(bool notReady){
            if(timeRemaining>0){
                timeRemaining -= Time.deltaTime;
                //Take time away from saved remaining time according to time taken in prep
                if(setUpRemaining>dScript.prepTime && notReady){
                    setUpRemaining -= (dScript.prepTime-timeRemaining);
                    dScript.setUpTime = setUpRemaining;
                    timeRemaining = dScript.setUpTime;
                    dScript.currentRound=DirectorScript.RoundState.SetUp;
                }
            }else{
                dScript.currentRound=DirectorScript.RoundState.Game;
                timeRemaining = dScript.gameTime;
            }

    }
    //Regular game timer. Possible haste mode future functionality.
    private void gameTimer(){
        if (timeRemaining>0){
            timeRemaining -= Time.deltaTime;
        }
    }
}
