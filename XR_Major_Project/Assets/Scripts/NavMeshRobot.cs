using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events; 

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshRobot : MonoBehaviour
{
	NavMeshAgent agent; 
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	public void MoveAgent(Vector3 move)
	{
		agent.destination = agent.transform.position + move;
	}
	public void StopAgent()
	{
		agent.ResetPath();
	}
	
}