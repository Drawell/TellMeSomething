using UnityEngine;
using System.IO;
using DialogGraph;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public DefaultStartNodeExecutor startNodeExcecutor;
    public DefaultCharAppExecutor charAppExecutor;
    public DefaultChoiseExecutor choiseExecutor;
    public DefaultReplicaExecutor replicaExecutor;
    public DefaultEndNodeExecutor endNodeExecutor;

    private Node _currentNode;
    private bool _isCanMakeNextStep = false;
    private int _playerChoise = 0;

    private float _timeCounter = 0f;
    private float _initialDelay = 0f;
    private float _autoSkipDelay = 0f;
    private bool _isAutoSkipEnable = true;

    void Start()
    {
        string actOnePath = Path.Combine(Path.Combine(Application.dataPath, "ForeverNotAlone", "Acts"), "ActOne.json");
        try
        {
            TaleAct act = ActLoader.LoadAct(actOnePath,
                startNodeExcecutor,
                charAppExecutor,
                choiseExecutor,
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
        if (_currentNode == null)
            return;

        _timeCounter += Time.deltaTime * 1000;
        _isCanMakeNextStep = _isCanMakeNextStep || IsPlayerMakeRandomClick() || IsAutoSkipTimeout();

        if (_currentNode != Node.EmptyNode && _isCanMakeNextStep)
        {
            ExecuteNextNode();
        }
    }

    private bool IsPlayerMakeRandomClick()
    {
        if (_timeCounter > _initialDelay)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    return true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsAutoSkipTimeout()
    {
        if (_isAutoSkipEnable && _timeCounter > _autoSkipDelay)
            return true;

        return false;
    }

    private void ExecuteNextNode()
    {
        _currentNode.Execute();
        _isCanMakeNextStep = false;
        _initialDelay = _currentNode.InitialDelay;
        _autoSkipDelay = _currentNode.AutoSkipDelay;
        _isAutoSkipEnable = !(_autoSkipDelay < 0.1);
        _currentNode = _currentNode.GetNextNode(_playerChoise);
        _playerChoise = 0;
    }

    public void OnPlayerMakeChose(int choise)
    {
        _playerChoise = choise;
        _isCanMakeNextStep = true;
    }
}
