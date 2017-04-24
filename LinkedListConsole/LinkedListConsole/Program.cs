using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListConsole
{
    class Program
    {
        public static LinkedList<Score> ScoreBoard = new LinkedList<Score>();

        

        static void Main(string[] args)
        {
            OrderedInsert(ScoreBoard, new Score { PlayerName = "PP", score = 10 });
            OrderedInsert(ScoreBoard, new Score { PlayerName = "BB", score = 30 });
            OrderedInsert(ScoreBoard, new Score { PlayerName = "AA", score = 20 });

            PrintScores();
            Console.ReadKey();
        }

        static void PrintScores()
        {
            LinkedListNode<Score> node = ScoreBoard.First;
             
            while(node != null)
            {
                Console.WriteLine("Player {0} Score {1}", node.Value.PlayerName, node.Value.score);
                node = node.Next;
            }
        }

        static public void OrderedInsert(LinkedList<Score> list, Score newScore  )
        {
            LinkedListNode<Score> node = list.First;
            while(node != null && node.Value.score <= newScore.score )
            {
                node = node.Next;
            }
            if (node == null && list.First == null)
                list.AddFirst(newScore);
            else if (node == null)
            {
                list.AddAfter(list.Last, newScore);
            }
            else list.AddBefore(node, newScore);

            }
        }
    
}
