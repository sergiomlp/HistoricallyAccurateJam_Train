using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public int FactoryType=1;

    //1 for canned
    //2 for cloth
    //3 for steel

    public int spiceCount=0;
    public int cottonCount=0;
    public int ironCount=0;

    public int level=1;

    public int currentNoOfDeliveries=0;

    public int missedDeliveries=0;

    public float moneyToBeGiven;

    [SerializeField] ScoreCounter sc;

    TrainResourceManager trm;

    int quantityReceived=0;

    public float currentTimer=45.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time has run out for this factory!");
            missedDeliveries++;
            spiceCount=0;
            ironCount=0;
            cottonCount=0;

            if(level==1)
            {
                currentTimer=45.0f;
            } else if(level==2) {
                currentTimer=30.0f;
            } else if(level==3) {
                currentTimer=30.0f;
            } else if(level==4) {
                currentTimer=25.0f;
            } else if(level==5) {
                currentTimer=25.0f;
            } 
        }

        if(missedDeliveries>=5)
        {
            sc.score-=1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="train")
        {
            trm=other.GetComponent<TrainResourceManager>();
            switch(FactoryType)
            {
                case 1:
                    if(level==1)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>0)
                        {
                            level=2;
                            spiceCount=0;
                            currentTimer=30.0f;
                            giveMoney(45.0f);
                        }
                    }
                    else if(level==2)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>0)
                        {
                            level=3;
                            spiceCount=0;
                            currentTimer=30.0f;
                            giveMoney(30.0f);
                        }
                    }
                    else if(level==3)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>2&&ironCount>1)
                        {
                            level=4;
                            spiceCount=0;
                            ironCount=0;
                            currentTimer=25.0f;
                            giveMoney(30.0f);
                        }
                    }
                    else if(level==4)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>2&&ironCount>1)
                        {
                            level=5;
                            spiceCount=0;
                            ironCount=0;
                            giveMoney(25.0f);
                        }
                    }
                    else if(level==5)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>2&&ironCount>1&&cottonCount>1)
                        {
                            level=5;
                            spiceCount=0;
                            ironCount=0;
                            cottonCount=0;
                            currentTimer=25.0f;
                            giveMoney(25.0f);
                        }
                    }
                break;

                case 2:
                    if(level==1)
                    {
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(cottonCount>0)
                        {
                            level=2;
                            cottonCount=0;
                            currentTimer=30.0f;
                            giveMoney(45.0f);
                        }
                    }
                    else if(level==2)
                    {
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(cottonCount>0)
                        {
                            level=3;
                            cottonCount=0;
                            currentTimer=30.0f;
                            giveMoney(30.0f);
                        }
                    }
                    else if(level==3)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>1&&cottonCount>2)
                        {
                            level=4;
                            spiceCount=0;
                            cottonCount=0;
                            currentTimer=25.0f;
                            giveMoney(30.0f);
                        }
                    }
                    else if(level==4)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>1&&cottonCount>2)
                        {
                            level=5;
                            spiceCount=0;
                            cottonCount=0;
                            giveMoney(25.0f);
                        }
                    }
                    else if(level==5)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>2&&ironCount>1&&cottonCount>2)
                        {
                            level=5;
                            spiceCount=0;
                            ironCount=0;
                            cottonCount=0;
                            currentTimer=25.0f;
                            giveMoney(25.0f);
                        }
                    }
                break;

                case 3:
                    if(level==1)
                    {
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(ironCount>0)
                        {
                            level=2;
                            ironCount=0;
                            currentTimer=30.0f;
                            giveMoney(45.0f);
                        }
                    }
                    else if(level==2)
                    {
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(ironCount>0)
                        {
                            level=3;
                            ironCount=0;
                            currentTimer=30.0f;
                            giveMoney(30.0f);
                        }
                    }
                    else if(level==3)
                    {
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(ironCount>1&&cottonCount>1)
                        {
                            level=4;
                            ironCount=0;
                            cottonCount=0;
                            currentTimer=25.0f;
                            giveMoney(30.0f);
                        }
                    }
                    else if(level==4)
                    {
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(ironCount>2&&cottonCount>1)
                        {
                            level=5;
                            ironCount=0;
                            cottonCount=0;
                            currentTimer=25.0f;
                            giveMoney(25.0f);
                        }
                    }
                    else if(level==5)
                    {
                        if(trm.spice>0)
                        {
                            spiceCount++;
                            trm.spice--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.iron>0)
                        {
                            ironCount++;
                            trm.iron--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(trm.cotton>0)
                        {
                            cottonCount++;
                            trm.cotton--;
                            trm.quantity--;
                            sc.score+=1;
                        }
                        if(spiceCount>1&&ironCount>2&&cottonCount>2)
                        {
                            level=5;
                            spiceCount=0;
                            ironCount=0;
                            cottonCount=0;
                            currentTimer=25.0f;
                            giveMoney(25.0f);
                        }
                    }

                break;
            }
        }
    }

    void giveMoney(float duration)
    {
        if(currentTimer>=duration/2)
        {
            sc.money+=1000;
        }
        else if(currentTimer<duration/2&&currentTimer>=duration/4)
        {
            sc.money+=500;
        }
        else
        {
            sc.money+=250;
        }
    }
}
