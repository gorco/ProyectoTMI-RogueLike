using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsswitch : MonoBehaviour {

    private Light light;
    private float maxRange;
    float time = 0;
	// Use this for initialization
	void Start () {
       light = gameObject.GetComponent<Light>();
       maxRange = light.range;
	}
	



	// Update is called once per frame
	void Update () {
        if (time < 30)
        {
            Debug.Log("Aqui");
            if (light.range > maxRange)
            {
                light.range -= 0.3f;
                time -= Time.deltaTime;
            }

            else
            {
                light.range += 0.3f;
                time -= Time.deltaTime;
            }

        }



       
	}
}
