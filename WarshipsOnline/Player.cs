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
        protected int sizeOfBoard;

        public Player(int sizeOfBoard)
        {
            this.sizeOfBoard = sizeOfBoard;

            OwnFields = new int[sizeOfBoard, sizeOfBoard];
            for (int i = 0; i < sizeOfBoard; i++)
                for (int j = 0; j < sizeOfBoard; j++)
                    OwnFields[i, j] = 0;

            HitAttacksID = new List<string>();  
        }

        public void SetSelectedFileds(List<string> stringID)
        {
            for(int i = 0;i < stringID.Count; i++)
            {
                var firstStr = stringID[i].Substring(0,1);
                var secondStr = stringID[i].Substring(1,1);
                if(int.TryParse(firstStr,out var first) && int.TryParse(secondStr,out var second))
                {
                    OwnFields[first, second] = 1;
                }
            }
        }

        public void MarineRadar()
        {

        }
    }
}
