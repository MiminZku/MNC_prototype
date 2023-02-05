using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Material[] materials;
    MeshRenderer meshRenderer;
    Animator animator;

    public bool isObastacleSet; // ��ֹ��� ��ġ�� Ÿ������
    public bool[] isSelected;   // ���� ���� ���������� bool[0] = true, ���� ���� -> bool[1] = true
                                // �Ѵ� true�� ��ġ�� �Ŵϱ� �� �ȹٲ��
    int boolIndex;  // isSelected �迭 �����Ҷ� ���
    public int whose = 0; // �߸��̸� 0, �����̸� 1, �����̸� 2 -> ���� ������ �� ���

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