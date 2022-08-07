using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Base<Pool>
{
    private static Dictionary<string, Queue<GameObject>> pool = new();

    public static GameObject Get(string name)
    {
        GameObject obj;

        if (pool.ContainsKey(name) && pool[name].Count > 0)
        {
            obj = pool[name].Dequeue();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        obj.SetActive(true);
        return obj;
    }

    public static void Recyle(GameObject obj)
    {
        obj.SetActive(false);
        if (pool.ContainsKey(obj.name))
        {
            pool[obj.name].Enqueue(obj);
        }
        else
        {
            pool.Add(obj.name, new Queue<GameObject>());
            pool[obj.name].Enqueue(obj);
        }
    }
}
