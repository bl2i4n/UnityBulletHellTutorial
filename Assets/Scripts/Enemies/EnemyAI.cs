using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFlaot = 2f;

    private enum State
    {
        Roaming,
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }
    
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while(state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            // Debug.Log(roamPosition);
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFlaot);
        }
    }

    private Vector2 GetRoamingPosition()
{                                                                      // normalize makes sure it doesn't go quicker and doesn't go diagonally
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
    }
}
