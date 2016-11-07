using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using KWX;
namespace KWX_From
{
    public interface LoginInterface
    {
        void LoginOK(RSP_Login result);

        void EntCartRoom(RSP_CreateRoom result);

        void EntRoom(RSP_EnterRoom result);

        void LinkLoginServerOK();
    }
    public interface GameInterface
    {
        void EntServer(RSP_LoginServer result);

        void HearBeat(RSP_HeartBeat result);

        void ExitRoom(RSP_ExitRoom result);

        void OnRoomUserInfo(NTF_RoomUserInfo result);

        void OnSameUserLogin(NTF_SameUserLogin result);

        void OnRoomUserOnline(NTF_RoomUserOnline result);

        void OnRoomUserOffline(NTF_RoomUserOffline result);

        void OnRoomDismissed(NTF_RoomDismissed result);

        void OnMessage(NTF_Message result);

        void DismisseMessage(NTF_Dismiss result);

        void OnGAME_STATE(PB_Server_Room_Info result);

        void OnSingleResult(PB_Server_Single_Bill result);

        void OnAllResult(PB_Server_ALL_Bill_Info result);

        void LinkGameServerOK();
    }
}