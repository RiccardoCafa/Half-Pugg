using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public enum MessageStatus : byte
    {
        None = 1,
        Altered = 2,
        Deleted = 4,
        NoReceived = 8
    }
}