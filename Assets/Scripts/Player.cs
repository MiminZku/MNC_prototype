using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ================================
// <Player class>
//
// -> 이동을 위한 입력값 받음
// -> 실제 이동 구현
// ================================
public class Player : MonoBehaviour
{
    
    public Tile[,] board;
    // 현재 위치 인덱스
    int[] nowIndex = new int[2];
    // flags
    bool isPossibleMove = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(isPossibleMove)
        {   
            float horizon = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if(horizon > 0)
            {
                // 오른쪽으로 이동
            }
            else
            {
                // 왼쪽
            }
            if(vertical > 0)
            {
                // 위로 
            }
            else
            {
                // 아래로 
            }
        }
    }
}
