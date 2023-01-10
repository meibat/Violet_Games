using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Data.Enums
{
    public enum PerfilEnum
    {
        Admin = 1,
        Stand = 2
    }

    public enum Office{
        Boss = 1,
        Employee = 2
    }

    public enum StatusConsole
    {
        Active = 1,
        Pending = 2,
        Disable = 3
    }

    public enum StatusPayment
    {
        Paid = 1,
        Pending = 2,
        Overdue = 3
    }
}
