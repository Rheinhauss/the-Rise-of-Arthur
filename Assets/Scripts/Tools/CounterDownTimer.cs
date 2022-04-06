using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʱ����
/// </summary>
public sealed class CountDownTimer
{
    public bool IsAutoCycle { get; private set; }                   // �Ƿ��Զ�ѭ����С�ڵ���0�����ã�
    public bool IsStoped { get; private set; }                      // �Ƿ��Ƿ���ͣ��
    public float CurrentTime { get { return UpdateCurrentTime(); } }// ��ǰʱ��
    public bool IsTimeUp { get { return CurrentTime <= 0; } }       // �Ƿ�ʱ�䵽
    public float Duration { get; private set; }                     // ��ʱʱ�䳤��

    private float lastTime;                                         // ��һ�θ��µ�ʱ��
    private int lastUpdateFrame;                                    // ��һ�θ��µ���ʱ��֡��������һ֡��θ��¼�ʱ��
    private float currentTime;                                      // ��ǰ��ʱ��ʣ��ʱ��
    /// <summary>
    /// ����ʱ�����¼�������Ϊʣ��ʱ��
    /// </summary>
    public List<Action<float>> UpdateAction = new List<Action<float>>();
    public List<Action> EndActions = new List<Action>();
    private bool IsEnd = false;

    /// <summary>
    /// ���쵹��ʱ��
    /// </summary>
    /// <param name="duration">��ʼʱ��</param>
    /// <param name="autocycle">�Ƿ��Զ�ѭ��</param>
    public CountDownTimer(float duration, bool autocycle = false, bool autoStart = true)
    {
        IsStoped = true;
        Duration = Mathf.Max(0f, duration);
        IsAutoCycle = autocycle;
        Reset(duration, !autoStart);
        CounterDownTimerMgr.Instance.AddTimer(this);
    }

    ~CountDownTimer()
    {
        CounterDownTimerMgr.Instance.RemoveTimer(this);
    }

    /// <summary>
    /// ���¼�ʱ��ʱ��
    /// </summary>
    /// <returns>����ʣ��ʱ��</returns>
    public float UpdateCurrentTime()
    {
        if (IsStoped || lastUpdateFrame == Time.frameCount)         // ��ͣ�˻��Ѿ���һ֡���¹��ˣ�ֱ�ӷ���
            return currentTime;
        //����
        if (currentTime <= 0)                                       // С�ڵ���0ֱ�ӷ��أ����ѭ���Ǿ�����ʱ��
        {
            if (IsAutoCycle)
                Reset(Duration, false);
            if (IsEnd)
                return currentTime;
            IsEnd = true;
            foreach(var action in EndActions)
            {
                action.Invoke();
            }
            return currentTime;
        }
        //����ʱ
        float countTime = Time.fixedDeltaTime;
        currentTime -= countTime;
        foreach (var action in UpdateAction)
        {
            action?.Invoke(currentTime);
        }
        UpdateLastTimeInfo();
        return currentTime;
    }

    /// <summary>
    /// ����ʱ������Ϣ
    /// </summary>
    private void UpdateLastTimeInfo()
    {
        lastTime = Time.time;
        lastUpdateFrame = Time.frameCount;
    }

    /// <summary>
    /// ��ʼ��ʱ��ȡ����ͣ״̬
    /// </summary>
    public void Start()
    {
        Reset(Duration, false);
    }

    /// <summary>
    /// ���ü�ʱ��
    /// </summary>
    /// <param name="duration">����ʱ��</param>
    /// <param name="isStoped">�Ƿ���ͣ</param>
    public void Reset(float duration, bool isStoped = false)
    {
        IsEnd = false;
        UpdateLastTimeInfo();
        Duration = Mathf.Max(0f, duration);
        currentTime = Duration;
        IsStoped = isStoped;
    }

    /// <summary>
    /// ��ͣ
    /// </summary>
    public void Pause()
    {
        UpdateCurrentTime();    // ��ͣǰ�ȸ���һ��
        IsStoped = true;
    }

    /// <summary>
    /// ������ȡ����ͣ��
    /// </summary>
    public void Continue()
    {
        UpdateLastTimeInfo();   // ����ǰ�ȸ��µ�ǰʱ����Ϣ
        IsStoped = false;
    }

    /// <summary>
    /// ��ֹ����ͣ�����õ�ǰֵΪ0
    /// </summary>
    public void End()
    {
        IsStoped = true;
        currentTime = 0f;
    }

    /// <summary>
    /// ��ȡ����ʱ����ʣ�0Ϊû��ʼ��ʱ��1Ϊ��ʱ������
    /// </summary>
    /// <returns></returns>
    public float GetPercent()
    {
        UpdateCurrentTime();
        if (currentTime <= 0 || Duration <= 0)
            return 1f;
        return 1f - currentTime / Duration;
    }

}

