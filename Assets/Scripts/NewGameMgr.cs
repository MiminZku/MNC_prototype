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
    CheckInput,
    SetObstacle,
    Move,
    Finish
}

public class NewGameMgr : MonoBehaviourPunCallbacks
{
    [SerializeField] Dice dice;
    int mainTurnNum = 6;
    int currentTurn = 1;
    BattleState state;
    GameObject myPlayerObject;
    NewPlayer myPlayer;
    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    public Text diceText;
    public bool isProgressing = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        SpanwPlayer();
    }
    // Update is called once per frame
    void Update()
    {
        GameProcess();
    }
    private void SpanwPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];
        myPlayerObject = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
        myPlayer = myPlayerObject.GetComponent<NewPlayer>();
    }

    private void GameProcess()
    {
        switch (state)
        {
            case BattleState.Start:

                state = BattleState.Input;
                break;
            case BattleState.Input:
                if (isProgressing) break;
                isProgressing = true;
                // 주사위 굴리기
                dice.RollDice();
                // 주사위 눈 수 UI에 표시
                ShowDiceNum();
                // 장애물, 이동 선택 UI 표시
                ShowSelectUI();
                // UI로 장애물 놓을건지 이동할건지 입력 받음

                // 입력에 따라 이동, 장애물 설치 입력 함수 실행
                // 이동 선택했을 경우, 플레이어에서 이동 입력 받음
                // 장애물 설치 선택했을 경우, 플레이어에서 장애물 설치 입력 받음
                if(myPlayer.isObstacleSelected)
                {
                    myPlayer.InputObstacle();
                }
                else
                {
                    myPlayer.InputMove();
                }
                isProgressing = false;
                state = BattleState.CheckInput;
                break;
            case BattleState.CheckInput:
                // 네트워크에 모든 플레이어가 입력이 완료 됐는지 확인
                if (EveryPlayerReady())
                {
                    state = BattleState.SetObstacle;
                }
                break;
            case BattleState.SetObstacle:
                // 게임매니저의 타일들을 탐색해서 장애물 플래그가 있는 타일에 장애물 실제로 설치
                SetObstaclesUp();
                
                break;
            case BattleState.Move:
                // 이동 입력에 따라서 플레이어에서  이동
                // 마지막 턴이면 상태 Finish로 변경
                myPlayer.Move();
                if(currentTurn == mainTurnNum)
                {
                    state = BattleState.Finish;
                }
                break;
            case BattleState.Finish:
                // 승패 판정
                break;
        }
    }

    private void SetObstaclesUp()
    {
        state = BattleState.Move;
    }

    private bool EveryPlayerReady()
    {
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            Hashtable cp = player.CustomProperties;
            Debug.Log(cp["isInputDone"]);
            if (!(bool)cp["isInputDone"]) return false;
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
