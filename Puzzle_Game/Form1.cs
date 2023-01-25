using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Puzzle_Game
{
    public partial class Form1 : Form
    {
        Point EmptyPoint;
        ArrayList images = new ArrayList();
        public Form1()
        {
            EmptyPoint.X = 180;
            EmptyPoint.Y = 180;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /*The button9_Click method is called when the button with the name "button9" is clicked. 
         * It enables all buttons in the "panel1" container, loads an image from the project's resources, 
         * and calls the RecortarImagen method to crop the image into 8 smaller images. 
         * These smaller images are then shuffled and added to the buttons in the "panel1" container using the AñadirImagenABoton method.*/
        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Button b in panel1.Controls)
                b.Enabled = true;

            Image orginal = Properties.Resources.loro;

            RecortarImagen(orginal, 300, 300);
            

            AñadirImagenABoton(images);
        }
        /*The AñadirImagenABoton method takes an ArrayList of images as a parameter and assigns the images to the buttons in the "panel1" container in a shuffled order.*/
        private void AñadirImagenABoton(ArrayList images)
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7 };

            arr = suffle(arr);

            foreach (Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
            }
        }
        /*The suffle method takes an int[] array as a parameter, shuffles its elements using the Random class, and returns the shuffled array.*/
        private int[] suffle(int[] arr)
        {
            Random rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray();
            return arr;
        }


        /*The RecortarImagen method takes an Image and two integers as parameters. 
         * It creates a new bitmap with the specified width and height, draws the original image onto the bitmap, 
         * and crops the bitmap into 8 smaller images, which are then added to the images ArrayList.*/
        private void RecortarImagen(Image orginal, int w, int h) 
        {
            Bitmap bmp = new Bitmap(w, h);

            Graphics graphic = Graphics.FromImage(bmp);

            graphic.DrawImage(orginal, 0, 0, w, h);

            graphic.Dispose();

            int movr = 0, movd = 0;

            for (int x = 0; x < 8; x++)
            {
                Bitmap piece = new Bitmap(90, 90);

                for (int i = 0; i < 90; i++)
                    for (int j = 0; j < 90; j++)
                        piece.SetPixel(i, j,
                            bmp.GetPixel(i + movr, j + movd));

                images.Add(piece);

                movr += 90;

                if (movr == 270)
                {
                    movr = 0;
                    movd += 90;
                }
            }

        }
        /*The button1_Click method is called when any button in the "panel1" container is clicked. 
         * It calls the MoveButton method, passing the clicked button as a parameter.*/
        private void button1_Click(object sender, EventArgs e)
        {
            MoveButton((Button)sender);
        }
        /*The MoveButton method takes a Button object as a parameter, 
         * and swaps its location with the location of the EmptyPoint field if the button is adjacent to the EmptyPoint. 
         * After the swap, it calls the ComprobarPuzzle method if the EmptyPoint field's location is the original location.*/
        private void MoveButton(Button btn)
        {
            if (((btn.Location.X == EmptyPoint.X - 90 || btn.Location.X == EmptyPoint.X + 90)
                && btn.Location.Y == EmptyPoint.Y)
                || (btn.Location.Y == EmptyPoint.Y - 90 || btn.Location.Y == EmptyPoint.Y + 90)
                && btn.Location.X == EmptyPoint.X)
            {
                Point swap = btn.Location;
                btn.Location = EmptyPoint;
                EmptyPoint = swap;
            }

            if (EmptyPoint.X == 180 && EmptyPoint.Y == 180)
                ComprobarPuzzle();
        }
        /*The ComprobarPuzzle method compares the images of the buttons in the "panel1" container with the images in the images ArrayList, 
         * and if they match, a message box is displayed to indicate that the puzzle has been completed.*/
        private void ComprobarPuzzle()
        {
            int count = 0, index;
            foreach (Button btn in panel1.Controls)
            {
                index = (btn.Location.Y / 90) * 3 + btn.Location.X / 90;
                if (images[index] == btn.Image)
                    count++;
            }
            if (count == 8)
                MessageBox.Show("Yupiiiii lo lograste!!!");
        }
    }
}