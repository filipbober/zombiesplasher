using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class EnemyInputResponse : MonoBehaviour, IPointerClickHandler, IEnemyInputResponse
{
    //public static event System.EventHandler<EnemyClickedEventArgs> EnemyClicked;    
    public event EventHandler<EnemyClickedEventArgs> EnemyClicked;

    private EnemyData _enemyData;    

    public void Initialize(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Enemy clicked");
        OnEnemyClicked(new EnemyClickedEventArgs(gameObject, _enemyData));
    }

    public void OnEnemyClicked(EnemyClickedEventArgs e)
    {
        if (EnemyClicked != null)
        {
            EnemyClicked(this, e);
        }
    }

}
