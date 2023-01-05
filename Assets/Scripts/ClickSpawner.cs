using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickSpawner : MonoBehaviour
{
    public GameObject[] prefabs; //Prefabs to spawn

    public Canvas canvas;

    public Canvas speedController;

    [SerializeField] ScoreCounter sc;

    Camera c;
    int selectedPrefab = 0;
    int rayDistance = 300;

    // Start is called before the first frame update
    void Start()
    {
        // Releases the cursor
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.None;
        // // Locks the cursor
        // Cursor.lockState = CursorLockMode.Locked;
        // // Confines the cursor
        // Cursor.lockState = CursorLockMode.Confined;

        c = GetComponent<Camera>();
        if(prefabs.Length == 0)
        {
            Debug.LogError("You haven't assigned any Prefabs to spawn");
        }
        canvas.enabled=false;
        speedController.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedPrefab++;
            if(selectedPrefab >= prefabs.Length)
            {
                selectedPrefab = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedPrefab--;
            if (selectedPrefab < 0)
            {
                selectedPrefab = prefabs.Length - 1;
            }
        }
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            //Remove spawned prefab when holding left shift and left clicking
            Transform selectedTransform = GetObjectOnClick();
            if (selectedTransform)
            {
                sc.score+=5;
                Destroy(selectedTransform.gameObject);
            }
        }
        else if (Input.GetMouseButtonDown(0)&&Input.GetKey(KeyCode.LeftControl))
        {
            //On left click spawn selected prefab and align its rotation to a surface normal
            Vector3[] spawnData = GetClickPositionAndNormal();

            Transform clickedObject=GetObject();

            Debug.Log(spawnData);
            if(spawnData[0] != Vector3.zero)
            {
                if(clickedObject.tag=="Ground"&&clickedObject.tag!="RailTrack")
                {
                    sc.score-=5;
                    GameObject go = Instantiate(prefabs[selectedPrefab], spawnData[0], prefabs[selectedPrefab].transform.rotation);
                    go.name += " _instantiated";
                }
                
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            Transform clickedObject=GetObject();
            Debug.Log(clickedObject.tag);
            if(clickedObject.tag=="Junction")
            {
                canvas.enabled=true;
                Slider slider=GameObject.FindWithTag("Slider").GetComponent<Slider>();
                slider.value=clickedObject.GetComponent<JunctionController>().currentActive;
                Invoke("resetSlider",3.0f);
            }
            if(clickedObject.tag=="train"||clickedObject.tag=="wagon")
            {
                Debug.Log(clickedObject.transform.parent.gameObject.tag);
                SpeedController sp=clickedObject.transform.parent.gameObject.GetComponent<SpeedController>();
                speedController.enabled=true;
                Slider slider=GameObject.FindWithTag("SliderSpeed").GetComponent<Slider>();
                //Debug.Log(slider.value);
                sp.function(slider,clickedObject.GetComponent<locomotive>().speed);
            }
        }
        
        // else if(Input.GetMouseButtonDown(0))
        // {
        //     Transform clickedObject=GetObject();
        //     if(clickedObject.tag!="Junction")
        //     {
        //         canvas.enabled=false;
        //     }
        // }
    }

    void resetSlider()
    {
        canvas.enabled=false;
    }

    Vector3[] GetClickPositionAndNormal()
    {
        Vector3[] returnData = new Vector3[] { Vector3.zero, Vector3.zero }; //0 = spawn poisiton, 1 = surface normal
        Ray ray = c.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            returnData[0] = hit.point;
            returnData[1] = hit.normal;
        }

        return returnData;
    }

    Transform GetObject()
    {
        Ray ray = c.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.transform;
        }
        return null;
    }

    Transform GetObjectOnClick()
    {
        Ray ray = c.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Transform root = hit.transform.root;
            if (root.name.EndsWith("_instantiated"))
            {
                return root;
            }
        }

        return null;
    }

    void OnGUI()
    {
        if(prefabs.Length > 0 && selectedPrefab >= 0 && selectedPrefab < prefabs.Length)
        {
            string prefabName = prefabs[selectedPrefab].name;
            GUI.color = new Color(0, 0, 0, 0.84f);
            GUI.Label(new Rect(5 + 1, 5 + 1, 200, 25), prefabName);
            GUI.color = Color.green;
            GUI.Label(new Rect(5, 5, 200, 25), prefabName);
        }  
    }
}
