using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform camera_transform;
    private GameObject player;
    public Rigidbody player_body;
    public float cam_offset_mult = 1.0f;
    private Vector3 cam_destination;
    private Vector3 cam_destination2;
    private Vector3 cam_final_dest;
    public int interp_frames_count;
    int elapsed_frames;

    void Start()
    {
        player = GameObject.Find("Player");
        player_body = player.GetComponent<Rigidbody>();
        cam_offset_mult *= 0.02f;
    
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 cam_dest_offset = (cam_destination - cam_destination2);

        cam_dest_offset.x = Mathf.Abs(cam_dest_offset.x); // Get rid of negative X value
        cam_dest_offset.y = Mathf.Abs(cam_dest_offset.y); // Get rid of negative Y value
        cam_dest_offset.z = Mathf.Abs(cam_dest_offset.z); // Get rid of negative Z value


        float cam_dest_offset_all = cam_dest_offset.magnitude / 3 * 100;
        Mathf.Abs(cam_dest_offset_all);
        //Debug.Log($"{cam_dest_offset_all}");
        float interp_ratio = (Time.deltaTime / interp_frames_count * 2);
        
        cam_destination = transform.position;
        cam_destination2 = camera_transform.position;
        
        elapsed_frames = (elapsed_frames + 1) % (interp_frames_count + 1);

        cam_final_dest = Vector3.Lerp(cam_destination, cam_destination2, interp_ratio * cam_dest_offset_all );

        

        transform.position = cam_final_dest;
        //transform.position = cam_final_dest;



    }
}

