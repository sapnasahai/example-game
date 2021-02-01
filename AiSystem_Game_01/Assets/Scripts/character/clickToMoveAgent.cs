
using UnityEngine;
using UnityEngine.AI;

public class clickToMoveAgent : MonoBehaviour
{
    public Camera camera;
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();



    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out m_HitInfo))
                m_Agent.destination = m_HitInfo.point;




        }




        
    }
}
