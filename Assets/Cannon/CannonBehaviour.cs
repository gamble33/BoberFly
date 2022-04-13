using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootBehaviour))]
public class CannonBehaviour : MonoBehaviour
{
    [SerializeField] private Cannon cannon;
    [SerializeField] private float rotateSpeed;

    private Vector3 _position;
    private bool _movingCannon = false;
    private ShootBehaviour _shootBehaviour;
    
    private void Awake() {
      _shootBehaviour = gameObject.GetComponent<ShootBehaviour>();
      SoundManager.Instance.StartSound(SoundManager.Sound.AimingMusic);
    }

    private void Start()
    {
        _position = transform.position;
    }

    private void Update()
    {

        
        
        if(GameManager.Instance.GetState() == GameManager.State.Aiming) {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Recoil();
                _shootBehaviour.Shoot();
                GameManager.Instance.SetState(GameManager.State.Flying);
                return;
            }

            _movingCannon = false;
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _movingCannon = true;
                RotateCannon(-rotateSpeed);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _movingCannon = true;
                RotateCannon(rotateSpeed);
            }

            if (_movingCannon)
            {
                SoundManager.Instance.StartSound(SoundManager.Sound.MachineMove);
            }
            else
            {
                SoundManager.Instance.StopSound(SoundManager.Sound.MachineMove);
            }
          
            transform.position = _position;
        } else {
            transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime);
        }
        
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
        if(clampedAngle != angle) _movingCannon = false;
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
     
     private void Recoil() {
        transform.position = new Vector3(transform.position.x - 0.25f, 0.0f, 0.0f);
     }

}
