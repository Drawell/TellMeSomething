using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DialogGraph;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<TextAsset> ActsJson;
    public DefaultStartNodeExecutor startNodeExcecutor;
    public DefaultCharAppExecutor charAppExecutor;
    public DefaultCharDisappExecutor charDisappExecutor;
    public DefaultChoiceExecutor choiseExecutor;
    public DefaultReplicaExecutor replicaExecutor;
    public DefaultAddItemNodeExecutor addItemNodeExecutor;
    public DefaultSwitchByItemExecutor switchByItemExecutor;
    public DefaultSetLandscapeExecutor setLandscapeExecutor;
    public DefaultEndNodeExecutor endNodeExecutor;

    private Node _currentNode;
    private Node _nextNode;
    private bool _isCanMakeNextStep = false;
    private float _timeCounter = 0f;

    //Сначала идет _initialDelay, который нельзя пропустить, потом _autoSkipDelay, по истечению его
    //Исполняется слудующий Node. Если он меньше 0, то переход к следующему Node только по щелчку пользователя.
    //То автопропуск отключен.
    private float _initialDelay = 0f;
    private float _autoSkipDelay = 0f;
    private bool _isAutoSkipEnable = true;

    private bool _crushed = false;

    void Start()
    {
        try
        {
            TaleAct act = ActLoader.LoadAct(ActsJson[0].text,
                startNodeExcecutor,
                charAppExecutor,
                charDisappExecutor,
                choiseExecutor,
                replicaExecutor,
                addItemNodeExecutor,
                switchByItemExecutor,
                setLandscapeExecutor,
                endNodeExecutor);

            _nextNode = act.GetStartNode();
            _isCanMakeNextStep = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            _crushed = true;
        }
    }

    void Update()
    {
        if (_crushed)
            return;

        _timeCounter += Time.deltaTime * 1000;
        _isCanMakeNextStep = _isCanMakeNextStep || IsPlayerMakeRandomClick() || IsAutoSkipTimeout();

        if (_nextNode != Node.EmptyNode && _nextNode != null && _isCanMakeNextStep)
        {
            ExecuteNode(_nextNode);
            _nextNode = _currentNode.GetNextNode();
        }
        else if (_nextNode == Node.EmptyNode || _nextNode == null)
        {
            _crushed = true;
            Debug.Log("Node is empty or Null");
        }

    }

    private bool IsPlayerMakeRandomClick()
    {
        if (_timeCounter > _initialDelay && _isAutoSkipEnable)
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
        if (_isAutoSkipEnable && _timeCounter > _autoSkipDelay + _initialDelay)
            return true;

        return false;
    }

    private void ExecuteNode(Node node)
    {
        _currentNode = node;
        _isCanMakeNextStep = false;
        _initialDelay = _currentNode.InitialDelay;
        _autoSkipDelay = _currentNode.AutoSkipDelay;
        _isAutoSkipEnable = _autoSkipDelay > -0.01;
        _timeCounter = 0;
        try
        {
            _currentNode.Execute();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            _crushed = true;
        }
    }

    public void OnPlayerMakeChose(int choise)
    {
        _nextNode = _currentNode.GetNextNode(choise);
        _isCanMakeNextStep = true;
    }
}
