using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Pathfinding;

public class Tentacle : MonoBehaviour
{
    public Animator Anim;
    public Combat.HitBox BodyHitBox;

    private IAstarAI AstarAI;
    private bool isUnder = false;

    void Start()
    {
        AstarAI = GetComponent<IAstarAI>();
    }
    public void MoveToPosition(Vector3 position)
    {
        AstarAI.destination = position;
    }

    public void Dive()
    {
        if (!isUnder)
        {
            BodyHitBox.enabled = false;
            Anim.Play("Tentacle_Dive");
            isUnder = true;
        }
    }
    public void Emerge()
    {
        if (isUnder)
        {
            isUnder = false;
            BodyHitBox.enabled = true;
            Anim.Play("Tentacle_Rise");
            MoveToPosition(transform.position);
        }
    }

    private void Update()
    {
        if (isUnder)
        {
            GameObject playerGo = Game.Instance.World.Player.gameObject;
            Vector3 dir = (playerGo.transform.position - transform.position);
            if (dir.sqrMagnitude < 1f)
            {
                Emerge();
            }
        }
    }
}
