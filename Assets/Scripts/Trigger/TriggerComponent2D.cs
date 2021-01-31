using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerComponent2D : MonoBehaviour
{
    public GameEvent OnCollisionEnterEvent;
    public GameEvent OnCollisionExitEvent;
    public GameEvent OnTriggerEnter2DEvent;
    public GameEvent OnTriggerExit2DEvent;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameContext context = new GameContext();
        context.ContextEntity = collision.gameObject;
        OnCollisionEnterEvent.Invoke(context);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GameContext context = new GameContext();
        context.ContextEntity = collision.gameObject;
        OnCollisionExitEvent.Invoke(context);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameContext context = new GameContext();
        context.ContextEntity = other.gameObject;
        OnTriggerEnter2DEvent.Invoke(context);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GameContext context = new GameContext();
        context.ContextEntity = other.gameObject;
        OnTriggerExit2DEvent.Invoke(context);
    }
}
