using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class EnemyInputResponse : MonoBehaviour, IPointerClickHandler, IEnemyInputResponse
{
    public event EventHandler<EnemyPropertiesEventArgs> EnemyClicked;

    private EnemyProperties _properties;

    public void Initialize(EnemyProperties enemyProperties)
    {
        _properties = enemyProperties;
    }

    public void OnEnemyClicked(EnemyPropertiesEventArgs e)
    {
        if (EnemyClicked != null)
        {
            EnemyClicked(this, e);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnEnemyClicked(new EnemyPropertiesEventArgs(gameObject, _properties));
    }
}
