using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
 
    WaveConfig waveConfig;
    int waypointIndex = 0;
    List<Transform> waypoints;
    [SerializeField] bool isKamikaze = false;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var movementThisFrame = waveConfig.GetMovementSpeed() * Time.deltaTime;
        if (isKamikaze == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementThisFrame);
        }
        else
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPos = waypoints[waypointIndex].transform.position;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
                if (transform.position == targetPos)
                {
                    waypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
