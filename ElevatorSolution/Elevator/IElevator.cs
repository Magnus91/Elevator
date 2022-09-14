using ElevatorSolution.ControlSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSolution.Elevator
{
    public interface IElevator
    {
        InternalControlSystem GetInternalControlSystem();

        ExternalControlSystem GetExternalControlSystem();
    }
}
