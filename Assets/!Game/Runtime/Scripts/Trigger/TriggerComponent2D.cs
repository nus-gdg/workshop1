using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class TriggerComponent2D : MonoBehaviour
{
    public GameEvent OnCollisionEnterEvent;
    public GameEvent OnCollisionExitEvent;
    public GameEvent OnTriggerEnterEvent;
    public GameEvent OnTriggerExitEvent;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameContext context = new GameContext();
        context.ContextEntity = collision.gameObject;
        context.SourceEntity = gameObject;
        OnCollisionEnterEvent.Invoke(context);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GameContext context = new GameContext();
        context.ContextEntity = collision.gameObject;
        context.SourceEntity = gameObject;
        OnCollisionExitEvent.Invoke(context);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameContext context = new GameContext();
        context.ContextEntity = other.gameObject;
        context.SourceEntity = gameObject;
        OnTriggerEnterEvent.Invoke(context);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GameContext context = new GameContext();
        context.ContextEntity = other.gameObject;
        context.SourceEntity = gameObject;
        OnTriggerExitEvent.Invoke(context);
    }
}
