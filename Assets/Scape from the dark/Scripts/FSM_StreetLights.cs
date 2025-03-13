using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "FSM_StreetLights", menuName = "Finite State Machines/FSM_StreetLights", order = 1)]
public class FSM_StreetLights : FiniteStateMachine, IRestartGameElement
{
    private StreetLightBlackboard m_streetLightBlackboard;

    public override void OnEnter()
    {
        GameManager.m_instanceGameManager.AddRestartGameElement(this);

        m_streetLightBlackboard = GetComponent<StreetLightBlackboard>();
        base.OnEnter();
    }

    public override void OnExit()
    {
        DisableAllSteerings();

        base.OnExit();
    }

    public override void OnConstruction()
    {


        State streetLightOFF = new State("streetLightOFF",
            () =>
            {
                gameObject.tag = "streetLight";
                m_streetLightBlackboard.m_streetLightOn = false;
                m_streetLightBlackboard.m_theLight.SetActive(false);
            },
            () =>
            {
            },
            () => { }
        );

        State streetLightON = new State("streetLightON",
            () =>
            {
                m_streetLightBlackboard.m_streetLightOn = true;
                m_streetLightBlackboard.m_theLight.SetActive(true);
            },
            () =>
            {
            },
            () => { }
        );

        State playerIsNear = new State("playerIsNear",
            () => { m_streetLightBlackboard.m_timer = 0; },
            () =>
            {
                m_streetLightBlackboard.m_timer += Time.deltaTime;
            },
            () => { }
        );

        State streetLightDesactivated = new State("streetLightDesactivated",
            () => {
                m_streetLightBlackboard.m_streetLightOn = false;
                m_streetLightBlackboard.m_theLight.SetActive(false);
                m_streetLightBlackboard.m_timer = 0; 
                gameObject.tag = "streetLightOff";
                GameManager.m_instanceGameManager.m_oneTorch = null;
            },
            () =>
            {
                m_streetLightBlackboard.m_timer += Time.deltaTime;

            },
            () => { }
        );




        Transition turningOnTheStreetLight = new Transition("turningOnTheStreetLight",
            () => { return m_streetLightBlackboard.m_streetLightOn == true; },
            () => { }
        );

        Transition startTurningOffTheStreetLight = new Transition("turningOffTheStreetLight",
            () => { return SensingUtils.DistanceToTarget(gameObject, m_streetLightBlackboard.m_thePlayer) <= m_streetLightBlackboard.m_inLightDistance; },
            () => { }
        );

        Transition timeToTurnOffTheStreetLightHasPassed = new Transition("timeToTurnOffTheStreetLightHasPassed",
            () => { return m_streetLightBlackboard.m_timer >= m_streetLightBlackboard.m_timeToTurnOff; },
            () => { }
        );

        Transition timeOutToBePrepared = new Transition("timeOutToActivate",
            () => { return m_streetLightBlackboard.m_timer >= m_streetLightBlackboard.m_timeToTurnOn; },
            () => { }
        );



        AddStates(streetLightOFF, streetLightON, playerIsNear, streetLightDesactivated);

        AddTransition(streetLightOFF, turningOnTheStreetLight, streetLightON);
        AddTransition(streetLightON, startTurningOffTheStreetLight, playerIsNear);
        AddTransition(playerIsNear, timeToTurnOffTheStreetLightHasPassed, streetLightDesactivated);
        AddTransition(streetLightDesactivated, timeOutToBePrepared, streetLightOFF);






        initialState = streetLightOFF;


    }

    public void RestartGame()
    {
        m_streetLightBlackboard.m_streetLightOn = false;
        m_streetLightBlackboard.m_theLight.SetActive(false);
        currentState = initialState;
        gameObject.tag = "streetLight";
    }
}
