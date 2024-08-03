using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarshipsOnline
{
    public class Bot : Player
    {
        private Random random;
        public Bot(int sizeOfBoard) : base(sizeOfBoard)
        {
            random = new Random();
        }

        public string SelectRandomAttackFields(Player player)
        {
            var first = random.Next(0, sizeOfBoard);
            var second = random.Next(0, sizeOfBoard);
            while (player.OwnFields[first, second] != 0 && player.OwnFields[first, second] != 1)
            {
                first = random.Next(0, sizeOfBoard);
                second = random.Next(0, sizeOfBoard);
            }
            return $"{first}{second}";
        }
    }
}
