using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.TestTools;
using Unity.VisualScripting;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine.Rendering;

public class PlayerCam : MonoBehaviour
{


    private Vector2 move_axis;
    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Cursor.visible = false;
    }

}
