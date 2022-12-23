using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    [HideInInspector] public Sprite _productionSprite = null;
    [HideInInspector] public Vector3 birthVector;
    [HideInInspector] public string title;
    [HideInInspector] public string subTitle;
    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;
    protected bool IsSelected;

    private void Awake()
    {
        transform.position = birthVector;
    }

    private void OnEnable()
    {
        ProductionManager.OnMouseClickedLeft += OnMouseClickedLeft;
        ProductionManager.OnMouseClickedRight += OnMouseClickedRight;
    }

    private void OnDisable()
    {
        ProductionManager.OnMouseClickedLeft -= OnMouseClickedLeft;
        ProductionManager.OnMouseClickedRight -= OnMouseClickedRight;
    }

    private void OnMouseClickedLeft()
    {
        IsSelected = false;
    }

    public virtual void OnMouseClickedRight(Transform vector3)
    {
        
        
    }

    private void OnMouseUp()
    {
        IsSelected = true;
        
        InformationController.instance.UpdateInformationView(title, subTitle, _productionSprite);
        InformationController.instance.UpdateProduction(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health<=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
}
