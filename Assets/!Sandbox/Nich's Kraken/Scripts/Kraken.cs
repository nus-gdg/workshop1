using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
    public SpriteRenderer Head;
    public SpriteRenderer DiveShadow;
    public Material HeadMaterial;
    public List<Tentacle> Tentacles;
    public float YEdgeSpeed = 0.1f;

    public BoxCollider2D Zone;

    void Start()
    {
        HeadMaterial.SetFloat("Y_Edge", 0f);
    }

    void OnApplicationQuit()
    {
        HeadMaterial.SetFloat("Y_Edge", 0f);
    }

    public void Dive()
    {
        StopAllCoroutines();
        StartCoroutine(DiveRoutine());
    }
    public void TentacleDive()
    {
        foreach (Tentacle tentacle in Tentacles)
        {
            tentacle.Dive();
        }
    }

    public void TentacleEmerge()
    {
        foreach (Tentacle tentacle in Tentacles)
        {
            tentacle.Emerge();
        }
    }

    public void TentacleMoveToRandomPosition()
    {
        //float radius = Mathf.Min(Zone.size.x, Zone.size.y);
        foreach (Tentacle tentacle in Tentacles)
        {
            Vector3 position = Random.insideUnitCircle * 5f;
            tentacle.MoveToPosition(tentacle.transform.position + position);
        }
    }

    public void TentacleMoveToZoneBoundary()
    {
        float radius = Mathf.Min(Zone.size.x, Zone.size.y);
        Vector2 zoneTopLeft = new Vector2(Zone.bounds.min.x, Zone.bounds.max.y);
        foreach (Tentacle tentacle in Tentacles)
        {
            tentacle.MoveToPosition(zoneTopLeft);
            zoneTopLeft += Zone.bounds.size.x / Tentacles.Count * Vector2.right;
        }
    }

    public void Emerge()
    {
        StopAllCoroutines();
        StartCoroutine(EmergeRoutine());
    }

    IEnumerator EmergeRoutine()
    {
        DiveShadow.enabled = false;
        float value = HeadMaterial.GetFloat("Y_Edge");
        while (value > 0f)
        {
            value = value - YEdgeSpeed * Time.deltaTime;
            HeadMaterial.SetFloat("Y_Edge", value);
            yield return null;
        }
        HeadMaterial.SetFloat("Y_Edge", 0f);
    }

    IEnumerator DiveRoutine()
    {
        float value = HeadMaterial.GetFloat("Y_Edge");

        while (value < 1f)
        {
            value = value + YEdgeSpeed * Time.deltaTime;
            HeadMaterial.SetFloat("Y_Edge", value);
            yield return null;
        }
        HeadMaterial.SetFloat("Y_Edge", 1f);
        DiveShadow.enabled = true;
    }
}
