﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunalipse.Resource.Generic
{
    public class Delegates
    {
        public delegate void ChuckOperated(int operated, int total);
        public delegate void EndpointReached(params object[] args);
        public delegate void SingleEndpointReached();

        public static ChuckOperated OnChuckOperated;
        public static EndpointReached OnEndpointReached;
        public static SingleEndpointReached OnSingleEndpointReached;
    }
}
