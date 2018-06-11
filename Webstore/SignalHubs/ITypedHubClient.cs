using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore.SignalHubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
