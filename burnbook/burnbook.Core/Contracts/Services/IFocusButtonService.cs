using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using burnbook.Core.Models;

namespace burnbook.Core.Contracts.Services;
public interface IFocusButtonService
{
    public void UpdateCurrentDay(int distractionCounter);
    public void OnNewDay();
    public void SaveDay();
    public void ClearAllDays();
    public FocusButtonModel GetCurrentDayData();
    public int GetDayCount();
}
