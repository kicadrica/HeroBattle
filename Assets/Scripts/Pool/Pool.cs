using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [Serializable]
    public class PoolSettings
    {
        public TypeOfPool Type;
        public GameObject Prefab;
        public int Count;
    }

    [SerializeField] private PoolSettings[] Settings;
    private static Pool _instance;

    private static Dictionary<TypeOfPool, Queue<GameObject>> _poolsDictionary = new Dictionary<TypeOfPool, Queue<GameObject>>();
    
    private void Awake()
    {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        
        for (var i = 0; i < Settings.Length; i++) {
            FillSinglePool(Settings[i].Type);
        }
    }

    private void FillSinglePool(TypeOfPool type)
    {
        var settings = Settings.FirstOrDefault(item => item.Type == type);
        if (settings == null) return;
        
        if (!_poolsDictionary.ContainsKey(settings.Type)) {
            _poolsDictionary.Add(settings.Type, new Queue<GameObject>());
        }
        
        for (int i = 0; i < settings.Count; i++) {
            var poolItem = Instantiate(settings.Prefab, transform);
            poolItem.SetActive(false);
            _poolsDictionary[settings.Type].Enqueue(poolItem);
        }
    }

    public static T GetFromPool<T>(TypeOfPool type)
    {
        if (!_poolsDictionary.ContainsKey(type) || _poolsDictionary[type].Count == 0) {
           _instance.FillSinglePool(type);
           
           Debug.Log("Pool creates new item: " + type);
        }
        var item = _poolsDictionary[type].Dequeue();
        item.SetActive(true);
        foreach (var poolItem in item.GetComponentsInChildren<IPoolItem>()) {
            poolItem.ResetPoolItem();
        }
        return item.GetComponent<T>();
    }

    public static void PutToPool(TypeOfPool type, Component item)
    {
        item.gameObject.SetActive(false);
        _poolsDictionary[type].Enqueue(item.gameObject);
    }

}

public enum TypeOfPool{MonsterBullet, PlayerBullet, MonsterExplosion, MonsterHit, PlayerHit, Coin, JellyMonster, BatMonster, Boss, YellowMonster, BatMonsterBoss}