using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMAReportCleaner
{
    public class Stats
    {
        public int sameFiles = 0;
        public int differentFiles = 0;
        public int missingFiles = 0;
        public TimeSpan elapsedTime = TimeSpan.MinValue;
    }
}
