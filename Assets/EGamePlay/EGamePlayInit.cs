using EGamePlay;
using EGamePlay.Combat;
using ET;
using System.Threading;
using Sirenix.OdinInspector;


public class EGamePlayInit : SerializedMonoBehaviour
{
    public static EGamePlayInit Instance { get; private set; }
    public ReferenceCollector ConfigsCollector;
    public bool EntityLog;
    

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);
        Entity.EnableLog = EntityLog;
        MasterEntity.Create();
        Entity.Create<TimerManager>();
        Entity.Create<CombatContext>();
        MasterEntity.Instance.AddComponent<ConfigManageComponent>(ConfigsCollector);
    }

    private void Update()
    {
        ThreadSynchronizationContext.Instance.Update();
        MasterEntity.Instance.Update();
        TimerManager.Instance.Update();
    }

    private void OnApplicationQuit()
    {
        MasterEntity.Destroy();
    }
}
