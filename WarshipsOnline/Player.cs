using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipsOnline
{
    public class Player
    {
        public int[,] OwnFields { get; set; }
        public List<string> HitAttacksID { get; set; }
        public int CountUseMarineRadar { get; set; }
        protected int sizeOfBoard;

        public Player(int sizeOfBoard, int CountUseMarineRadar)
        {
            this.sizeOfBoard = sizeOfBoard;
            this.CountUseMarineRadar = CountUseMarineRadar;

            OwnFields = new int[sizeOfBoard, sizeOfBoard];
            for (int i = 0; i < sizeOfBoard; i++)
                for (int j = 0; j < sizeOfBoard; j++)
                    OwnFields[i, j] = 0;

            HitAttacksID = new List<string>();  
        }

        public void SetSelectedFileds(List<string> stringButtonsID)
        {
            for(int i = 0;i < stringButtonsID.Count; i++)
            {
                var rowIDStr = stringButtonsID[i].Substring(0,1);
                var columnIDStr = stringButtonsID[i].Substring(1,1);
                if(int.TryParse(rowIDStr,out var first) && int.TryParse(columnIDStr, out var columnID))
                {
                    OwnFields[first, columnID] = 1;
                }
            }
        }

        public string MarineRadar()
        {
            int[,] checkedFields = new int[sizeOfBoard, sizeOfBoard];
            Dictionary<int, int> shipSizes = new Dictionary<int, int>();

            for (int i = 0; i < sizeOfBoard; i++)
            {
                for (int j = 0; j < sizeOfBoard; j++)
                {
                    if (OwnFields[i, j] == 1 && checkedFields[i, j] != 1)
                    {
                        int shipSize = 0;
                        Queue<(int, int)> queue = new Queue<(int, int)>();
                        queue.Enqueue((i, j));

                        while (queue.Count > 0)
                        {
                            (int x, int y) = queue.Dequeue();
                            if (x >= 0 && x < sizeOfBoard && y >= 0 && y < sizeOfBoard && OwnFields[x, y] == 1 && checkedFields[x, y] != 1)
                            {
                                checkedFields[x, y] = 1;
                                shipSize++;
                                queue.Enqueue((x - 1, y));
                                queue.Enqueue((x + 1, y));
                                queue.Enqueue((x, y - 1));
                                queue.Enqueue((x, y + 1));
                            }
                        }

                        if (shipSizes.ContainsKey(shipSize))
                        {
                            shipSizes[shipSize]++;
                        }
                        else
                        {
                            shipSizes.Add(shipSize, 1);
                        }
                    }
                }
            }

            string result = "";
            foreach (var pair in shipSizes)
            {
                result += $"{pair.Value}*{new String('x', pair.Key)}, ";
            }

            return result.TrimEnd(',', ' ');
        }

    }
}
