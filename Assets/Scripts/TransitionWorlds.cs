using UnityEngine;
using UnityEngine.InputSystem;

public class TransitionWorlds : MonoBehaviour
{
    private PlayerInput input;
    private FirstPersonDrifter movement;
    private MouseLook xLook;

    public GameObject lightCamGO;
    public GameObject hellCamGO;
    public GameObject downWorldGOs;

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

      GoToUpWorld();
    }

    void Update()
    {
      if (inTransition) {
        // lerp
        transform.position = Vector3.SmoothDamp(
            transform.position, end, ref velocity, 1 / transitionSpeed);

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
        // switch camera

        if (inHell) {
          GoToUpWorld();
        } else {
          GoToDownWorld();
        }
        inHell = !inHell;


#if false
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
#endif
      }

    }

    void GoToUpWorld() {
          lightCamGO.SetActive(true);
          hellCamGO.SetActive(false);
          movement.SetUpsideDown(false);
          xLook.SetUpsideDown(false);
          downWorldGOs.SetActive(false);
    }

    void GoToDownWorld() {
          lightCamGO.SetActive(false);
          hellCamGO.SetActive(true);
          movement.SetUpsideDown(true);
          xLook.SetUpsideDown(true);
          downWorldGOs.SetActive(true);
    }
    // floats are dumb
    bool FloatEqual(float a, float b) {
      return Mathf.Abs(a - b) < .01;
    }
}
