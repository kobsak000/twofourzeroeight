using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twozerofoureight
{
    public partial class TwoZeroFourEightView : Form, View

    {
        //private KeyPressEventHandler kpeh;
        //private KeyEventHandler keh;
        Form1 over;
        Model model;
        Controller controller;
        TwoZeroFourEightModel model2048;
        
        private string sc;
        
        public TwoZeroFourEightView()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(115,220, 235);
            model = new TwoZeroFourEightModel();
            model2048 = new TwoZeroFourEightModel();
            model.AttachObserver(this);
            controller = new TwoZeroFourEightController();
            controller.AddModel(model);
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);

            /*this.Focus();
            kpeh = new KeyPressEventHandler(key_Press);
            this.KeyPress += kpeh;

            keh = new KeyEventHandler(key_Down);
            this.KeyDown += keh;*/
        }

        
        public void Notify(Model m)
        {
            UpdateBoard(((TwoZeroFourEightModel) m).GetBoard());
            
        }

        private void TwoZeroFourEightView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                case Keys.Up:
                    controller.ActionPerformed(TwoZeroFourEightController.UP);
                    //btnUp.Focus();
                    
                    break;
                case Keys.S:
                case Keys.Down:
                    controller.ActionPerformed(TwoZeroFourEightController.DOWN);
                    //btnDown.Focus();
                    break;
                case Keys.A:
                case Keys.Left:
                    controller.ActionPerformed(TwoZeroFourEightController.LEFT);
                    //btnLeft.Focus();
                    break;
                case Keys.D:
                case Keys.Right:
                    controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
                    //btnRight.Focus();
                    break;
            }
        }



        private void UpdateTile(Label l, int i)
        {
            
            if (i != 0)
            {
                l.Text = Convert.ToString(i);
                
            } else {
                l.Text = "";
            }
            switch (i)
            {
                case 0:
                    l.BackColor = Color.White ;
                    break;
                case 2:
                    l.BackColor = Color.FromArgb(137, 255, 200); 
                    break;
                case 4:
                    l.BackColor = Color.FromArgb(0, 255,137);
                    break;
                case 8:
                    l.BackColor = Color.FromArgb(22, 191, 112);

                    break;
                default:
                    l.BackColor = Color.FromArgb(10, 132, 75);

                    break;
            }
        }

        private bool full(int[,]board)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0)
                        return false;
                }
            }
            return true;
        }
        private void UpdateBoard(int[,] board)
        {
            
            UpdateTile(lbl00,board[0, 0]);
            UpdateTile(lbl01,board[0, 1]);
            UpdateTile(lbl02,board[0, 2]);
            UpdateTile(lbl03,board[0, 3]);
            UpdateTile(lbl10,board[1, 0]);
            UpdateTile(lbl11,board[1, 1]);
            UpdateTile(lbl12,board[1, 2]);
            UpdateTile(lbl13,board[1, 3]);
            UpdateTile(lbl20,board[2, 0]);
            UpdateTile(lbl21,board[2, 1]);
            UpdateTile(lbl22,board[2, 2]);
            UpdateTile(lbl23,board[2, 3]);
            UpdateTile(lbl30,board[3, 0]);
            UpdateTile(lbl31,board[3, 1]);
            UpdateTile(lbl32,board[3, 2]);
            UpdateTile(lbl33,board[3, 3]); 
            label1.Text ="Score : "+ Convert.ToString(model2048.Upscore(board));
            sc = Convert.ToString(model2048.Upscore(board));
            if(full(board))
                gameover(board);
            
        }

        public void gameover(int[,] board)
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return;
                    }
                    if (i == 0)
                    {

                        switch (j)
                        {
                            case 0:
                                if (board[i, j] == board[i + 1, j] || board[i, j] == board[i, j + 1])
                                    return;
                                break;
                            case 3:
                                if (board[i, j] == board[i + 1, j] || board[i, j] == board[i, j - 1])
                                    return;
                                break;
                            default:
                                if (board[i, j] == board[i + 1, j] || board[i, j] == board[i, j - 1] || board[i, j] == board[i, j + 1])
                                    return;
                                break;
                        }
                    }

                    if (j == 0)
                    {

                        switch (i)
                        {
                            case 0:
                                if (board[i, j] == board[i + 1, j] || board[i, j] == board[i, j + 1])
                                    return;
                                break;

                            case 3:
                                if (board[i, j] == board[i - 1, j] || board[i, j] == board[i, j + 1])
                                    return;
                                break;
                            default:
                                if (board[i, j] == board[i + 1, j] || board[i, j] == board[i - 1, j] || board[i, j] == board[i, j + 1])
                                    return;
                                break;
                        }
                    }

                    if (i == 3)
                    {

                        switch (j)
                        {
                            case 0:
                                if (board[i, j] == board[i - 1, j] || board[i, j] == board[i, j + 1])
                                    return;
                                break;
                            case 3:
                                if (board[i, j] == board[i - 1, j] || board[i, j] == board[i, j - 1])
                                    return;
                                break;
                            default:
                                if (board[i, j] == board[i, j - 1] || board[i, j] == board[i - 1, j] || board[i, j] == board[i , j + 1])
                                    return;
                                break;
                        }
                    }

                    if (j == 3)
                    {
                        switch (i)
                        {
                            case 0:
                                if (board[i, j] == board[i + 1, j] || board[i, j] == board[i, j - 1])
                                    return;
                                break;
                            case 3:
                                if (board[i, j] == board[i - 1, j] || board[i, j] == board[i, j - 1])
                                    return;
                                break;

                            default:
                                if (board[i, j] == board[i, j - 1] || board[i, j] == board[i - 1, j] || board[i, j] == board[i + 1, j])
                                    return;
                                break;
                        }
                    }
                }
            }
            over = new Form1(label1.Text);
            this.Hide();
            over.Show();
        }
       
        private void btnLeft_Click(object sender, EventArgs e)
        {

            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            
            controller.ActionPerformed(TwoZeroFourEightController.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            
            controller.ActionPerformed(TwoZeroFourEightController.DOWN);
        }

        
    }
}
