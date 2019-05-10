using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private float acceleration = 0.1f;
    private float maxSpeed = 1f;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        House house = FindObjectOfType<House>();
        target = house.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        Vector2 direction2d = new Vector2(direction.x, direction.y);
        direction2d.Normalize();
        
        
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        
        // TODO
        
        
//        velocity += direction2d * Time.deltaTime * acceleration;
//        
//        float magnitude = velocity.magnitude;
//        if (magnitude > maxSpeed)
//        {
//            velocity = direction2d.normalized * maxSpeed;
//        }

        velocity = direction2d.normalized * maxSpeed;
        GetComponent<Rigidbody2D>().velocity = velocity;
        

    }
}
