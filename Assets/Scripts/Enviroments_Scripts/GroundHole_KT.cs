using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHole_KT : MonoBehaviour
{
    List<EnemyController_ML> enemys;
    List<float> timers;
    List<float> start_heights;
    void Start()
    {
        enemys = new List<EnemyController_ML>();
        timers = new List<float>();
        start_heights = new List<float>();
    }

    void Update()
    {
        for(int i = 0; i < enemys.Count; i++)
        {
            var ifEnd = EnemyFallAnim(i);
            if (ifEnd)
            {
                var death = enemys[i];
                enemys.RemoveAt(i);
                timers.RemoveAt(i);
                start_heights.RemoveAt(i);
                i--;
                Destroy(death.gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var enem = collision.GetComponent<EnemyController_ML>();
        if (!enem) return;
        enemys.Add(enem);
        timers.Add(0.8f);
        start_heights.Add(enem.transform.position.y);
    }

    bool EnemyFallAnim(int index)
    {
        timers[index] -= Time.deltaTime;
        if (timers[index] <= 0.0f) return true;
        float _t = 1.0f - (timers[index] / 0.8f);
        float param1 = 0.1f;
        float _y = ((_t - param1) * (_t - param1) - param1 * param1) / (1.0f - param1) / (1.0f - param1);
        float dist_y = _y * (-5.0f);
        enemys[index].transform.position = new Vector3(enemys[index].transform.position.x, start_heights[index] + dist_y, enemys[index].transform.position.z);
        return false;
    }
}
