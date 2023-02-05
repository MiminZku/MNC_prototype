using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Material[] materials;
    MeshRenderer meshRenderer;
    Animator animator;

    public bool isObastacleSet; // 장애물이 설치된 타일인지
    public bool[] isSelected;   // 나에 의해 뒤집어지면 bool[0] = true, 적에 의해 -> bool[1] = true
                                // 둘다 true면 겹치는 거니까 색 안바뀌게
    int boolIndex;  // isSelected 배열 접근할때 사용
    public int whose = 0; // 중립이면 0, 내땅이면 1, 적땅이면 2 -> 승패 판정할 때 사용

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer= GetComponent<MeshRenderer>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flip(int i)
    {
        Debug.Log("Tile Fliped");
        meshRenderer.material = materials[i];
        animator.SetTrigger("Flip");
    }

    public void NewFlip(int i)
    {
        
    }
}