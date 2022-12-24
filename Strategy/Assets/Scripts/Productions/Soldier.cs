using UnityEngine;
public class Soldier : Production
{
    
    public override void OnMouseClickedRight(Transform vector4)
    {
        if (!IsSelected)
        {
            return;
        }
        Pathfinding.Instance.production = transform;
        Pathfinding.Instance.target = vector4;
        Pathfinding.Instance.FollowThePath();
        
    }
}
