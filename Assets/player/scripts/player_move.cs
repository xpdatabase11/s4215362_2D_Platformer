using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using Vector2 =  UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class player_move : MonoBehaviour
{
    private Vector2 move_axis;
    private Vector2 jump_axis;
    private float input_x_axis;
    private bool jump;
    private float jumping;
    private float jump_force;
    public float jump_force_mult;
    public Transform ground_check;
    private BoxCollider2D ground_check_coll;
    private CapsuleCollider2D player_collider;
    public LayerMask ground;
    public GameObject player_body;
    private Rigidbody2D player_rigid;
    public float move_force;
    private Animator player_animator;
    private float anim_speed;
    public float max_speed;
    public bool facing_right = true;
    private int char_is_moving;
    private float jump_dis_drag;

    public Joystick joystick;
    private float joystick_x_axis;
    private float joystick_y_axis;

    

    private bool is_walking;
    private bool is_scraping;

    public int move_allowed;

    private AudioSource footstep_sound;
    public AudioClip footstep_step;
    public AudioClip footstep_scrape;
    private float footstep_volume;


    void Start()
    {
        //player_body = GameObject.Find("Player_BODY");
        player_animator = player_body.GetComponent<Animator>();
        ground_check_coll = ground_check.GetComponent<BoxCollider2D>();
        player_collider = GetComponent<CapsuleCollider2D>();
        player_collider.size = new Vector2(0.5f, 1.0f);
        footstep_sound = GetComponent<AudioSource>();
        footstep_sound.volume = 0.0f;
    }


    private void Update()
    {
        PlayerMoveAxis();
    }


    private void FixedUpdate()
    {
        PlayerMove();
    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(ground_check_coll.bounds.center, ground_check_coll.bounds.size, 0.0f, Vector2.down, 0.1f, ground);
        
    }
    
    private void PlayerMoveAxis()
    {
        move_axis.x = input_x_axis;
        jump_axis.y = jump_force;
        move_axis.y = 0.0f;
        jump_axis.x = 0.0f;

        if(is_scraping && !footstep_sound.isPlaying)
        {
            is_scraping = false;
        }


        if (input_x_axis < 0.0f && player_body.transform.localScale.x >= 0.001f)
        {
            facing_right = false;
            player_body.transform.localScale *= new Vector2(-1.0f, 1.0f);
            
        }
        else if (input_x_axis > 0.0f && player_body.transform.localScale.x <= -0.001f)
        {
            facing_right = true;
            player_body.transform.localScale *= new Vector2(-1.0f, 1.0f);
        }
        Vector3 local_scale = player_body.transform.localScale;
        local_scale.z = 1.0f;
        
        if (facing_right && player_rigid.velocity.x < 0.0f && Mathf.Abs(input_x_axis) > 0.0f && IsGrounded() && !is_scraping)
        {
            player_rigid.drag = 5.0f * jump_dis_drag;
            footstep_sound.clip = footstep_scrape;
            is_scraping = true;
           if (footstep_sound.clip = footstep_scrape)
            {
                footstep_sound.PlayOneShot(footstep_scrape);
            } 
           
        }
        else if (!facing_right && player_rigid.velocity.x > 0.0f && Mathf.Abs(input_x_axis) > 0.0f && IsGrounded() && !is_scraping)
        {
            player_rigid.drag = 5.0f * jump_dis_drag;
            is_scraping = true;
            if (footstep_sound.clip = footstep_scrape)
            {
                footstep_sound.PlayOneShot(footstep_scrape);
            }
        }
        else
        {

            if (Mathf.Abs(input_x_axis) != 1.0f && IsGrounded())
            {
                
                player_rigid.drag = 3.0f * jump_dis_drag;
            }
            else
            {
                player_rigid.drag = 0.7f * jump_dis_drag;
            }



            anim_speed = player_rigid.velocity.magnitude * 1.0f;
            if (player_rigid.velocity.x < 0.0f)
            {
                char_is_moving = 1;

                anim_speed = -1.0f;
            }
            else if (player_rigid.velocity.x > 0.0f)
            {
                char_is_moving = 1;

                anim_speed = 1.0f;

            }
            else
            {
                char_is_moving = 0;

            }

            if (player_rigid.velocity.magnitude > max_speed)
            {
                player_rigid.velocity = Vector2.ClampMagnitude(player_rigid.velocity, max_speed);
            }

        }
        
        
        player_body.transform.localScale = local_scale;
    }
    

    private void PlayerMove()
    {
        player_rigid = this.GetComponent<Rigidbody2D>();
        input_x_axis = Input.GetAxis("Horizontal");
        jump = Input.GetKey(KeyCode.Space);

        

        joystick_x_axis = joystick.Horizontal;

        input_x_axis += joystick_x_axis * 2;


        
        joystick_y_axis = joystick.Vertical;

        if (joystick_y_axis > 0.5)
        {
            jump = true;
        }


            if (IsGrounded() && player_rigid.velocity.y == 0.0f && jump)
        {
            jump_force = 10;
            jump_dis_drag = 0.5f;
            player_rigid.AddForce(jump_axis * jump_force_mult, ForceMode2D.Impulse);
            if (IsGrounded() && jump)
            {
                jump_dis_drag = 0.2f;
            }

        }
        else
        {
            jump_dis_drag = 0.1f;
        }
        if (IsGrounded())
        {
            if (char_is_moving == 1)
            {
                is_scraping = false;
                if (footstep_sound.isPlaying)
                {
                    
                }
                else
                {
                    
                    footstep_sound.PlayOneShot(footstep_step);
                    
           
                }


                footstep_sound.volume = footstep_volume;
                  
            }


            footstep_volume = player_rigid.velocity.magnitude;
            footstep_sound.pitch = player_rigid.velocity.magnitude * 0.4f + 1;

            jump_dis_drag = 1;
            
        }

        else
        {

            footstep_sound.volume = footstep_volume * 0.0f;

        }

        if (player_rigid.velocity.magnitude < 0.1)
        {
            footstep_sound.volume = footstep_volume * 0.0f;
        }

        player_rigid.AddForce(move_axis * move_force * move_allowed, ForceMode2D.Force);
        
        //player_rigid.AddForce(jump_axis * jump_force, ForceMode2D.Impulse);

        player_animator.SetFloat("Horizontal", char_is_moving);
        player_animator.SetFloat("Speed", (anim_speed * player_rigid.velocity.magnitude * 0.5f));
        player_animator.SetBool("FacingRight", facing_right);
        player_animator.SetBool("Is Not Jumping", !IsGrounded());

       
        
    }

}
