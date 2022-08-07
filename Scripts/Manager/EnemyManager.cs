using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public float Enemy1Time = 1;
    public float Enemy2Time = 3;
    public float Enemy3Time = 5;

    public int Enemy2Counter;
    public int Enemy3Counter;

    public float Interval_1 = 1;
    public float Interval_3 = 3;
    public float Interval_7 = 7;
    public float Interval_10 = 10;
    public float Double = 0.5f;

    public Vector2 CreatPos = new(0, 6.4f);

    private void FixedUpdate()
    {
        //Todo:优化敌人生成
        if (GameManager.Instance.isPlaying && !GameManager.Instance.isPaused)
        {
            Enemy1Time -= 0.02f;
            if (Enemy1Time < 0)
            {
                Enemy1Time = Random.Range(Interval_1, Interval_3);
                CreatPos.x = Random.Range(-2.6f, 2.6f);
                Pool.Get("Enemy1").transform.position = CreatPos;
            }
            Enemy2Time -= 0.02f;
            if (Enemy2Time < 0)
            {
                Enemy2Time = Random.Range(Interval_3 - Enemy2Counter * Double, Interval_7 - Enemy2Counter * Double);
                CreatPos.x = Random.Range(-2.6f, 2.6f);
                Pool.Get("Enemy2").transform.position = CreatPos;
            }
            Enemy3Time -= 0.02f;
            if (Enemy3Time < 0)
            {
                Enemy3Time = Random.Range(Interval_7 - Enemy3Counter * Double, Interval_10 - Enemy3Counter * Double);
                CreatPos.x = Random.Range(-2.6f, 2.6f);
                Pool.Get("Enemy3Fly1").transform.position = CreatPos;
            }
        }
    }
}
