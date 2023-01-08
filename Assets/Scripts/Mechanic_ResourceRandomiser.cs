using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mechanic_ResourceRandomiser : MonoBehaviour
{
    public GameObject[] resourceDepo;
    private GameObject tempR;
    public Sprite[] image;

    void Start()
    {
        Shuffle();        
    }

    public void Shuffle()
    {
        for (int i = 0; i < resourceDepo.Length; i++)
        {
            tempR = resourceDepo[i];
            int rand = Random.Range(i, resourceDepo.Length);
            resourceDepo[i] = resourceDepo[rand];
            resourceDepo[rand] = tempR;
        }
        Assign();
    }

    private void Assign()
    {
        resourceDepo[0].tag = "CottonResource";
        resourceDepo[0].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = image[0];
        resourceDepo[1].tag = "CottonResource";
        resourceDepo[1].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = image[0];
        resourceDepo[2].tag = "IronResource";
        resourceDepo[2].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = image[1];
        resourceDepo[3].tag = "IronResource";
        resourceDepo[3].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = image[1];
        resourceDepo[4].tag = "SpiceResource";
        resourceDepo[4].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = image[2];
        resourceDepo[5].tag = "SpiceResource";
        resourceDepo[5].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = image[2];
    }
}
