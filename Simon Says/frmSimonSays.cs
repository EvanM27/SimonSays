using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simon_Says
{
    public partial class GameForm : Form
    {
        //model instantiation
        GameEngine GE = new GameEngine();

        public GameForm()
        {
            InitializeComponent();
        }

        //start the game
        private void btnStart_Click(object sender, EventArgs e)
        {
            //begin first part of game loop
            gameStart();
        }

        //first part of game loop
        public void gameStart()
        {
            btnStart.Enabled = false;
            lblScore.Text = "Remember the pattern!";

            //generate pattern for player to match
            GE.generateSequence();

            //begin second part of loop
            displaySequence(GE.getSequence());
        }

        //seconf part of game loop
        public async void displaySequence(List<int> list)
        {
            //read list in model to display lights
            foreach (int x in list)
            {
                //red light
                if (x == 1)
                {
                    btnRed.BackColor = Color.Red;
                    await Task.Delay(1200);
                    btnRed.BackColor = SystemColors.Control;
                    await Task.Delay(500);
                }
                //blue light
                if (x == 2)
                {
                    btnBlue.BackColor = Color.Blue;
                    await Task.Delay(1200);
                    btnBlue.BackColor = SystemColors.Control;
                    await Task.Delay(500);
                }
                //yellow light
                if (x == 3)
                {
                    btnYellow.BackColor = Color.Yellow;
                    await Task.Delay(1200);
                    btnYellow.BackColor = SystemColors.Control;
                    await Task.Delay(500);
                }
                //green light
                if (x == 4)
                {
                    btnGreen.BackColor = Color.Green;
                    await Task.Delay(1200);
                    btnGreen.BackColor = SystemColors.Control;
                    await Task.Delay(500);
                }
            }

            //third part of loop begins here
            activateButtons();
        }
        
        //last part of game loop
        private void compTurn()
        {
            deactivateButtons();

            //determine success or failure
            if (GE.verify())
            {
                //make game harder
                GE.nextTurn();
                //recycle game loop
                gameStart();
            }
            else
            {
                //do not pass go, do not collect $200
                gameOver();
            }
        }

        //failure state
        public void gameOver()
        {
            //show final score
            lblScore.Text = " Game Over \nRounds survived: " + GE.getTurn();

            //reset game engine
            GE.reset();

            //allows players to start a fresh game
            btnStart.Enabled = true;
        }

        //enable player controls
        public void activateButtons()
        {
            lblScore.Text = "Enter the pattern!";

            btnRed.MouseClick += redClick;
            btnBlue.MouseClick += blueClick;
            btnYellow.MouseClick += yellowClick;
            btnGreen.MouseClick += greenClick;
        }

        //disable player controls
        public void deactivateButtons()
        {
            btnRed.MouseClick -= redClick;
            btnBlue.MouseClick -= blueClick;
            btnYellow.MouseClick -= yellowClick;
            btnGreen.MouseClick -= greenClick;
        }

        //player controls
        //also is third part of game loop 
        //segues into last part
        private void redClick(object sender, MouseEventArgs e)
        {
            GE.playerChoices.Add(1);
            GE.addPress();

            if (GE.getPresses() == GE.getSequence().Count)
                compTurn();
        }

        private void blueClick(object sender, MouseEventArgs e)
        {
            GE.playerChoices.Add(2);
            GE.addPress();

            if (GE.getPresses() == GE.getSequence().Count)
                compTurn();
        }

        private void yellowClick(object sender, MouseEventArgs e)
        {
            GE.playerChoices.Add(3);
            GE.addPress();

            if (GE.getPresses() == GE.getSequence().Count)
                compTurn();
        }

        private void greenClick(object sender, MouseEventArgs e)
        {
            GE.playerChoices.Add(4);
            GE.addPress();

            if (GE.getPresses() == GE.getSequence().Count)
                compTurn();
        }
    }
}
