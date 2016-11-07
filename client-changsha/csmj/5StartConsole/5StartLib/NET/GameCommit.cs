using KWX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KWX
{
    public enum CurSocket
    {
        Login_,
        Game_,
        NONO_,
    }
    public delegate void LoginGameFrom(BufferData Msg);
    public delegate void GameRoomFrom(BufferData Msg);
}
