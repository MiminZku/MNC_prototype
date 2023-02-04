using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

// ================================
// <Player class>
//
// -> 이동을 위한 입력값 받음
// -> 실제 이동 구현
// ================================
public class Player : MonoBehaviourPun
{
    
    public Tile[,] board;
    // 현재 위치 인덱스
    [SerializeField] int[] curIndex = new int[2];
    [SerializeField] int[] orgIndex = new int[2];

    private MeshRenderer meshRenderer;
    Vector3 currentPosition;
    public Material player1Mat;
    public Material player2Mat;
    [HideInInspector] public enum Move
    {
        None,
        Up,
        Left,
        Down,
        Right
    }
    public Move[] moves;
    public bool isMoveReady;
    int moveCount = 0;
    public bool isMoving;

    void Start()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isMoveReady", false } });
        GetComponent<PhotonTransformView>().m_SynchronizePosition = false;
        currentPosition = gameObject.transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        if (photonView.IsMine)
        {
            meshRenderer.material = player1Mat;
        }
        else
        {
            meshRenderer.material = player2Mat;
        }
        moves = new Move[2];

        if(currentPosition.z == 1.5)
        {
            SetIndex(0);
        }
        else if(currentPosition.z == 4.5)
        {
            SetIndex(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) { return; }
        if (isMoving) { return; }
        if (moveCount == moves.Length)
        {
            if(Input.GetKeyDown(KeyCode.Return))    // 엔터키
            {
                ReadyToMove();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (curIndex[0] == 0) { return; }
                curIndex[0] -= 1;
                photonView.RPC("InputMoveRPC", RpcTarget.All, 1);
                transform.Translate(new Vector3(0, 0, 1));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (curIndex[1] == 0) { return; }
                curIndex[1] -= 1;
                photonView.RPC("InputMoveRPC", RpcTarget.All, 2);
                transform.Translate(new Vector3(-1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (curIndex[0] == 5) { return; }
                curIndex[0] += 1;
                photonView.RPC("InputMoveRPC", RpcTarget.All, 3);
                transform.Translate(new Vector3(0, 0, -1));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (curIndex[1] == 2) { return; }
                curIndex[1] += 1;
                photonView.RPC("InputMoveRPC", RpcTarget.All, 4);
                transform.Translate(new Vector3(1, 0, 0));
            }
        }
    }

    [PunRPC]
    public void InputMoveRPC(int move)
    {
        if(move == 1)
        {
            moves[moveCount++] = Move.Up;
        }
        if (move == 2)
        {
            moves[moveCount++] = Move.Left;
        }
        if (move == 3)
        {
            moves[moveCount++] = Move.Down;
        }
        if (move == 4)
        {
            moves[moveCount++] = Move.Right;
        }
    }

    public void ReadyToMove()
    {
        isMoveReady = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isMoveReady", true } });
        transform.position = currentPosition;
        curIndex = (int[])orgIndex.Clone();

    }

    public void MoveBoth()
    {
        photonView.RPC("MoveBothRPC",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void MoveBothRPC()
    {
        isMoving = true;
        //GetComponent<PhotonTransformView>().m_SynchronizePosition = true;
        StartCoroutine("MoveDelay");
    }

    IEnumerator MoveDelay()
    {
        //if (!photonView.IsMine)
        //{
        //    yield break;
        //}
        isMoveReady = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isMoveReady", false } });
        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i] == Move.None) { continue; }
            switch (moves[i])
            {
                case Move.Up:
                    transform.Translate(new Vector3(0, 0, 1));
                    curIndex[0] -= 1;
                    GameManager.Instance.board[curIndex[0], curIndex[1]].Flip(photonView.IsMine ? 1 : 2);
                    break;
                case Move.Left:
                    transform.Translate(new Vector3(-1, 0, 0));
                    curIndex[1] -= 1;
                    GameManager.Instance.board[curIndex[0], curIndex[1]].Flip(photonView.IsMine ? 1 : 2);
                    break;
                case Move.Down:
                    transform.Translate(new Vector3(0, 0, -1));
                    curIndex[0] += 1;
                    GameManager.Instance.board[curIndex[0], curIndex[1]].Flip(photonView.IsMine ? 1 : 2);
                    break;
                case Move.Right:
                    transform.Translate(new Vector3(1, 0, 0));
                    curIndex[1] += 1;
                    GameManager.Instance.board[curIndex[0], curIndex[1]].Flip(photonView.IsMine ? 1 : 2);
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        orgIndex = (int[])curIndex.Clone();
        moveCount = 0;
        moves = new Move[2];
        currentPosition = transform.position;
        isMoving = false;
        //GetComponent<PhotonTransformView>().m_SynchronizePosition = false;
        //GameManager.Instance.isPlayersMoving = false;
    }
    public void SetIndex(int spawnPos)
    {
        photonView.RPC("SetIndexRPC", RpcTarget.AllBuffered, spawnPos);
    }

    [PunRPC]
    public void SetIndexRPC(int spawnPos)
    {
        if(spawnPos == 0)
        {
            curIndex = new int[] { 4, 1 };
        }
        else
        {
            curIndex = new int[] { 1, 1 };
        }
        GameManager.Instance.board[curIndex[0], curIndex[1]].Flip(photonView.IsMine ? 1 : 2);
        orgIndex = (int[])curIndex.Clone();
    }
}
