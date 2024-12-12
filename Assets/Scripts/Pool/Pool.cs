using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [Serializable]
    public class PoolData
    {
        public TypeOfPool poolType;
        public GameObject prefab;
        public int count;
    }

    [SerializeField] private PoolData[] poolData;
    private static Pool _instance;

    private static readonly Dictionary<TypeOfPool, Queue<GameObject>> PoolsDictionary = new();
    
    private void Awake()
    {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        
        FillPool();
    }
    
    public static T GetObject<T>(TypeOfPool type)
    {
        if (!PoolsDictionary.ContainsKey(type) || PoolsDictionary[type].Count == 0) {
           _instance.CreateSingleObject(type);
           
           Debug.Log("Pool creates new item: " + type);
        }
        
        var item = PoolsDictionary[type].Dequeue();
        item.SetActive(true);
        
        foreach (var poolItem in item.GetComponentsInChildren<IPoolItem>()) {
            poolItem.ResetPoolItem();
        }
        
        return item.GetComponent<T>();
    }

    public static void ReturnObject(TypeOfPool type, Component item)
    {
        item.gameObject.SetActive(false);
        PoolsDictionary[type].Enqueue(item.gameObject);
    }
    
    private void CreateSingleObject(TypeOfPool type)
    {
        var data = poolData.FirstOrDefault(item => item.poolType == type);
        if (data == null)
        {
            throw new ArgumentException("The pool does not contain this type: " + type + ". Add it to the pool in the Inspector.");
        }

        var poolItem = Instantiate(data.prefab, transform);
        poolItem.SetActive(false);
        PoolsDictionary[data.poolType].Enqueue(poolItem);
    }

    private void FillPool()
    {
        foreach (var data in poolData)
        {
            if (!PoolsDictionary.ContainsKey(data.poolType)) {
                PoolsDictionary.Add(data.poolType, new Queue<GameObject>());
            }
            
            for (var i = 0; i < data.count; i++) {
                var poolItem = Instantiate(data.prefab, transform);
                poolItem.SetActive(false);
                PoolsDictionary[data.poolType].Enqueue(poolItem);
            }
        }
    }

}

public enum TypeOfPool
{
    MonsterBullet, 
    PlayerBullet, 
    MonsterExplosion, 
    MonsterHit, 
    PlayerHit, 
    Coin, 
    JellyMonster, 
    BatMonster, 
    YellowBoss, 
    YellowMonster, 
    BatBoss
    
}