using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UsingAndroid : MonoBehaviour
{
    private bool using_android = false;
    public GameObject hud_android;
    public bool inverse = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inverse)
        {
            if (using_android)
            {
                hud_android.SetActive(true);
            }
            else
            {
                hud_android.SetActive(false);
            }


            if (!using_android && Input.GetKeyDown(KeyCode.F1))
            {
                using_android = true;


            }
            else if (using_android && Input.GetKeyDown(KeyCode.F1))
            {
                using_android = false;


            }

        }
        if (inverse)
        {
            if (using_android)
            {
                hud_android.SetActive(false);
            }
            else
            {
                hud_android.SetActive(true);
            }


            if (!using_android && Input.GetKeyDown(KeyCode.F1))
            {
                using_android = true;


            }
            else if (using_android && Input.GetKeyDown(KeyCode.F1))
            {
                using_android = false;


            }




            print(using_android);
        }
    }
}
