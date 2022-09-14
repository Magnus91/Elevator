using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorSolution.Elevator;
using ElevatorSolution.ControlSystem;

namespace ElevatorSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            ControlSystem.ControlSystem internalControlSystem = new InternalControlSystem(16, 5);
            ExternalControlSystem externalControlSystem = new ExternalControlSystem(16, 5, 1);

            externalControlSystem.AddFloorToQueue(externalControlSystem.GetCurrentQueue(), externalControlSystem.GetCurrentFloor());
            externalControlSystem.ChangeFloor(6);
            externalControlSystem.AddFloorToQueue(externalControlSystem.GetCurrentQueue(), externalControlSystem.GetCurrentFloor());
            externalControlSystem.ChangeFloor(3);
            externalControlSystem.AddFloorToQueue(externalControlSystem.GetCurrentQueue(), externalControlSystem.GetCurrentFloor());
            externalControlSystem.ChangeFloor(12);
            externalControlSystem.AddFloorToQueue(externalControlSystem.GetCurrentQueue(), externalControlSystem.GetCurrentFloor());
            Console.WriteLine("New elevator with 16 floors created. Starting on floor 5");
            Console.WriteLine("Button has been pressed on the outside of the elevator on floors 1, 6, 3 and 12");
            Console.ReadKey();

            while (true)
            {
                Console.WriteLine("Press ESC for emergency stop!");
                internalControlSystem.AddFloorToQueue(internalControlSystem.GetCurrentQueue(), internalControlSystem.GetCurrentFloor());
                internalControlSystem.SortQueue();
                foreach (var queuedFloor in internalControlSystem.GetCurrentQueue().ToList())
                {

                    if (!internalControlSystem.GoToFloor(queuedFloor))
                    {
                        Console.WriteLine("The elevator stopped on the way to floor: " + queuedFloor.GetFloorQueueNumber());
                        internalControlSystem.ClearQueue();
                        break;
                    }
                }
            }
        }
    }
}
