using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSolution.ControlSystem
{
    public class ExternalControlSystem : ControlSystem
    {
        private int controlFloor;
        public ExternalControlSystem(int numberOfFloors, int currentFloor, int controlFloor) : base(numberOfFloors, currentFloor)
        {
            this.controlFloor = controlFloor;
        }

        public override void AddFloorToQueue(List<FloorQueueInfo> currentQueue, int currentElevatorFloor)
        {
            var alreadyInQueue = currentQueue.Where(c => c.GetFloorQueueNumber() == currentElevatorFloor);
            if (alreadyInQueue.Any())
            {
                return;
            }
            FloorQueueInfo newInfo;

            newInfo = new FloorQueueInfo(controlFloor,
                    GetCurrentDirection(controlFloor, currentElevatorFloor),
                    GetTimeToFloor(controlFloor, currentElevatorFloor));

            currentQueue.Add(newInfo);
            return;
        }

        public void ChangeFloor(int floor)
        {
            controlFloor = floor;
        }


    }
}
