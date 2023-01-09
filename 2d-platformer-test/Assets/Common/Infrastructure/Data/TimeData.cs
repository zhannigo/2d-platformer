using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Infrastructure.Data
{
  [Serializable]
  public class TimeData
  {
    public List<float> gameTimes = new List<float>();

    public void Add(float newTime)
    {
      gameTimes.Add(newTime);
      if (gameTimes.Count > 3)
      {
        gameTimes.Remove(gameTimes.Min());
      }
    }

    public float GetBestTime() => 
      gameTimes.Max();
  }
}