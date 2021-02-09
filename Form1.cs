using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibleQuiz
{
    public partial class Form1 : Form
    {
        BookSetup bs = new BookSetup();
        int correct = 0;
        int max = 0;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the list.
            bs.BookKeys.Clear();
            //Reset the count of textBoxNumberCorrect to '0';
            textBoxNumberCorrect.Text = "0";
            //Shuffle the book keys.
            bs.ShuffleBookKeys();

            //Initialize each combo box with the book list.
            foreach (KeyValuePair<int, string> valuePair in BookSetup.BookDictionary)
            {
                comboBoxPreviousBook.Items.Add(valuePair.Value);
                comboBoxNextBook.Items.Add(valuePair.Value);
            }

            //Set the textbox to the first book to start the quiz.
            textBoxCurrentBook.Text = BookSetup.BookDictionary[bs.BookKeys[0]];
        }

        private void buttonCheckAnswerNext_Click(object sender, EventArgs e)
        {
            string toggle = buttonCheckAnswerNext.Text;
            //MessageBox.Show(buttonCheckAnswerNext.Text);
            if (bs.BookKeys.Count != 0)
            {
                if (buttonCheckAnswerNext.Text == "Check")
                {
                    CheckAnswers();
                    buttonCheckAnswerNext.Text = "Next";
                    buttonCheckAnswerNext.Focus();
                }
                else if (buttonCheckAnswerNext.Text == "Next")
                {
                    //Remove the key at index[0].
                    bs.BookKeys.RemoveAt(0);


                    //If the count of the key list is still not '0' after removing the previous index.
                    if (bs.BookKeys.Count != 0)
                    {
                        //Set the textBoxCurrentBook to the next value in the BookDictionary.
                        textBoxCurrentBook.Text = BookSetup.BookDictionary[bs.BookKeys[0]];
                    }
                    else
                    {
                        textBoxCurrentBook.Text = string.Empty;
                    }

                    //Reset the combo boxes, pictures, and 'Check' button.
                    comboBoxPreviousBook.Text = string.Empty;
                    comboBoxNextBook.Text = string.Empty;
                    pictureBoxPrevious.Image = null;
                    pictureBoxNext.Image = null;
                    buttonCheckAnswerNext.Text = "Check";
                    comboBoxPreviousBook.Focus();
                }              
               
            }            

            if (continuousToolStripMenuItem.Checked)
            {
                if (bs.BookKeys.Count == 0)
                {
                    bs.ShuffleBookKeys();
                }
                max++;
                textBoxMaxNumberCorrect.Text = max.ToString();
            }           
        }

        private void CheckAnswers()
        {
            //There is no previous book. 
            if (bs.BookKeys[0] == 1)
            {
                if ((comboBoxPreviousBook.Text == string.Empty) && (comboBoxNextBook.Text == BookSetup.BookDictionary[2]))
                {
                    correct++;                    
                    bothCorrect();
                }
                else if (comboBoxPreviousBook.Text == string.Empty)
                {
                    previousCorrect();
                }
                else if (comboBoxNextBook.Text == BookSetup.BookDictionary[2])
                {
                    nextCorrect();
                }
                else
                {
                    bothIncorrect();
                }
            }
            //There is no next book.
            else if (bs.BookKeys[0] == 66)
            {
                if ((comboBoxPreviousBook.Text == BookSetup.BookDictionary[65]) && (comboBoxNextBook.Text == string.Empty))
                {
                    correct++;
                    bothCorrect();
                }
                else if (comboBoxPreviousBook.Text == BookSetup.BookDictionary[65])
                {
                    bothCorrect();                    
                }
                else if (comboBoxNextBook.Text == string.Empty)
                {
                    nextCorrect();
                }
                else
                {
                    bothIncorrect();
                }
            }
            else
            {
                if ((comboBoxPreviousBook.Text == BookSetup.BookDictionary[bs.BookKeys[0] - 1]) && (comboBoxNextBook.Text == BookSetup.BookDictionary[bs.BookKeys[0] + 1]))
                {
                    correct++;
                    bothCorrect();                                       
                }
                else if (comboBoxPreviousBook.Text == BookSetup.BookDictionary[bs.BookKeys[0] - 1])
                {
                    previousCorrect();
                }
                else if (comboBoxNextBook.Text == BookSetup.BookDictionary[bs.BookKeys[0] + 1])
                {
                    nextCorrect();
                }
                else
                {
                    bothIncorrect();
                };
            }
            textBoxNumberCorrect.Text = correct.ToString();
        }

        private void exhaustListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            continuousToolStripMenuItem.Checked = false;
            exhaustListToolStripMenuItem.Checked = true;
            textBoxMaxNumberCorrect.Text = "66";
        }

        private void continuousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exhaustListToolStripMenuItem.Checked = false;
            continuousToolStripMenuItem.Checked = true;
            textBoxMaxNumberCorrect.Text = "0";
        }

        private void bothCorrect()
        {
            pictureBoxPrevious.Image = Image.FromFile(filename: @"Images\Correct.png");
            pictureBoxNext.Image = Image.FromFile(filename: @"Images\Correct.png");
        }

        private void bothIncorrect()
        {
            pictureBoxPrevious.Image = Image.FromFile(filename: @"Images\Incorrect.png");
            pictureBoxNext.Image = Image.FromFile(filename: @"Images\Incorrect.png");
        }

        private void previousCorrect()
        {
            pictureBoxPrevious.Image = Image.FromFile(filename: @"Images\Correct.png");
            pictureBoxNext.Image = Image.FromFile(filename: @"Images\Incorrect.png");
        }

        private void nextCorrect()
        {
            pictureBoxPrevious.Image = Image.FromFile(filename: @"Images\Incorrect.png");
            pictureBoxNext.Image = Image.FromFile(filename: @"Images\Correct.png");
        }
    }
}
