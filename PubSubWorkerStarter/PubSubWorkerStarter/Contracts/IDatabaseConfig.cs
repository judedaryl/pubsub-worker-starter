﻿using System;
using System.Data.Common;

namespace PubSubWorkerStarter.Contracts
{
    public interface IDatabaseConfig
    {
        Func<DbConnection> GetConnectionFactory();
    }
}