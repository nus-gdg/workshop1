using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
    public SpriteRenderer Head;
    public void Dive()
    {
        Head.color = Color.black;
    }

    public void Emerge()
    {
        Head.color = Color.white;
    }

}
