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

        public void SetSelectedFileds()
        {

        }

        public void AttackMethod()
        {

        }

        public void MarineRadar()
        {

        }
    }
}
