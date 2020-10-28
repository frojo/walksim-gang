using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TransitionWorlds : MonoBehaviour
{
    private PlayerInput input;
    private FirstPersonDrifter movement;
    private MouseLook xLook;
    private AudioSource bgm;

    public GameObject lightCamGO;
    public GameObject hellCamGO;
    public GameObject downWorldGOs;
    public AudioClip dayBGM;
    public AudioClip underBGM;
    public AudioClip dayToUnderSFX;
    public AudioClip underToDaySFX;
    public AudioSource transSFX;

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

    void Start()
    {
      input = GetComponent<PlayerInput>();
      movement = GetComponent<FirstPersonDrifter>();
      xLook = GetComponent<MouseLook>();
      bgm = GetComponent<AudioSource>();

      GoToUpWorld();
    }

    void Update()
    {
      if (inTransition) {
        // lerp
        transform.position = Vector3.SmoothDamp(
            transform.position, end, ref velocity, 1 / transitionSpeed);
      }
      
    }

    public void OnFlip(InputAction.CallbackContext ctx)
    {

      // only activate on mouse button released
      if (ctx.canceled) {
        // don't overlap transitions...
        if (inTransition) {
          return;
        }


        inTransition = true;
        StartCoroutine(DoTransition());
      }

    }

    private IEnumerator DoTransition() {
        if (inHell) {
          transSFX.clip = underToDaySFX;
        } else {
          transSFX.clip = dayToUnderSFX;
        }

        transSFX.Play();

        yield return new WaitForSeconds(.7f);

        if (inHell) {
          GoToUpWorld();
        } else {
          GoToDownWorld();
        }
        inHell = !inHell;

        inTransition = false;
    }

    void GoToUpWorld() {
          lightCamGO.SetActive(true);
          hellCamGO.SetActive(false);
          movement.SetUpsideDown(false);
          xLook.SetUpsideDown(false);
          downWorldGOs.SetActive(false);
          bgm.clip = dayBGM;
          bgm.volume = .15f;
          bgm.Play();
    }

    void GoToDownWorld() {
          lightCamGO.SetActive(false);
          hellCamGO.SetActive(true);
          movement.SetUpsideDown(true);
          xLook.SetUpsideDown(true);
          downWorldGOs.SetActive(true);
          bgm.clip = underBGM;
          bgm.volume = .3f;
          bgm.Play();
    }


    // floats are dumb
    bool FloatEqual(float a, float b) {
      return Mathf.Abs(a - b) < .01;
    }
}
