using System.Collections.Generic;
using UnityEngine;

public enum CrowdMode
{
    Naive,
    Optimized
}

public class CrowdManager : MonoBehaviour
{
    public static CrowdMode GlobalMode { get; private set; }

    [Header("General")]
    [SerializeField] private GameObject agentPrefab;
    [SerializeField] private int agentCount = 500;

    [Header("Mode")]
    [SerializeField] private CrowdMode currentMode = CrowdMode.Naive;

    private readonly List<GameObject> activeAgents = new List<GameObject>();

    public CrowdMode CurrentMode => currentMode;
    public int ActiveAgentCount => activeAgents.Count;

    [Header("Optimized")]
    [SerializeField] private ObjectPool agentPool;


    private void Start()
    {
        GlobalMode = currentMode;
        SpawnNaive();
    }

        private void ClearAgents()
    {
        foreach (var agent in activeAgents)
        {
            if (agent == null) continue;

            if (currentMode == CrowdMode.Optimized && agentPool != null)
            {
                agentPool.ReturnToPool(agent);
            }
            else
            {
                Destroy(agent);
            }
        }

        activeAgents.Clear();
    }


    private void SpawnNaive()
    {
        ClearAgents();

        for (int i = 0; i < agentCount; i++)
        {
            Vector3 pos = GetRandomPosition();
            GameObject agent = Instantiate(agentPrefab, pos, Quaternion.identity);
            activeAgents.Add(agent);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float range = 50f;
        float x = Random.Range(-range, range);
        float z = Random.Range(-range, range);
        return new Vector3(x, 0f, z);
    }

    public void ToggleMode()
    {
        if (currentMode == CrowdMode.Naive)
        {
            currentMode = CrowdMode.Optimized;
            GlobalMode = currentMode;
            SpawnOptimized();
        }
        else
        {
            currentMode = CrowdMode.Naive;
            GlobalMode = currentMode;
            SpawnNaive();
        }
    }


        private void SpawnOptimized()
    {
        ClearAgents();

        if (agentPool == null)
        {
            Debug.LogError("CrowdManager has no agentPool assigned.");
            return;
        }

        for (int i = 0; i < agentCount; i++)
        {
            Vector3 pos = GetRandomPosition();
            GameObject agent = agentPool.GetFromPool();
            agent.transform.position = pos;
            agent.transform.rotation = Quaternion.identity;
            activeAgents.Add(agent);
        }
    }

}
