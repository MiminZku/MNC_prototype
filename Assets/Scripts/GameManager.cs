using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ================================
// <GameManager class>
// - 싱글톤으로 구현
//
// -> UI 관리
// -> gmae process 진행
// 
// ================================

public class GameManager : MonoBehaviour
{
    // <- ^ _ ->
    [SerializeField] Text movePath;
    [SerializeField] int boardRow = 6;
    [SerializeField] int boardCol = 3;
    [SerializeField] GameObject tiles;
    // 실제로는 board 이용

    public Tile[,] board;

    

    // Start is called before the first frame updates
    void Start()
    {
        
        // 부모 오브젝트 tiles를 통해서 tile을 가져와서 board에 넣어주기
        board = new Tile[boardRow, boardCol];
        for(int i = 0; i < boardRow; i++)
        {
            for(int j = 0; j < boardCol; j++)
            {
                Transform child = tiles.transform.GetChild(i*boardCol + j);
                board[i, j] = child.GetComponent<Tile>();
            }
        }
        // check
        // for(int i = 0; i < boardRow; i++)
        // {
        //     for(int j = 0; j < boardCol; j++)
        //     {
        //         Debug.Log("오브젝트 " + (i*boardCol + j));
        //         Debug.Log(board[i,j].transform.position);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
