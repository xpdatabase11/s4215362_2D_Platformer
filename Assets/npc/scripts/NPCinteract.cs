using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCinteract : MonoBehaviour
{
    public GameObject npc_screen;
    public RectTransform text_trans;
    public GameObject text_obj;
    public GameObject cam_pos;
    private Collider2D dest_collider;
    private float cam_pos_z_offset;
    private TextMeshProUGUI text;
    private string use_tutorial_lines;
    private int line_number = 0;
    private string intro;
    public GameObject interact_sign;
    private bool interacted = false;
    private bool collided_with_player = false;
    public GameObject player;
    public player_move player_move_script;
    public bool interact_button;
    public GameObject interact_sign_button;
    private bool using_android;
    

    public bool interact;

    // Start is called before the first frame update
    void Start()
    {
        text_trans = text_obj.GetComponent<RectTransform>();
        npc_screen.gameObject.SetActive(false);
        interact_sign_button.SetActive(false);

        text = text_obj.GetComponent<TextMeshProUGUI>();

        player_move_script = player.GetComponent<player_move>();

    }
    

    private void TutorialLines()
    {
        

        List<String> tutorial_lines = new List<String>();

        if (using_android)
        {
            tutorial_lines.Clear();
            tutorial_lines.Add("Hey, there friend. I am here to teach you all you need to know to survive.");
            tutorial_lines.Add("You can move by using A or D to move left or right.");
            tutorial_lines.Add("To jump, just press Space.");
            tutorial_lines.Add("Use your mouse to aim your gun.");
            tutorial_lines.Add("Press MOUSE 1 to shoot.");
        }
        else if (!using_android)
        {
            tutorial_lines.Clear();
            tutorial_lines.Add("Hey, there friend. I am here to teach you all you need to know to survive.");
            tutorial_lines.Add("Use the Joystick on the left side of the screen to move.");
            tutorial_lines.Add("To jump, push the joystick upwards.");
            tutorial_lines.Add("Tap the screen to shoot towards the direction.");
            tutorial_lines.Add("That is all.");
        }



        use_tutorial_lines = tutorial_lines[line_number];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interact_sign.SetActive(true);
        interact_sign_button.SetActive(true);


        MoveNPCScreen(collision);
        
        dest_collider = collision;
        cam_pos_z_offset = npc_screen.transform.position.z + 5;



    }

    private void Update()
    {

        if (!using_android && Input.GetKeyDown(KeyCode.F1))
        {
            using_android = true;


        }
        else if (using_android && Input.GetKeyDown(KeyCode.F1))
        {
            using_android = false;


        }



        if (line_number == 6)
        {
            interact_sign.SetActive(false);
            interacted = false;
            text_trans.position = new Vector3(-1000, 0, 0);
            npc_screen.gameObject.SetActive(false);
            interact_sign.SetActive(true);


            line_number = 0;
            player_move_script.move_allowed = 1;


        }


        if (Input.GetKeyDown(KeyCode.E) | interact_button)
        {
            interact = true;
            interact_button = false;
        }
        else
        {
            interact = false;
            interact_button = false;
        }
        

        if (interact)
        {
            interacted = true;
            interact_sign.SetActive(false);
            line_number += 1;
            text.SetText(use_tutorial_lines);
        }

        npc_screen.transform.position = new Vector3 (cam_pos.transform.position.x, cam_pos.transform.position.y, -30f);
        if (dest_collider != null)
        {
            MoveNPCScreen(dest_collider);
            TutorialLines();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interact_sign.SetActive(false);
        interact_sign_button.SetActive(false);
        interacted = false;

        text_trans.position = new Vector3(-1000, 0, 0);
        npc_screen.gameObject.SetActive(false);

        collided_with_player = false;
        line_number = 0;
        player_move_script.move_allowed = 1;

    }

    private void MoveNPCScreen(Collider2D collision)
    {
        print(collision.gameObject.tag);


        if (collision.gameObject.tag == "Player")
        {
            collided_with_player = true;

        }
        if (interacted && collided_with_player)
        {
            player_move_script.move_allowed = 0;
            text_trans.position = new Vector3(400, 400, 0);
            npc_screen.gameObject.SetActive(true);
            interact_sign.SetActive(false);
        }

    }

    public void InteractButton()
    {
        interact_button = true;
    }
}
