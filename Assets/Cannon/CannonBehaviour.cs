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
        movingCannon = false;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateCannon(-rotateSpeed);
            movingCannon = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotateCannon(rotateSpeed);
            movingCannon = true;
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

    private void RotateCannon(float speed)
    {
        transform.RotateAround(
            cannon.pivotBottomLeft.position,
            Vector3.forward,
            30f * speed * Time.deltaTime
        );
        float angle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(0f, 0f, ClampAngle(angle, -72.0f,18.0f));
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