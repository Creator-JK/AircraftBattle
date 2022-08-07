using UnityEngine;

public class Behaviours : Base<Behaviours>
{
    public static void Fire(string Bulletname, Vector3 FirePos, Vector3 MiddlePos)
    {
        Pool.Get(Bulletname).transform.position = FirePos + MiddlePos;
    }

    public static void Fire(string Bulletname, Vector3 FirePos, Vector3 LeftPos, Vector3 RightPos)
    {
        Pool.Get(Bulletname).transform.position = FirePos + LeftPos;
        Pool.Get(Bulletname).transform.position = FirePos + RightPos;
    }

    public static Vector3 MoveEdgeDetection(Vector3 Pos, Vector3 Direction, Vector3 min, Vector3 max)
    {
        if (Pos.x < min.x || Pos.x > max.x)
            Direction.x = -Direction.x;
        if (Pos.y < min.y || Pos.y > max.y)
            Direction.y = -Direction.y;
        return Direction;
    }

    public static float Move(Transform transform, float MoveTime, float Speed, Vector3 Direction, Vector3 min, Vector3 max)
    {
        if (transform.position.y > 3.68f)
            transform.Translate(new Vector3(0, -2, 0) * Time.deltaTime);
        if (MoveTime < 0)
        {
            Direction = new(Random.Range(-Speed, Speed), Random.Range(-Speed, Speed));
            MoveTime = Random.Range(2f, 3f);
        }
        Direction = Behaviours.MoveEdgeDetection(transform.position, Direction, min, max);
        transform.Translate(Direction * Time.deltaTime);
        return MoveTime;
    }
}
