using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// ================================
// <GameManager class>
// - 싱글톤으로 구현
//
// -> UI 관리
// -> gmae process 진행
// 
// ================================

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance
    {
        get
        {
            if(instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }
    private static GameManager instance;

    // <- ^ _ ->
    //[SerializeField] Text movePath;
    [SerializeField] int boardRow = 6;
    [SerializeField] int boardCol = 3;
    [SerializeField] GameObject tiles;
    // 실제로는 board 이용

    public Tile[,] board;

    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    public bool isPlayersMoving;


    // Start is called before the first frame updates
    void Start()
    {
        //// 부모 오브젝트 tiles를 통해서 tile을 가져와서 board에 넣어주기
        //board = new Tile[boardRow, boardCol];
        //for(int i = 0; i < boardRow; i++)
        //{
        //    for(int j = 0; j < boardCol; j++)
        //    {
        //        Transform child = tiles.transform.GetChild(i*boardCol + j);
        //        board[i, j] = child.GetComponent<Tile>();
        //    }
        //}
        // check
        // for(int i = 0; i < boardRow; i++)
        // {
        //     for(int j = 0; j < boardCol; j++)
        //     {
        //         Debug.Log("오브젝트 " + (i*boardCol + j));
        //         Debug.Log(board[i,j].transform.position);
        //     }
        // }
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length < 2) return;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            Hashtable cp = player.CustomProperties;
            Debug.Log(cp["isMoveReady"]);
            if (!(bool)cp["isMoveReady"]) return;
        }
        if (isPlayersMoving) return;
        isPlayersMoving = true;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Player>().MoveBoth();
        }
    }

    public override void OnLeftRoom() // 방 나가질 때 자동 실행(내가 나갈 경우에만)
    {
        SceneManager.LoadScene("Lobby");
    }
}
