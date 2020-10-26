﻿using UnityEngine;
using UnityEngine.InputSystem;

public class GoToHell : MonoBehaviour
{
    private PlayerInput input;
    private FirstPersonDrifter movement;

    private bool inHell = false;

    // the heights that the player will be moved to 
    public float lightY = 1.5f;
    public float hellY = -2.5f;
    public float transitionSpeed = .1f;

    // used for the transition where we are moving the player to/from hell
    private bool inTransition = false;
    private Vector3 start;
    private Vector3 end;
    private Vector3 velocity = Vector3.zero;




    // Start is called before the first frame update
    void Start()
    {
      input = GetComponent<PlayerInput>();
      movement = GetComponent<FirstPersonDrifter>();
    }

    // Update is called once per frame
    void Update()
    {
      if (inTransition) {
        // lerp
        transform.position = Vector3.SmoothDamp(
            transform.position, end, ref velocity, 1 / transitionSpeed);
        // if we've reached the destionation:
        //
        // inHell = !inHell
        // inTranstion = false
        // movement.gravity = -movement.gravity;
        if (FloatEqual(transform.position.y, end.y)) {
          inTransition = false;
          input.enabled = true;
          movement.enabled = true;
          inHell = !inHell;


          if (inHell) {
            movement.gravity = -10f;
          } else {
            movement.gravity = 10f;

          }

        }

      }
      
    }

    public void OnFlip(InputAction.CallbackContext ctx)
    {

      // only activate on mouse button released
      if (ctx.canceled) {
        // disable input
        input.enabled = false;
        movement.enabled = false;

        // set start and end for the transition logic
        start = transform.position;
        float currX = transform.position.x;
        float currZ = transform.position.y;
        if (inHell) {
          end = new Vector3(currX, lightY, currZ);
        } else {
          end = new Vector3(currX, hellY, currZ);
        }

        // signal to begin transition
        inTransition = true;
      }

    }

    // floats are dumb
    bool FloatEqual(float a, float b) {
      return Mathf.Abs(a - b) < .01;
    }
}
