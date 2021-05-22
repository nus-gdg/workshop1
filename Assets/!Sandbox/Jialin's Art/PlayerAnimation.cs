using UnityEngine;

namespace Project.Views.World.Entities {
  public class PlayerAnimation : MonoBehaviour
  {
      //private InputManager.PlayerActions _controls;
      private Animator anim;
      public PlayerUi player;
      // Start is called before the first frame update
      void Start()
      {
          anim = GetComponent<Animator>();
          //player = GetComponent<PlayerUi>();
      }

      // Update is called once per frame
      void Update()
      {
          var moveInput = player.Direction;
          anim.SetFloat("inputX", moveInput.x);
          anim.SetFloat("inputY", moveInput.y);
          var isWalking = player.Speed != 0;
          anim.SetBool("isWalking", isWalking);
      }
  }
}


