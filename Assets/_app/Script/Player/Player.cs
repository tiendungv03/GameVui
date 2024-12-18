using System.Data;
using System;

public class Player
{
    private int health;
    private float speed;

    public void AddDataStatus(IDataReader readEnemyStatus)
    {
        health = Convert.ToInt32(readEnemyStatus["health"]);
        speed = (float)Convert.ToDouble(readEnemyStatus["speed"]);
    }

    public override string ToString()
    {
        return "health: " + health +
                "speed: " + speed;
    }

    public int Health() { return health; }
    public float Speed() { return speed; }
}
