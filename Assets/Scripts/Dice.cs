using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int diceNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RollDice()
    {
        diceNum = Random.Range(0, 7);
        Debug.Log(string.Format("dice : {0}",diceNum));
        return diceNum;
    }
}
