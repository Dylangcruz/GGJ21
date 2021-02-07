using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotManager : MonoBehaviour
{
    private bool hasSecret = false;
    public void setSecret(bool state){
        hasSecret = state;
    }
    public bool getSecret(){
        return hasSecret;
    }
}
