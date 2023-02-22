using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Util.Clean
{
    public class Clean
    {
        public static string Date(string dateForClean)
        {
            return dateForClean.Replace("00:00:00", "");
        }
    }
}
