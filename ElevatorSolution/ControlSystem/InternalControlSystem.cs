using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSolution.ControlSystem
{
    public class InternalControlSystem : ControlSystem
    {
        public InternalControlSystem(int numberOfFloors, int currentFloor) : base(numberOfFloors, currentFloor)
        {

        }

        public override void AddFloorToQueue(List<FloorQueueInfo> currentQueue, int currentElevatorFloor)
        {
            Console.WriteLine("Press button for the floor you want to go to:");
            int input = 0;
            int.TryParse(Console.ReadLine(), out input);
            if (input != 0)
            {

                var alreadyInQueue = currentQueue.Where(c => c.GetFloorQueueNumber() == currentElevatorFloor);
                if (alreadyInQueue.Any())
                {
                    Console.WriteLine("Button for floor " + input + " has already been pressed.");
                    return;
                }
                if (input == currentElevatorFloor)
                {
                    Console.WriteLine("You are on floor " + currentElevatorFloor + " already.");
                    return;
                }

                FloorQueueInfo newInfo;
                
                newInfo = new FloorQueueInfo(input,
                       GetCurrentDirection(input, currentElevatorFloor),
                       GetTimeToFloor(input, currentElevatorFloor));

                currentQueue.Add(newInfo);
                Console.WriteLine("Added " + input + " to queue");
                return;
            }
            Console.WriteLine("Invalid input");
            return;
        }

    }

}

