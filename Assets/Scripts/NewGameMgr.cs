using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public enum BattleState
{
    Start,
    Input,
    SetObstacle,
    Move,
    Finish
}


public class NewGameMgr : MonoBehaviourPunCallbacks
{
    Dice dice;
    int mainTurnNum = 6;
    int currentTurn = 1;
    BattleState state;
    GameObject myPlayerObject;
    NewPlayer myPlayer;
    public Transform[] spawnPositions;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        SpanwPlayer();
    }

    private void SpanwPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];
        myPlayerObject = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
        myPlayer = myPlayerObject.GetComponent<NewPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        GameProcess();

    }

    private void GameProcess()
    {
        switch (state)
        {
            case BattleState.Start:

                state = BattleState.Input;
                break;
            case BattleState.Input:
                // �ֻ��� ������
                dice.RollDice();
                // �ֻ��� �� �� UI�� ǥ��
                ShowDiceNum();
                // ��ֹ�, �̵� ���� UI ǥ��
                ShowSelectUI();
                // UI�� ��ֹ� �������� �̵��Ұ��� �Է� ����

                // �Է¿� ���� �̵�, ��ֹ� ��ġ �Է� �Լ� ����
                // �̵� �������� ���, �÷��̾�� �̵� �Է� ����
                // ��ֹ� ��ġ �������� ���, �÷��̾�� ��ֹ� ��ġ �Է� ����
                if(myPlayer.isObstacleSelected)
                {
                    myPlayer.InputObstacle();
                }
                else
                {
                    myPlayer.InputMove();
                }

                // ��Ʈ��ũ�� ��� �÷��̾ �Է��� �Ϸ� �ƴ��� Ȯ��
                if (EveryPlayerReady())
                {
                    state = BattleState.SetObstacle;
                }

                break;
            case BattleState.SetObstacle:
                // ���ӸŴ����� Ÿ�ϵ��� Ž���ؼ� ��ֹ� �÷��װ� �ִ� Ÿ�Ͽ� ��ֹ� ������ ��ġ
                
                break;
            case BattleState.Move:
                // �̵� �Է¿� ���� �÷��̾��  �̵�
                // ������ ���̸� ���� Finish�� ����
                break;
            case BattleState.Finish:
                // ���� ����
                break;
        }
    }

    private bool EveryPlayerReady()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            Hashtable cp = player.CustomProperties;
            Debug.Log(cp["isMoveReady"]);
            if (!(bool)cp["isMoveReady"]) return false;
        }
        return true;
    }

    private void ShowSelectUI()
    {
        throw new NotImplementedException();
    }

    private void ShowDiceNum()
    {
        throw new NotImplementedException();
    }


    
}
