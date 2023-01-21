using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDayCycle : MonoBehaviour
{

    public bool enableCycle = true;
    [Range(0.0f, 20f)]
    public float cycleSpeed = 10;
    private float updateTimeInterval;
    public GameObject dayLight, nightLight;

    // Start is called before the first frame update
    void Start()
    {
        if(enableCycle){
            dayLight = GameObject.Find("DayLight");
            //nightLight = GameObject.Find("NightLight");
            dayLight.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            //nightLight.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            StartCoroutine(DayCycle());
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateTimeInterval = cycleSpeed/10;
    }

    IEnumerator DayCycle(){
        
            while(true){
                dayLight.transform.Rotate(new Vector3(cycleSpeed*Time.deltaTime,0,0));
                yield return new WaitForSeconds(updateTimeInterval);
            }
    }
}
