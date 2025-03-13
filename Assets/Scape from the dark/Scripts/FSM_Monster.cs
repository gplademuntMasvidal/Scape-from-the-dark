using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "FSM_Monster", menuName = "Finite State Machines/FSM_Monster", order = 1)]
public class FSM_Monster : FiniteStateMachine
{
    private MonsterBlackboard m_monsterBlackboard;
    private PathFeeder m_pathFeeder;

    public override void OnEnter()
    {
        m_monsterBlackboard = GetComponent<MonsterBlackboard>();
        m_pathFeeder = GetComponent<PathFeeder>();
        base.OnEnter();
    }

    public override void OnExit()
    {

        DisableAllSteerings();
        base.OnExit();
    }

    public override void OnConstruction()
    {


        State goingToPlayer = new State("goingToPlayer",
            () =>
            {
                m_pathFeeder.target = m_monsterBlackboard.m_thePlayer;
                m_pathFeeder.enabled = true;
            },
            () => { },
            () => {  }
        );

        State searchingPlayer = new State("searchingPlayer",
            () => {
                m_pathFeeder.target = m_monsterBlackboard.m_leavingPoint;
            },
            () => { },
            () => {  }
        );



        Transition playerIsNotSafe = new Transition("playerFound",
            () => {

                bool l_playerIsSafe = m_monsterBlackboard.m_thePlayer.GetComponent<PlayerBlackboard>().m_playerIsSafe;

                return l_playerIsSafe == false;
            },
            () => { }
        );


        Transition playerIsSafe = new Transition("playerIsSafe",
            () =>
            {

                bool l_playerIsSafe = m_monsterBlackboard.m_thePlayer.GetComponent<PlayerBlackboard>().m_playerIsSafe;

                return l_playerIsSafe == true;

            },
            () => { }
        );


        AddStates(goingToPlayer, searchingPlayer);

        AddTransition(goingToPlayer, playerIsSafe, searchingPlayer);
        AddTransition(searchingPlayer, playerIsNotSafe, goingToPlayer);


        initialState = goingToPlayer;


    }
}
