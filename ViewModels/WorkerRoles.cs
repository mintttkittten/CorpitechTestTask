using System;
using System.Collections.Generic;
using TestTask.Models;

namespace TestTask.ViewModels
{
    public static class WorkerRoles
    {
        public static IEnumerable<WorkerRole> All => Enum.GetValues<WorkerRole>();
    }
}
