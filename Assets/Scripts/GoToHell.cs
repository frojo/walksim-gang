using UnityEngine;
using UnityEngine.InputSystem;

public class GoToHell : MonoBehaviour
{
    private PlayerInput input;
    private bool inHell = false;

    private bool inTransition = false;
    private bool startTransition = false;

    private int test = 69;
    private FirstPersonDrifter fpdtest;

    // Start is called before the first frame update
    void Start()
    {
      print("started being a player");
      input = GetComponent<PlayerInput>();
      fpdtest = GetComponent<FirstPersonDrifter>();
      print(input);
      print(fpdtest);
    }

    // Update is called once per frame
    void Update()
    {

      print("startTransition = " + startTransition);
      print("inTransition = " + inTransition);
      if (startTransition) {
        startTransition = false; 
        print("starting transition!!!");

      }

      
    }

    public void OnFlip(InputAction.CallbackContext ctx)
    {
      // for some reason, input is Null in this event handler, but
      // we can call GetComponent() to get it /shrug
      inHell = !inHell;
      // HEY WE MIGHT NOT HAVE THE VARIABLES FROM THE CURRENT CLASS
      // we might have to just use ctx
      //print(input.active);

      print("inTransition (flip)= " + inTransition);
      // only activate on mouse button released
      if (ctx.canceled && !inTransition) {
        print("flip!!");
        inTransition = true;
        startTransition = true;

      }
      print("inTransition (flip after)= " + inTransition);

      // disable inputs
      // turn off colliders
      // turn off gravity
      // lerp dude
      // turn on colliders
      // turn on gravity upside down
    }
}
