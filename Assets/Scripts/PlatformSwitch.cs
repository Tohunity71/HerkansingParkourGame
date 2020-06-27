using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{

    public GameObject bluePlatform;
    public GameObject orangePlatform;
    public GameObject[] orange;
    public GameObject[] blue;
    private bool platformSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        //Orange settings
        //Vult Array[orange] met alle game objecten met de tag "orange"
        orange = GameObject.FindGameObjectsWithTag("orange");

        // Voor elk game object in Array[orange] maakt hij ze "false"
        foreach (GameObject orangeObjects in orange)
        {
            orangeObjects.SetActive(false);
        }


        // blue settings
        //Vult Array[orange] met alle game objecten met de tag "orange"
        blue = GameObject.FindGameObjectsWithTag("blue");

        // Voor elk game object in Array[orange] maakt hij ze "false"
        foreach (GameObject blueObjects in blue)
        {
            blueObjects.SetActive(true);
        }

    }
    // Update is called once per frame
    void Update()
    {
        //BluePlatform.GetComponent<GameObject>();

        if (platformSwitch == false && Input.GetKeyDown(KeyCode.E))
        {
            platformSwitch = true;

                foreach (GameObject blueObjects in blue)
                {
                blueObjects.SetActive(platformSwitch);
                }

                foreach (GameObject orangeObjects in orange)
            {
                orangeObjects.SetActive(!platformSwitch);

            }
        }
        else if (platformSwitch == true && Input.GetKeyDown(KeyCode.E))
        {
            platformSwitch = false;

                foreach (GameObject blueObjects in blue)
                {
                    blueObjects.SetActive(platformSwitch);
                }

                foreach (GameObject orangeObjects in orange)
            {
                orangeObjects.SetActive(!platformSwitch); // ! zorgt ervoor dat het NIET platform switch is/ false is
            }
        }
        else
        {
            return;
        }

    }
}
