using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Data.Enums
{
    public enum CategoryProduct //Produto
    {
        Game = 1,
        Console = 2,
        Diversos = 3
    }

    public enum Plan //Cliente
    {
        Free = 0,
        Stand = 25,
        Premium = 50
    }

    public enum CategoryConsole //Console
    {
        Ps1 = 1,
        Ps2 = 2,
        Ps3 = 3,
        Ps4 = 4,
        Ps5 = 5,
        Wii = 6,
        Pc = 7,
        Nitendo64 = 64,
        Xbox360 = 360,
        XboxOne = 11,
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
    }
}
