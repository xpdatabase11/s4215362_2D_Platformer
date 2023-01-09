using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class player_guns : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject weapon_shotgun;
    private Animator weapon_shotgun_anim;
    private bool has_fired;
    private bool is_idle;
    private bool is_slide;
    private bool is_fire;
    private AudioSource audio;
    private AudioSource audio_new;
    public AudioClip shotgun_fire_sound;
    public AudioClip shotgun_slide_sound;
    public Light2D muzzle_light;
    public ParticleSystem smoke;
    private float t;
    private float maximum = 3.0f;
    private float minimum = 1.0f;
    public GameObject arm_axis;
    public Quaternion arm_axis_flip;
    public float arm_axis_offset_amount;
    public Quaternion arm_axis_offset;
    public GameObject muzzle_pos;
    private player_move player_move;
    
    

    public GameObject bullet;


    void Start()
    {

       

        weapon_shotgun_anim = weapon_shotgun.GetComponent<Animator>();
        audio = weapon_shotgun.GetComponent<AudioSource>();
        muzzle_light = weapon_shotgun.GetComponentInChildren<Light2D>();
        smoke.GetComponent<ParticleSystem>();
        player_move = GetComponent<player_move>();

    }

    void  Update()
    {
        arm_axis_flip = arm_axis.transform.rotation * Quaternion.Euler(0, 0, 180.0f);
        Quaternion bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount);
        arm_axis_offset = arm_axis.transform.rotation * bullet_rot;

        t += 1.0f * Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 5.0f;
        }

        Shoot();
        has_fired = false;
        is_idle = weapon_shotgun_anim.GetCurrentAnimatorStateInfo(0).IsName("shotgun_idle");
        is_slide = weapon_shotgun_anim.GetCurrentAnimatorStateInfo(0).IsName("shotgun_slide");
        is_fire = weapon_shotgun_anim.GetCurrentAnimatorStateInfo(0).IsName("shotgun_fire 1");

        if (is_idle)
        {
            muzzle_light.intensity = Mathf.Abs(0);


            if (Input.GetButtonDown("Fire1") && has_fired == false)
            {
                Destroy(audio_new);
                audio_new = weapon_shotgun.AddComponent<AudioSource>();

                has_fired = true;

                audio_new.GetComponent<AudioSource>();
                audio_new.clip = shotgun_fire_sound;

                smoke.Play();

                audio_new.Play();
                if(player_move.facing_right == true)
                {
                    
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount = 0.0f) ;
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis.transform.rotation * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount + 5.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis.transform.rotation * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount - 5.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis.transform.rotation * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount + 10.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis.transform.rotation * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount -10.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis.transform.rotation * bullet_rot);
                }
                else if(player_move.facing_right == false)
                {
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount = 0.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis_flip * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount + 5.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis_flip * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount - 5.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis_flip * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount + 10.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis_flip * bullet_rot);
                    bullet_rot = Quaternion.Euler(0, 0, arm_axis_offset_amount - 10.0f);
                    Instantiate<GameObject>(bullet, muzzle_pos.transform.position, arm_axis_flip * bullet_rot);
                }
                

            }


        }
        if (is_slide)
        {
            muzzle_light.intensity = 0;
            smoke.Stop();
            audio.clip = shotgun_slide_sound;
            if (audio.isPlaying)
            {

            }
            else {
                audio.PlayOneShot(shotgun_slide_sound);
            }
            
        }
        if (is_fire)
        {
            
            muzzle_light.intensity = Mathf.Lerp(minimum, maximum, t * 20);
            
            
        }
        
    }
    void Shoot()
    {
        weapon_shotgun_anim.SetBool("fired", has_fired);
        

        
    }
}
