using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSolution.ControlSystem
{
    public abstract class ControlSystem
    {
        private int numberOfFloors;
        private int currentFloor;
        private static List<FloorQueueInfo> currentQueue;

        public ControlSystem(int numberOfFloors, int currentFloor)
        {
            this.numberOfFloors = numberOfFloors;
            this.currentFloor = currentFloor;
            currentQueue = new List<FloorQueueInfo>();
        }


        /// <summary>
        /// Gets current direction. True is up and false is down.
        /// </summary>
        /// <param name="currentFloor"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool GetCurrentDirection(int controlFloor, int currentElevatorFloor)
        {
            return currentElevatorFloor > controlFloor ? false : true;
        }

        /// <summary>
        /// Adds new floors to elevator queue. Different for external and internal systems.
        /// </summary>
        /// <param name="currentQueue">The floors currently queued</param>
        /// <param name="currentElevatorFloor">The floor the elevator is currently on</param>
        /// <returns></returns>
        public abstract void AddFloorToQueue(List<FloorQueueInfo> currentQueue, int currentElevatorFloor);


        /// <summary>
        /// Assume that one floor takes one unit of time to go to. 
        /// </summary>
        /// <param name="floorNumber"></param>
        /// <returns>time needed to go to selected floor</returns>
        public int GetTimeToFloor(int floorNumber, int currentElevatorFloor)
        {
            return Math.Abs(floorNumber - currentElevatorFloor);
        }
        /// <summary>
        /// Moves to a specific floor. If escape is pressed that counts as an emergency stop.
        /// Code for listening for key from here: https://stackoverflow.com/questions/5891538/listen-for-key-press-in-net-console-app
        /// Waits one second for each floor.
        /// </summary>
        /// <param name="floorQueueInfo"></param>
        /// <returns></returns>
        public bool GoToFloor(FloorQueueInfo floorQueueInfo)
        {
            if (floorQueueInfo.GetFloorQueueNumber() > 0 && floorQueueInfo.GetFloorQueueNumber() <= numberOfFloors)
            {
                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.WriteLine("Going " + floorQueueInfo.GetFloorQueueDirectionString() + " to floor " + floorQueueInfo.GetFloorQueueNumber().ToString() + ". currently at:" + currentFloor);
                        Thread.Sleep(1000);

                        if (currentFloor == floorQueueInfo.GetFloorQueueNumber())
                        {
                            RemoveFromQueue(floorQueueInfo);
                            Console.WriteLine("Arrived at floor: " + currentFloor);
                            return true;
                        }

                        if (floorQueueInfo.GetFloorQueueDirection())
                        {
                            currentFloor += 1;
                        }
                        else
                        {
                            currentFloor -= 1;
                        }
                    }

                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

                Console.WriteLine("Emergency Stop!");
                return false;

            }
            RemoveFromQueue(floorQueueInfo);
            Console.WriteLine("Could not go to floor " + floorQueueInfo.GetFloorQueueNumber().ToString());
            return false;

        }
        public int GetCurrentFloor()
        {
            return currentFloor;
        }
        public List<FloorQueueInfo> GetCurrentQueue()
        {
            return currentQueue;
        }
        public void RemoveFromQueue(FloorQueueInfo floorQueueInfo)
        {
            currentQueue.Remove(floorQueueInfo);
        }

        public void ClearQueue()
        {
            currentQueue = new List<FloorQueueInfo>();
        }

        /// <summary>
        /// Sorts current queue. First checks if the queue contains the same amount of requests for going up and down.
        /// If they are equal, check how much time it would take to go to the floors in the different directions and go the shortest way.
        /// </summary>
        public void SortQueue()
        {
            var upRequests = currentQueue.Count(c => c.GetFloorQueueDirection() == true);
            var downRequests = currentQueue.Count(c => c.GetFloorQueueDirection() == false);
            if (upRequests > downRequests)
            {
                currentQueue = currentQueue.OrderByDescending(c => c.GetFloorQueueDirection() == true).ThenBy(c => c.GetFloorQueueTimeToFloor()).ToList();
            }
            else if (upRequests == downRequests)
            {
                var totalUpRequestTime = currentQueue.Where(c => c.GetFloorQueueDirection() == true).Sum(c => c.GetFloorQueueTimeToFloor());
                var totalDownRequestTime = currentQueue.Where(c => c.GetFloorQueueDirection() == false).Sum(c => c.GetFloorQueueTimeToFloor());

                if (totalUpRequestTime < totalDownRequestTime)
                {
                    currentQueue = currentQueue.OrderByDescending(c => c.GetFloorQueueDirection() == true).ThenBy(c => c.GetFloorQueueTimeToFloor()).ToList();
                }
                else
                {
                    currentQueue = currentQueue.OrderByDescending(c => c.GetFloorQueueDirection() == false).ThenBy(c => c.GetFloorQueueTimeToFloor()).ToList();
                }
            }
            else
            {
                currentQueue = currentQueue.OrderByDescending(c => c.GetFloorQueueDirection() == false).ThenBy(c => c.GetFloorQueueTimeToFloor()).ToList();
            }
        }

        /// <summary>
        /// Class for information needed in a queue.
        /// </summary>
        public class FloorQueueInfo
        {
            private int floorNumber = 0;
            private bool direction = false;
            private int timeToFloor = 0;

            public FloorQueueInfo(int floorNumber, bool direction, int timeToFloor)
            {
                this.floorNumber = floorNumber;
                this.direction = direction;
                this.timeToFloor = timeToFloor;
            }
            public int GetFloorQueueNumber()
            {
                return floorNumber;
            }
            public string GetFloorQueueDirectionString()
            {
                return direction == true ? "up" : "down";
            }
            public bool GetFloorQueueDirection()
            {
                return direction;
            }
            public int GetFloorQueueTimeToFloor()
            {
                return timeToFloor;
            }
            public void UpdateFloorQueueTimeToFloor(int addedTimeToFloor)
            {
                timeToFloor += addedTimeToFloor;
            }

        }

    }
}
