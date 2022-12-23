using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Util.SendEmail
{
    public interface IEmail
    {
        bool Send(string email, string content, string message);
    }
}
