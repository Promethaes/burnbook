using System.Runtime.Serialization;

namespace burnbook.Core.Models;

[Serializable]
public class FocusButtonModel
{
    [DataMember]
    public int DistractionCounter
    {
        get; set;
    }
    [DataMember]
    public DateTime dateTime
    {
        get; set;
    }
    [DataMember]
    public Dictionary<string, bool> MorningRoutine = new();
    [DataMember]
    public TimeSpan WakeupTime;
}
