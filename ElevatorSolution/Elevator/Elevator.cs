using ElevatorSolution.ControlSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSolution.Elevator
{
    public class Elevator : IElevator
    {
        

        private InternalControlSystem internalControlSystem;
        private ExternalControlSystem externalControlSystem;


        public Elevator(InternalControlSystem internalControlSystem, ExternalControlSystem externalControlSystem)
        {
            
            this.internalControlSystem = internalControlSystem;
            this.externalControlSystem = externalControlSystem;
            

        }

        public InternalControlSystem GetInternalControlSystem()
        {
            return internalControlSystem;
        }
        public ExternalControlSystem GetExternalControlSystem()
        {
            return externalControlSystem;
        }

       
    }
}
