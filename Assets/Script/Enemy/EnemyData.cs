using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemy",menuName = "New Enemy")]
public class EnemyData : ScriptableObject
{
   public string id;
   public GameObject spawPrefab;
}
