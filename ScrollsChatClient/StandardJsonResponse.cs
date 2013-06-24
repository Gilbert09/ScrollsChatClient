using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrollsChatClient
{
    class StandardJsonResponse
    {
        public string msg;
    }

    class Op
    {
        public string op;
        public string msg;
    }

    class Profile
    {
        public string id;
        public string name;
        public bool acceptChallenges;
        public bool acceptTrades;
        public string adminRole;
    }

    class RoomEnter
    {
        public string roomName;
        public string msg;
    }

    class RoomExit
    {
        public string roomName;
        public string msg;
    }

    class RoomInfo
    {
        public string roomName;
        public List<Profile> profiles;
        public string msg;
    }

    class RoomChatMessage
    {
        public string from;
        public string text;
        public string roomName;
        public string msg;
    }
}
