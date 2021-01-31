using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotManager : MonoBehaviour
{
    private bool hasBug = false;
    public void setBug(bool state){
        hasBug = state;
    }
    public bool getBug(){
        return hasBug;
    }
}
