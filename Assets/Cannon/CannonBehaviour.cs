using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    [SerializeField] Cannon cannon;
    [SerializeField] private float rotateSpeed;

    private Vector3 _position;
    private bool movingCannon = false;

    private void Start()
    {
        _position = transform.position;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    
        movingCannon = false;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movingCannon = true;
            RotateCannon(-rotateSpeed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movingCannon = true;
            RotateCannon(rotateSpeed);
        }

        if (movingCannon)
        {
            SoundManager.Instance.StartSound(SoundManager.Sound.MachineMove);
        }
        else
        {
            SoundManager.Instance.StopSound(SoundManager.Sound.MachineMove);
        }

        transform.position = _position;
    }

    private void Shoot()
    {
        
    }

    private void RotateCannon(float speed)
    {
        transform.RotateAround(
            cannon.pivotBottomLeft.position,
            Vector3.forward,
            30f * speed * Time.deltaTime
        );

        float angle = transform.eulerAngles.z;
        float clampedAngle = ClampAngle(angle, -72.0f,18.0f);
        if(clampedAngle != angle) movingCannon = false;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, clampedAngle);
    }
    
     private float ClampAngle(float angle, float min, float max)
     {
         if(angle < 90f || angle > 270f)
         {
             if(angle > 180)
             {
                 angle -= 360f;
             }
             if(max > 180)
             {
                 max -= 360f;
             }
             if(min > 180)
             {
                 min -=360f;
             }
         }
         angle = Mathf.Clamp (angle, min, max);
         if(angle < 0)
         {
             angle += 360f;
         }
         return angle;
     }

}
