
using Steerings;
using UnityEngine;

public class GuillemGroupManager : Steerings.GroupManager
{
    public static GuillemGroupManager Instance { get; private set; }

    public int numInstances;
    public float delay = 0.5f;
    public GameObject prefab;
    //public bool around = false;
    //public GameObject attractor;
    //private BlackboardEnemies m_blackboardEnemies;

    private int created = 0;
    private float elapsedTime = 0f;

    // the following attributes are specifically created to help listeners of UI
    // components get the initial values for the UI elements they're attached to

    /*public float maxSpeed;
    public float maxAcceleration;
    public float cohesionThreshold;
    public float repulsionThreshold;
    public float coneOfVisionAngle;
    public float cohesionWeight;
    public float repulsionWeight;
    public float alignmentWeight;
    public float seekWeight;*/

    // Update is called once per frame

    // la variable que vols usar

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Update()
    {
        Spawn();
    }


    private void Spawn()
    {
        if (created == numInstances) return;

        if (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            return;
        }

        // if this point is reached, it's time to spawn a new instance
        GameObject clone = Instantiate(prefab);
        clone.transform.position = transform.position; 

        if (created == 0)
        {
            // first one and only it
            ShowRadiiPro shr = clone.GetComponent<ShowRadiiPro>();
            shr.componentTypeName = "Steerings.SteeringContext";
            shr.innerFieldName = "repulsionThreshold";
            shr.outerFieldName = "cohesionThreshold";
            shr.enabled = true;

        }

        BlackboardEnemies BbeP1 = clone.GetComponent<BlackboardEnemies>();
        if (BbeP1 != null)
        {
            BbeP1.m_formationIndex = created;  // cada enemic rep l'índex segons l'ordre de creació
            BbeP1.m_totalEnemies = numInstances; // estableix el nombre total d'enemics
            Debug.Log(BbeP1.m_formationIndex);
        }

        AddBoid(clone);
       // m_blackboardEnemies.GetComponent<BlackboardEnemies>().m_formationIndex += 1;
        created++;
        elapsedTime = 0f;
    }
}
