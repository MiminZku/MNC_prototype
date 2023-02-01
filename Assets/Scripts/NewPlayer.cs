using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NewPlayer : MonoBehaviourPun
{
    internal bool isObstacleSelected;



    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isInputDone", false } });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObstacleButtonClick()
    {
        isObstacleSelected = true;
    }

    public void OnMoveButtonClick()
    {
        isObstacleSelected = false;
    }

    internal void InputObstacle()
    {

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isInputDone", true } });
    }

    internal void InputMove()
    {

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isInputDone", true } });
    }
}
