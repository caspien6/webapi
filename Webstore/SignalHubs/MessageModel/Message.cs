﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore.SignalHubs.MessageModel
{
    public class Message
    {
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
