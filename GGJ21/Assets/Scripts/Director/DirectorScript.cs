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
    public RoundState currentRound;
    private TimerScript tScript;
    private bool hasPunished;

    //internal timer
    public int secretsFound;
    public int initialSecrets;

    GameObject seeker;
    GameObject hider;
    private HiderInteractions hScript;

    public Text timerText;
    public Image timerVisual;


//This script is for handling most events. Current round is stored here, but changed in timer.
//Attach this and TimerScript to same GameObject.
//Times are publically set here, but managed in timer.
//Amount of time remaining and time consumed in set up are stored in timer.

/** TODO:
Create array to hold objects that have been bugged
highlight bugged objects for hider for eventual multiplayer
**/
    void Start(){
        hasPunished=false;
        seeker = GameObject.FindGameObjectWithTag("SeekerPlayer");
        hider = GameObject.FindGameObjectWithTag("HiderPlayer");
        Debug.Log(hider);
        hScript = hider.GetComponent<HiderInteractions>();
        tScript = gameObject.GetComponent<TimerScript>();
        currentRound = RoundState.SetUp;
        seeker.SetActive(false);
    }

    void Update(){
        timerText.text = Mathf.Ceil(tScript.timeRemaining).ToString();
        //set up bugs
        if (currentRound==RoundState.SetUp){
            tScript.setUpReady = hScript.secretAmount>0;
            hScript.isSetUp = true;
            timerFill(setUpTime);
        }else if(currentRound==RoundState.Prep){
            tScript.prepReady = hScript.secretAmount!=0;
            timerFill(prepTime);
            //handle not hiding lives and punish
        }else{
            seeker.SetActive(true);
            punish();
        }
        //game time
        if(currentRound==RoundState.Game){
            hScript.isSetUp=false;
            timerFill(gameTime);
            if (tScript.timeRemaining>0){
                if(initialSecrets==secretsFound){
                    seekerWins();
                }
            }else{
                if(hScript.secretAmount == 0){
                    hiderWins();
                }else{
                    seekerWins();
                }
            }
        }
    }
    private void punish(){
        //punish only once
        if(!hasPunished){
            if(hScript.secretAmount>0){
                //Set max secrets to amount hidden already
                secretsFound = hScript.secretAmount-1;
                hScript.secretAmount = 1;
            }
        }
        hasPunished=true;
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
        timerVisual.fillAmount = tScript.timeRemaining / gameMoment;
        if(timerVisual.fillAmount<= .25f)
        {
            timerVisual.color = Color.red;
        }
        else
        {
            timerVisual.color = Color.green;
        }
    }

    public void foundSecret(){
        secretsFound++;
    }
}
