using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI moneyDisp;
    public int score=0;

    public int deliveriesMissed=0;

    public int money=1000;
    public bool gameOver=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text="Score : "+score;
        moneyDisp.text="Money : "+money;
    }
}
