using BoxMirror.Service.Models;
using System.Linq;

namespace BoxMirror.Service.Services
{
    /*
     *  Sample Input

    5,4
    -1
    1,2RR
    3,2L
    -1
    1,0V
    -1 

     */
    public class BoxLaserService
    {
        /// <summary>
        /// Process the laser on input
        /// </summary>
        /// <param name="boxInput"></param>
        /// <returns></returns>
        public Room ProcessLaser(BoxInput boxInput)
        {
            Room[,] house = new Room[boxInput.Columns, boxInput.Rows];

            // Construct house 
            for (int column = 0; column < boxInput.Columns; column++)
            {
                for (int row = 0; row < boxInput.Rows; row++)
                {
               
                    Mirror mirror = boxInput.Mirrors.SingleOrDefault(m => m.Column == column && m.Row == row);
                    house[column, row] = new Room() { Column = column, Row = row, Mirror = mirror };
                }
            }

            // Assign laser orientation
            switch (boxInput.Entry.Orientation)
            {
                case "V":
                    if (boxInput.Entry.Row == 0)
                        house[boxInput.Entry.Column, boxInput.Entry.Row ].Direction = "+";
                    else
                        house[boxInput.Entry.Column, boxInput.Entry.Row ].Direction = "-";
                    break;

                case "H":
                    if (boxInput.Entry.Column == 0)
                        house[boxInput.Entry.Column, boxInput.Entry.Row].Direction = "+";
                    else
                        house[boxInput.Entry.Column, boxInput.Entry.Row].Direction = "-";
                    break;
            }
            house[boxInput.Entry.Column, boxInput.Entry.Row].Orientation = boxInput.Entry.Orientation;
            Room previousRoom = null;
            Room currentRoom = house[boxInput.Entry.Column, boxInput.Entry.Row];
            while (currentRoom != null)
            {
                previousRoom = currentRoom;
                currentRoom = NextRoom(house, currentRoom, boxInput.Columns, boxInput.Rows);
            }
            return previousRoom;
        }

        /// <summary>
        /// Find next room, if exit return null
        /// </summary>
        /// <param name="house"></param>
        /// <param name="currentRoom"></param>
        /// <param name="totalColumns"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        private Room NextRoom(Room[,] house, Room currentRoom,int totalColumns, int totalRows)
        {
            int nextRow =0 , nextColumn =0;
            string nextDirection = "";
            string nextOrientation = "";

            //Check does whether current room has mirror
            switch (currentRoom.Orientation + currentRoom.Direction)
            {
                case "V+":
                    if (currentRoom.Mirror == null)
                    {
                        nextRow = currentRoom.Row + 1;
                        nextColumn = currentRoom.Column;
                        nextOrientation = "V";
                        nextDirection = "+";
                    }
                    else
                    {
                        switch (currentRoom.Mirror.Leaning + currentRoom.Mirror.Reflection)
                        {
                            case "RR":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column + 1;
                                nextOrientation = "H";
                                nextDirection = "+";
                                break;
                            case "RL":
                                nextRow = currentRoom.Row + 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "+";
                                break;
                            case "RRL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column + 1;
                                nextOrientation = "H";
                                nextDirection = "+";
                                break;
                            case "LR":
                                nextRow = currentRoom.Row + 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "+";
                                break;
                            case "LL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column - 1;
                                nextOrientation = "H";
                                nextDirection = "-";
                                break;
                            case "LRL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column - 1;
                                nextOrientation = "H";
                                nextDirection = "-";
                                break;
                        }
                    }
                    break;
                case "V-":
                    if (currentRoom.Mirror == null)
                    {
                        nextRow = currentRoom.Row - 1;
                        nextColumn = currentRoom.Column;
                        nextOrientation = "V";
                        nextDirection = "-";
                    }
                    else
                    {
                        switch (currentRoom.Mirror.Leaning + currentRoom.Mirror.Reflection)
                        {
                            case "RR":
                                nextRow = currentRoom.Row - 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "-";
                                break;
                            case "RL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column - 1;
                                nextOrientation = "H";
                                nextDirection = "-";
                                break;
                            case "RRL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column - 1;
                                nextOrientation = "H";
                                nextDirection = "-";
                                break;
                            case "LR":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column + 1;
                                nextOrientation = "H";
                                nextDirection = "+";
                                break;
                            case "LL":
                                nextRow = currentRoom.Row - 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "-";
                                break;
                            case "LRL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column + 1;
                                nextOrientation = "H";
                                nextDirection = "+";
                                break;
                        }
                    }
                    break;
                case "H+":
                    if (currentRoom.Mirror == null)
                    {
                        nextRow = currentRoom.Row;
                        nextColumn = currentRoom.Column + 1;
                        nextOrientation = "H";
                        nextDirection = "+";
                    }
                    else
                    {
                        switch (currentRoom.Mirror.Leaning + currentRoom.Mirror.Reflection)
                        {
                            case "RR":
                                nextRow = currentRoom.Row ;
                                nextColumn = currentRoom.Column + 1;
                                nextOrientation = "H";
                                nextDirection = "+"; 
                                break;
                            case "RL":
                                nextRow = currentRoom.Row + 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "+";
                                break;
                            case "RRL":
                                nextRow = currentRoom.Row + 1;
                                nextColumn = currentRoom.Column ;
                                nextOrientation = "V";
                                nextDirection = "+";
                                break;
                            case "LR":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column + 1;
                                nextOrientation = "H";
                                nextDirection = "+";
                                break;
                            case "LL":
                                nextRow = currentRoom.Row - 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "-";
                                break;
                            case "LRL":
                                nextRow = currentRoom.Row - 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "-";
                                break;
                        }
                    }
                    break;
                case "H-":
                    if (currentRoom.Mirror == null)
                    {
                        nextRow = currentRoom.Row;
                        nextColumn = currentRoom.Column - 1;
                        nextOrientation = "H";
                        nextDirection = "-";
                    }
                    else
                    {
                        switch (currentRoom.Mirror.Leaning + currentRoom.Mirror.Reflection)
                        {
                            case "RR":
                                nextRow = currentRoom.Row - 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "-";
                                break;
                            case "RL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column - 1;
                                nextOrientation = "H";
                                nextDirection = "-";
                                break;
                            case "RRL":
                                nextRow = currentRoom.Row - 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "-";
                                break;
                            case "LR":
                                nextRow = currentRoom.Row + 1;
                                nextColumn = currentRoom.Column ;
                                nextOrientation = "V";
                                nextDirection = "+";
                                break;
                            case "LL":
                                nextRow = currentRoom.Row;
                                nextColumn = currentRoom.Column - 1;
                                nextOrientation = "H";
                                nextDirection = "-";

                                break;
                            case "LRL":
                                nextRow = currentRoom.Row + 1;
                                nextColumn = currentRoom.Column;
                                nextOrientation = "V";
                                nextDirection = "+";
                                break;
                        }
                    }
                    break;
            }

            // if next room is not in the house boundry , return the result
            if(nextRow >= totalRows || nextColumn >= totalColumns || nextRow <0 || nextColumn < 0)
            {
                return null;
            }
            else
            {
                house[nextColumn, nextRow].Direction = nextDirection;
                house[nextColumn, nextRow].Orientation = nextOrientation;
                return house[nextColumn, nextRow];
            }
        }
    }
}
