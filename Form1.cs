using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPQ
{
    public partial class Form1 : Form
    {
        int ImageNumber = 0;
        int ImageCount = 9;
        List<Slide> AllImages = new List<Slide>();
        List<Slide> SelectedImages = new List<Slide>();
        List<PictureBox> Multibox;
        bool ShowingSingle = true;
        public Form1()
        {
            InitializeComponent();
            MakeBorderInvisible(BTN_BACKWARD);
            MakeBorderInvisible(BTN_FORWARD);
            MakeBorderInvisible(BTN_SHOW_INFO);
            MakeBorderInvisible(BTN_SEARCH);
            MakeBorderInvisible(BTN_SWITCHDISPLAY);
            BTN_SWITCHDISPLAY.Image = Image.FromFile("SingleToMultiple.png");
            var color = ColorTranslator.FromHtml("#EFEFEF");
            Multibox = new List<PictureBox> { PB_MULTIPLE1, PB_MULTIPLE2, PB_MULTIPLE3, PB_MULTIPLE4, PB_MULTIPLE5, PB_MULTIPLE6, PB_MULTIPLE7, PB_MULTIPLE8 };
            TXT_SEARCH.BackColor = color;
            LBL_SLIDE.BackColor = color;
            InitialiseAllImages();
            foreach(Slide slide in AllImages)
            {
                SelectedImages.Add(slide);
            }
            UpdateImage();
            
        }
        private void InitialiseAllImages()
        {
            StreamReader sr = new StreamReader("test.csv");
            string value;
            string[] split;
            
            for (int i =0; i< ImageCount; i++) 
            {
                List<string> tags = new List<string>();
                split = sr.ReadLine().Split(',');
                
                for (int j = 0; j < split.Length; j++)
                {
                    tags.Add(split[j].Trim());
                }
                
                value = Convert.ToString(i) + ".png";
                var bmp = Image.FromFile(value);

                Slide TempSlide = new Slide(tags, i, bmp);
                AllImages.Add(TempSlide);
            }
        }
        private void UpdateSlideNumber()
        {
            LBL_SLIDE.Text = (ImageNumber+1).ToString() + "/" + (SelectedImages.Count).ToString();
        }
        private void UpdateTags()
        {
            LBL_SHOWTAGS.Visible = false;
            LBL_SHOWTAGS.Text = "";
            foreach (string tag in SelectedImages[ImageNumber].GetTags())
            {
                LBL_SHOWTAGS.Text += tag + "\n";
            }
        }
        private void UpdateImage()
        {

            
            if (SelectedImages.Count == 0)
            {
                PB_SINGLEIMAGE.Image = Image.FromFile("noresults.png");
            }
            else
            {
                foreach(PictureBox picture in Multibox)
                {
                    picture.Image = null;
                }
                if (ShowingSingle)
                {
                    PB_SINGLEIMAGE.Image = SelectedImages[ImageNumber].Image;
                }
                else
                {
                    int tempImageNumber = ImageNumber;
                    int min = Math.Min(SelectedImages.Count - ImageNumber, 8);
                    for (int i = 0; i < min; i++)
                    {
                        Multibox[i].Image = SelectedImages[tempImageNumber].Image;
                        tempImageNumber++;
                        if (tempImageNumber == SelectedImages.Count)
                        {
                            tempImageNumber = 0;
                        }
                    }
                }
                UpdateTags();
            }
            UpdateSlideNumber();
        }
        private void MakeBorderInvisible(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }
        private void BTN_FORWARD_Click(object sender, EventArgs e)
        {
            if (!ShowingSingle && SelectedImages.Count < 8)
            {

            }
            else
            {
                if (ImageNumber != -1)
                {
                    if (ShowingSingle)
                    {
                        ImageNumber++;
                        if (ImageNumber == (SelectedImages.Count))
                        {
                            ImageNumber = 0;
                        }
                    }
                    else
                    {
                        ImageNumber += 8;
                        if (ImageNumber > (SelectedImages.Count))
                        {
                            ImageNumber = ImageNumber%(SelectedImages.Count-1);
                        }
                    }
                    UpdateImage();
                }

            }
        }
        private void BTN_BACKWARD_Click(object sender, EventArgs e)
        {
            
            if (!ShowingSingle && SelectedImages.Count < 8 )
            {

            }
            else
            {
                if (ShowingSingle)
                {
                    ImageNumber--;
                    if (ImageNumber == -1)
                    {
                        ImageNumber = (SelectedImages.Count) - 1;
                    }
                }
                else
                {
                    ImageNumber -= 8;
                    if (ImageNumber < 0)
                    {
                        ImageNumber = (SelectedImages.Count) + ImageNumber + (8- SelectedImages.Count%8);
                    }
                }
                UpdateImage();
            }

        }

        private void BTN_SHOW_INFO_Click(object sender, EventArgs e)
        {
            LBL_SHOWTAGS.Visible = !LBL_SHOWTAGS.Visible;
        }
        private void SWITCHDISPLAY()
        {
            if (!ShowingSingle)
            {
                BTN_SWITCHDISPLAY.Image = Image.FromFile("SingleToMultiple.png");
            }
            else
            {
                BTN_SWITCHDISPLAY.Image = Image.FromFile("MultipleToSingle.png");
                ImageNumber -= ImageNumber % 8;
            }
            ShowingSingle = !ShowingSingle;
            BOX_MULTIPLE.Visible = !BOX_MULTIPLE.Visible;
            BOX_SINGLE.Visible = !BOX_SINGLE.Visible;
            BTN_SHOW_INFO.Visible = !BTN_SHOW_INFO.Visible;
            LBL_SHOWTAGS.Visible = false;
            UpdateImage();
        }
        private void BTN_SWITCHDISPLAY_Click(object sender, EventArgs e)
        {
            SWITCHDISPLAY();
        }

        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            SelectedImages.Clear();
            string search = TXT_SEARCH.Text;
            bool added = false;
            if (search == "*")
            {
                foreach (Slide slide in AllImages)
                {
                    SelectedImages.Add(slide);
                }
            }
            else
            {
                foreach (Slide tempslide in AllImages)
                {
                    List<string> tags = tempslide.GetTags();
                    for (int j = 0; j < tags.Count; j++)
                    {
                        if (search.Length <= tags[j].Length)
                        {
                            if (tags[j].Substring(0, search.Length) == search && !added)
                            {
                                added = true;
                                SelectedImages.Add(tempslide);
                            }
                        }

                    }
                    added = false;
                }
            }
            
            if (!ShowingSingle)
            {
               SWITCHDISPLAY();
            }
            if(SelectedImages.Count == 0)
            {
                ImageNumber = -1;
                LBL_SHOWTAGS.Text = "";
            }
            else
            {
                ImageNumber = 0;
            }
            
            UpdateImage();
        }

        
    }
    public class Slide
    {
        private List<string> Descriptor;
        private int ImageIndex;
        public Image Image;
        public Slide( List<string> aDescriptor, int aImageIndex, Image aImage)
        {
            this.Descriptor = aDescriptor;
            this.ImageIndex = aImageIndex;
            this.Image = aImage;
        }
        public List<string> GetTags()
        {
            return Descriptor;
        } 
    }

}
