using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject muzzle_flash;
    public GameObject player_body;
    private Vector3 particle_pos;
    private Vector3 particle_rot;
    private Vector3 particle_offset;
    private Quaternion particle_rot_offset;
    private Vector3 muzzle_flash_offset;
    ParticleSystem.ShapeModule particle_shape;
    private Quaternion particle_rot_euler;
 

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle_shape = particle.shape;
        particle_offset = particle_shape.position;
        particle_rot_offset = transform.rotation;
        muzzle_flash_offset = muzzle_flash.transform.position;
    }

    void Update()
    {
        //Vector3 muzzle_rot = Quaternion.Euler(muzzle_pos.transform.rotation);
     
        particle_rot_euler = Quaternion.Euler(particle_rot);

    
        particle_shape = particle.shape;
        particle_shape.position = ((muzzle_flash.transform.position - muzzle_flash_offset)) - particle_offset;
        

        //particle_shape.rotation = Quaternion.Inverse(particle_rot_offset) * particle_shape.rotation;
        print($"Particle position: {particle_shape.position}");
        
        //particle_pos = muzzle_pos.transform.position;
        //particle_rot_euler = muzzle_pos.transform.rotation;

        //particle_rot = new Vector3(particle_rot_euler.x, particle_rot_euler.y, particle_rot_euler.z);
        //transform.localScale = player_body.transform.lossyScale;

        
    }
}
