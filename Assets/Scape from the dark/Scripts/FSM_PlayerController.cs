using FSMs;
using UnityEngine;

[CreateAssetMenu(fileName = "FSM_PlayerController", menuName = "Finite State Machines/FSM_PlayerController", order = 1)]
public class FSM_PlayerController : FiniteStateMachine
{

    private PlayerBlackboard m_playerBlackboard;
    private PathFeeder m_pathFeeder;
    private GameObject m_theStreetLight;

    public override void OnEnter()
    {
        m_playerBlackboard = GetComponent<PlayerBlackboard>();
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


        State goingToLight = new State("goingToLight",
            () =>
            {
                m_playerBlackboard.m_playerIsSafe = false;
                m_pathFeeder.target = m_playerBlackboard.m_theLight;
                m_pathFeeder.enabled = true;
            },
            () => { },
            () => { m_pathFeeder.enabled = false; }
        );

        State staySafe = new State("staySafe",
            () =>
            {
                m_playerBlackboard.m_playerIsSafe = true;
                //m_pathFeeder.enabled = false;
            },
            () => { },
            () => { }
        );



        Transition lightFound = new Transition("lightFound",
            () => {
                m_theStreetLight = SensingUtils.FindRandomInstanceWithinRadius(gameObject, "streetLight", m_playerBlackboard.m_lightDetectedRadius);
                return m_theStreetLight != null; },
            () => { }
        );


        Transition notSafe = new Transition("notSafe",
           () =>
           {
               return m_theStreetLight.GetComponent<StreetLightBlackboard>().m_streetLightOn == false;

           },
           () => { Debug.Log("Transitioning to goingToLight"); }
        );


        AddStates(goingToLight, staySafe);

        AddTransition(goingToLight, lightFound, staySafe);
        AddTransition(staySafe, notSafe, goingToLight);



        initialState = goingToLight;


    }
   
}
