using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineCollection<T>
{
    private CallbackCollector _callbackCollector;
    private List<ICoroutineCollectionWriter<T>> _writersList;
    private List<Coroutine> _coroutineList;

    public CoroutineCollection()
    {
        _callbackCollector = new CallbackCollector();
        _writersList = new List<ICoroutineCollectionWriter<T>>();
        _coroutineList = new List<Coroutine>();
    }

    public void AddCoroutine(ICoroutineCollectionWriter<T> collectionWriter)
    {
        if (_callbackCollector.IsRunning)
        {
            throw new Exception();
        }
        _writersList.Add(collectionWriter);
    }

    public void RemoveCoroutine(ICoroutineCollectionWriter<T> collectionWriter)
    {
        if (_callbackCollector.IsRunning)
        {
            throw new Exception();
        }
        _writersList.Remove(collectionWriter);
    }

    public void Run(Action onComplete, T arg)
    {

        for (int i = 0; i < _writersList.Count; i++)
        {
            _coroutineList.Add
            (
                CoroutineInvoker.Instance.StartCoroutine(
                    _writersList[i].Coroutine(_callbackCollector.AddCallback(), arg))
            );
        }

        _callbackCollector.SetReady(() =>
        {
            onComplete.SafeInvoke();
            _coroutineList.Clear();
        });
    }

    public void ForceStop()
    {
        if (_callbackCollector.IsRunning)
        {
            for (int i = 0; i < _coroutineList.Count; i++)
            {
                CoroutineInvoker.Instance.StopCoroutine(_coroutineList[i]);
            }
            _callbackCollector.ForceStop();
            _coroutineList.Clear();
        }
    }

    public bool IsRunning
    {
        get
        {
            return _callbackCollector.IsRunning;
        }
    }
}

public interface ICoroutineCollectionWriter<T>
{
    IEnumerator Coroutine(Action onComplete, T args);
}

