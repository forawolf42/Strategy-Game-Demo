using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Production
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float attackRange = 5;

    public override void OnMouseClickedRight(Transform vector4)
    {
        if (!IsSelected)
        {
            return;
        }

        Pathfinding.instance.seeker = transform;
        Pathfinding.instance.target = vector4;
        Pathfinding.instance.GoThePosition();

    }
}
