using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using burnbook.Core.Contracts.Services;
using burnbook.Core.Helpers;
using burnbook.Core.Models;
using Newtonsoft.Json;

namespace burnbook.Core.Services;

//Singleton added to app services
public class FocusButtonService : IFocusButtonService
{
    readonly FileService fileService = new();

    public readonly string dataPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public readonly string dataFileName = "focusbuttonmodel.json";

    public Dictionary<string, object> data;
    public int currentDayIndex = 0;

    public Dictionary<string, bool> morningRoutine = new();
    public TimeSpan wakeupTime = new();

    public FocusButtonService()
    {
        data = fileService.Read<Dictionary<string, object>>(dataPath, dataFileName)
            ?? HandleNewDatabase();
        currentDayIndex = int.Parse(String.Format("{0}", data["currentDayIndex"]));
    }

    Dictionary<string, object> HandleNewDatabase()
    {
        var result = new Dictionary<string, object>();
        result.Add("currentDayIndex", 0);
        data = result;
        UpdateCurrentDay(distractionCounter: 0);
        return result;
    }

    public void UpdateCurrentDay(int distractionCounter)
    {
        var model = new FocusButtonModel();
        model.DistractionCounter = distractionCounter;
        model.dateTime = DateTime.Now;
        model.MorningRoutine = morningRoutine;
        model.WakeupTime = wakeupTime;

        data[currentDayIndex.ToString()] = JsonConvert.SerializeObject(model);
        fileService.Save<Dictionary<string, object>>(dataPath, dataFileName, data);
    }

    public void OnNewDay()
    {
        currentDayIndex++;
        data["currentDayIndex"] = currentDayIndex;
        morningRoutine = new();
        UpdateCurrentDay(distractionCounter: 0);
    }

    public void SaveDay()
    {
        UpdateCurrentDay(distractionCounter: GetCurrentDayData().DistractionCounter);
    }

    public void ClearAllDays()
    {
        data.Clear();
        currentDayIndex = 0;
        data = HandleNewDatabase();
        fileService.Delete(dataPath, dataFileName);
    }

    public FocusButtonModel GetCurrentDayData()
    {
        return JsonConvert.DeserializeObject<FocusButtonModel>(data[currentDayIndex.ToString()] as string); 
    }

    public int GetDayCount() => currentDayIndex + 1;
    public void UpdateMorningRoutine(string key,bool val) => morningRoutine[key] = val;
    public bool GetCheckboxValue(string key) => GetCurrentDayData().MorningRoutine.ContainsKey(key) ? GetCurrentDayData().MorningRoutine[key] : false;
    public void UpdateWakeupTime(TimeSpan time) => wakeupTime = time;
}
