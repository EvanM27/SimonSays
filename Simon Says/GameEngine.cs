using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon_Says
{
    class GameEngine
    {
        //used to determine difficulty scaling
        private int turnCounter;

        //used to determine the end of a players turn
        int pressCount;

        //computer pattern to match
        private List<int> Sequence;

        //storage for player choices
        public List<int> playerChoices;

        //simple initializer
        public GameEngine()
        {
            turnCounter = 0;
            pressCount = 0;

            Sequence = new List<int>();
            playerChoices = new List<int>();
        }

        //generate random patter or build on old patter for difficulty
        public void generateSequence()
        {
            Random random = new Random();
            
            if(turnCounter == 0)//new game generation
            {
                for (int i = 0; i < 3; i++)
                {
                    Sequence.Add(random.Next(1, 4));
                }
            }
            else//added difficulty
            {
                Sequence.Add(random.Next(1, 4));
            }
        }

        //check for failure
        public bool verify()
        {
            bool survive = true;

            //if player picked wrong mark for failure
            for(int i = 0; i < playerChoices.Count; i++)
            {
                if (playerChoices.ElementAt(i) != Sequence.ElementAt(i))
                    survive = false;
            }

            return survive;
        }

        //gets and sets
        public List<int> getSequence()
        {
            return Sequence;
        }

        public int getPresses()
        {
            return pressCount;
        }

        public void addPress()
        {
            pressCount++;
        }

        public int getTurn()
        {
            return turnCounter;
        }

        public void nextTurn()
        {
            turnCounter++;
            pressCount = 0;
            playerChoices = new List<int>();
        }

        public void reset()
        {
            turnCounter = 0;
            pressCount = 0;
            Sequence = new List<int>();
            playerChoices = new List<int>();
        }
    }
}
