using UnityEngine;
using UnityEngine.InputSystem;

public class GoToHell : MonoBehaviour
{
    private PlayerInput input;
    private bool inHell = false;

    // Start is called before the first frame update
    void Start()
    {
      print("started being a player");
      input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFlip(InputAction.CallbackContext ctx)
    {
      print("flip!!");
      // HEY WE MIGHT NOT HAVE THE VARIABLES FROM THE CURRENT CLASS
      // we might have to just use ctx
      //print(input.active);

      // what a weird fucking word
      //input.PassivateInput();

      // disable inputs
      // turn off colliders
      // turn off gravity
      // lerp dude
      // turn on colliders
      // turn on gravity upside down
    }
}
