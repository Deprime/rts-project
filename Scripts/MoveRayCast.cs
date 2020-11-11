using UnityEngine;
using UnityEngine.AI;


public class MoveRayCast : MonoBehaviour
{
    [SerializeField] LayerMask whatCanBeClick;
    [SerializeField] private NavMeshAgent _myAgent;
    public bool isSelected;
    //[SerializeField] private Animator _anim;
    //private Vector3 _targetPosition;

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        //_anim = GetComponent<Animator>();
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        RtsGameController units = controller.GetComponent<RtsGameController>();
        units.NewUnit(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if(Physics.Raycast(ray, out rayHit, 100f, whatCanBeClick))
            {
                _myAgent.SetDestination(rayHit.point);
                //_anim.Play("Run"); 
            }
        }
    }
}
