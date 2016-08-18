using Backgammon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace Backgammon_Form
{
    public partial class BackgammonGame : Form
    {
        public Controller controller { get; private set; }
        public List<Label> triangelsLablesList { get; private set; }
        private string CurrentPlayerTurn = @"Black Player Turn";
        public BackgammonGame()
        {
            InitializeComponent();
            controller = new Controller();
            SetTriangelsLablesList();
            UpdateBoardStatus();

            PlayerTurnLbl.Text = CurrentPlayerTurn;
        }

        public void SetTriangelsLablesList()
        {
            triangelsLablesList = new List<Label> ();
            triangelsLablesList.Add(label1);
            triangelsLablesList.Add(label2);
            triangelsLablesList.Add(label3);
            triangelsLablesList.Add(label4);
            triangelsLablesList.Add(label5);
            triangelsLablesList.Add(label6);
            triangelsLablesList.Add(label7);
            triangelsLablesList.Add(label8);
            triangelsLablesList.Add(label9);
            triangelsLablesList.Add(label10);
            triangelsLablesList.Add(label11);
            triangelsLablesList.Add(label12);
            triangelsLablesList.Add(label13);
            triangelsLablesList.Add(label14);
            triangelsLablesList.Add(label15);
            triangelsLablesList.Add(label16);
            triangelsLablesList.Add(label17);
            triangelsLablesList.Add(label18);
            triangelsLablesList.Add(label19);
            triangelsLablesList.Add(label20);
            triangelsLablesList.Add(label21);
            triangelsLablesList.Add(label22);
            triangelsLablesList.Add(label23);
            triangelsLablesList.Add(label24);
        }
        public void UpdateBoardStatus()
        {
            for (int i = 0; i < 24; i++)
            {
                if (controller.BoardGame.Triangles[i]!=null)
                {
                    if (controller.BoardGame.Triangles[i].CheckerColor == Backgammon.Color.Black)
                    {
                        triangelsLablesList[i].Text= controller.BoardGame.Triangles[i].CheckerNum.ToString();
                        triangelsLablesList[i].ForeColor=Color.Black;
                    }
                    else
                    {
                        triangelsLablesList[i].Text = controller.BoardGame.Triangles[i].CheckerNum.ToString();
                        triangelsLablesList[i].ForeColor = Color.White;
                    }
                }
                if (controller.BoardGame.Bar.CountBlackInBar!=0)
                {
                    blackInBarTxtbox.Text = controller.BoardGame.Bar.CountBlackInBar.ToString();
                }
                if (controller.BoardGame.Bar.CountWhiteInBar!=0)
                {
                    whiteInBarTxtbox.Text = controller.BoardGame.Bar.CountWhiteInBar.ToString();
                }
            }
        }
        private void RollDiceBtn_Click(object sender, EventArgs e)
        {
            controller.RollDice();
            firstDiceResult.Text = controller.DiceGame.FirstDice.ToString();
            secondDiceResult.Text = controller.DiceGame.SecondDice.ToString();
        }

        private void moveBtn_Click(object sender, EventArgs e)
        {
            if (sourceTxtbox.Text == "" || targetTxtbox.Text=="")
            {
                MessageBox.Show(@"Must enter from where and to where would you like to move");
            }
            controller.SetPlayerSourceMove(Int32.Parse(sourceTxtbox.Text));
            controller.SetPlayerTargetMove(Int32.Parse(targetTxtbox.Text),false);

        }
    }
}
