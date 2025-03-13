using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "FSM_Player", menuName = "Finite State Machines/FSM_Player", order = 1)]
public class FSM_Player : FiniteStateMachine, IRestartGameElement
{
    private PlayerBlackboard m_playerBlackboard;


    public override void OnEnter()
    {
        GameManager.m_instanceGameManager.AddRestartGameElement(this);
        m_playerBlackboard = GetComponent<PlayerBlackboard>();
        base.OnEnter();
    }

    public override void OnExit()
    {
        
        base.OnExit();
    }

    public override void OnConstruction()
    {
        FiniteStateMachine playerBehaviour = ScriptableObject.CreateInstance<FSM_PlayerController>();


        State dead = new State("dead",
            () => { m_playerBlackboard.Die(); }, 
            () => { },
            () => { } 
        );



       

        Transition playerDies = new Transition("playerDies",
            () => { return SensingUtils.DistanceToTarget(gameObject, m_playerBlackboard.m_theMonster) <= 3; }, 
            () => { }  
        );



       
            
        AddStates(playerBehaviour, dead);

        AddTransition(playerBehaviour, playerDies, dead);

        initialState = playerBehaviour;


    }
    public void RestartGame()
    {
        currentState = initialState;
    }
}
