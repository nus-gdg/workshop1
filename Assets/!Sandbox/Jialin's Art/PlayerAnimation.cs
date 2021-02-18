using Core;
using Core.Managers;
using UnityEngine;

//just to set inputX and inputY variables in the animator
//but scared to edit directly in player
public class PlayerAnimation : MonoBehaviour
{
    private InputManager.PlayerActions _controls;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _controls = Game.Instance.Input.Player;
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = _controls.Move.ReadValue<Vector2>();
        anim.SetFloat("inputX", moveInput.x);
        anim.SetFloat("inputY", moveInput.y);
    }
}
