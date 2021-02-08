using UnityEngine;

public class HideTilemapColliderOnPlay : MonoBehaviour
{
    private Renderer colliderRenderer;

    void Start()
    {
        colliderRenderer = GetComponent<Renderer>();
        colliderRenderer.enabled = false;
    }
}
