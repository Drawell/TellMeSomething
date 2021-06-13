using UnityEngine;
using System.IO;
using DialogGraph;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public DefaultStartNodeExecutor startNodeExcecutor;
    public DefaultReplicaExecutor replicaExecutor;
    public DefaultEndNodeExecutor endNodeExecutor;

    private Node _currentNode;
    private bool _isCanMakeNextStep = false;
    private int _userChoise = 0;

    private float _timeLeft = 0;
    private float _minimalTimeInputIgnore = 500;
    private float _minimalTimeLeft;
    private bool _infiniteWaiting = false;

    void Start()
    {
        string actOnePath = Path.Combine(Path.Combine(Application.dataPath, "ForeverNotAlone", "Acts"), "ActOne.json");
        try
        {
            TaleAct act = ActLoader.LoadAct(actOnePath,
                startNodeExcecutor,
                replicaExecutor,
                endNodeExecutor);

            _currentNode = act.GetStartNode();
            _isCanMakeNextStep = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    void Update()
    {


        if (_isCanMakeNextStep && _currentNode != Node.EmptyNode)
        {
            _currentNode.Execute();
            _isCanMakeNextStep = false;
            _timeLeft = _currentNode.Timeout;
            _minimalTimeLeft = _minimalTimeInputIgnore;
            _infiniteWaiting = _timeLeft < 0;
            _currentNode = _currentNode.GetNextNode(_userChoise);
        }

        if (_minimalTimeLeft < 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    _isCanMakeNextStep = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                _isCanMakeNextStep = true;
            }
        }
        else
        {
            _minimalTimeLeft -= Time.deltaTime * 1000;
        }

        if (_timeLeft <= 0 && !_infiniteWaiting)
        {
            _isCanMakeNextStep = true;
        }
        else
        {
            _timeLeft -= Time.deltaTime * 1000;
        }


    }
}
