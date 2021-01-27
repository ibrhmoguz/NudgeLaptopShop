using System;
using System.Collections.Generic;
using System.Text;

namespace Nudge.Logical.Imp
{
    public class ConsecutiveNumber
    {
        public bool IsConsecutive(string series)
        {
            // Throw error if string has any non-integer character
            foreach (var item in series)
            {
                if (!int.TryParse(item.ToString(), out _))
                {
                    throw new ArgumentException("Please enter set of numbers!");
                }
            }

            var isConsecutive = true;
            var groups = new List<int>();
            var sb = new StringBuilder();

            var tempSeries = series[0];
            for (var i = 1; i < series.Length; i++)
            {
                if (tempSeries < series[i])
                {
                    sb.Append(tempSeries);
                }
                else
                {
                    sb.Append(tempSeries);
                    groups.Add(int.Parse(sb.ToString()));
                    sb.Clear();
                }

                // move next character
                tempSeries = series[i];
            }

            // Check group has any item and items are consecutive.
            if (groups.Count > 0)
            {
                var temp = groups[0];
                for (var i = 1; i < groups.Count; i++)
                {
                    if (temp + 1 == groups[i])
                    {
                        temp = groups[i];
                    }
                    else
                    {
                        isConsecutive = false;
                        break;
                    }
                }
            }

            return isConsecutive;
        }
    }
}
