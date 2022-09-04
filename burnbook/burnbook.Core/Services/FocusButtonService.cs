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

        data[currentDayIndex.ToString()] = JsonConvert.SerializeObject(model);
        SaveDay();
    }

    public void OnNewDay()
    {
        currentDayIndex++;
        data["currentDayIndex"] = currentDayIndex;
        UpdateCurrentDay(distractionCounter: 0);
        SaveDay();
    }

    public void SaveDay()
    {
        fileService.Save<Dictionary<string, object>>(dataPath, dataFileName, data);
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
}
