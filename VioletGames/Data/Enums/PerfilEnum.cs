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

    public enum StatusLocation
    {
        Ativo = 1,
        Pedente = 2,
        Desativado = 3
    }

    public enum StatusPayment
    {
        Pago = 1,
        Pendente = 2,
        Nao_Pago = 3
    }
}
