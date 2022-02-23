using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje5._36_PuzzleOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap resim = null, arkaPlan = null, tahta = null;
        private List<Parca> parcalar = null;
        int satirSayisi, kolonSayisi, satirYuksekligi, kolonGenisligi;
        private Parca hareketEdenParca = null;
        private Point hareketNoktasi;
        private bool oyunBitisi = true;
        private OpenFileDialog ofdDosyaSecimi = new OpenFileDialog();

        private void Form1_Load(object sender, EventArgs e)
        {
            if(ofdDosyaSecimi.ShowDialog() == DialogResult.OK)
            {
                Bitmap bm = new Bitmap(ofdDosyaSecimi.FileName);
                resim = new Bitmap(bm.Width, bm.Height);
                Graphics gr = Graphics.FromImage(resim);
                gr.DrawImage(bm, 0, 0, bm.Width, bm.Height);

                arkaPlan = new Bitmap(resim.Width, resim.Height);
                tahta = new Bitmap(resim.Width, resim.Height);
                pbAlan.Size = resim.Size;
                pbAlan.Image = tahta;
                this.ClientSize = new Size(pbAlan.Right + pbAlan.Left, pbAlan.Bottom + pbAlan.Left);

                if (resim == null) return;
                oyunBitisi = false;

                satirSayisi = 5;
                satirYuksekligi = resim.Height / satirSayisi;
                kolonSayisi = 5;
                kolonGenisligi = resim.Width / kolonSayisi;

                Random rand = new Random();
                parcalar =  new List<Parca>();
                for (int row = 0; row < satirSayisi; row++)
                {
                    int hgt = satirYuksekligi;
                    if (row == satirSayisi - 1) hgt = resim.Height - row * satirYuksekligi;
                    for (int col = 0; col < kolonSayisi; col++)
                    {
                        int wid = kolonGenisligi;
                        if(col == kolonSayisi - 1) wid = resim.Width - col * kolonGenisligi;
                        Rectangle rect = new Rectangle(col * kolonGenisligi, row * satirYuksekligi,
                            wid, hgt);

                        Parca yeniParca = new Parca(resim, rect);

                        yeniParca.MevcutNoktasi = new Rectangle(rand.Next(0, resim.Width - wid), rand.Next(0, resim.Height - hgt), wid, hgt);

                        parcalar.Add(yeniParca);
                    }
                }
                arkaPlanYap();
                tahtaOlustur();
            }
        }

        private void arkaPlanYap()
        {
            Graphics gr = Graphics.FromImage(arkaPlan);
            gr.Clear(pbAlan.BackColor);
            diyagramOlustur(gr);
            parcalariOlustur(gr);
            pbAlan.Visible = true;
            pbAlan.Refresh();
        }

        private void diyagramOlustur(Graphics gr)
        {
            Pen kalem = new Pen(Color.DarkGray, 4);
            for(int y = 0; y <= resim.Height; y += satirYuksekligi) gr.DrawLine(kalem, 0, y, resim.Width, y);

            gr.DrawLine(kalem, 0, resim.Height, resim.Width, resim.Height);
            for (int x = 0; x <= resim.Width; x += kolonGenisligi) gr.DrawLine(kalem, x, 0, x, resim.Height);

            gr.DrawLine(kalem, resim.Width, 0, resim.Width, resim.Height);
        }

        private void parcalariOlustur(Graphics gr)
        {
            Pen beyazKalem = new Pen(Color.White, 3);
            Pen siyahKalem = new Pen(Color.Black, 3);
            foreach(Parca piece in parcalar)
            {
                //Sabitlenmiş parçalar dışındakiler için.
                if(piece != hareketEdenParca)
                {
                    gr.DrawImage(resim,
                        piece.MevcutNoktasi,
                        piece.BaslangicNoktasi,
                        GraphicsUnit.Pixel);
                    if(!oyunBitisi)
                    {
                        if (piece.baslangicNoktasindaMi())
                            gr.DrawRectangle(beyazKalem, piece.MevcutNoktasi);
                        else
                            //Siyah kenarlıklı kilitli parçalar çizin.
                            gr.DrawRectangle(siyahKalem, piece.MevcutNoktasi);
                    }
                }
            }
        }

        private void tahtaOlustur()
        {
            Graphics gr = Graphics.FromImage(tahta);
            gr.DrawImage(arkaPlan, 0, 0, arkaPlan.Width, arkaPlan.Height);
            if(hareketEdenParca != null)
            {
                gr.DrawImage(resim, hareketEdenParca.MevcutNoktasi, 
                    hareketEdenParca.BaslangicNoktasi, GraphicsUnit.Pixel);
                Pen blue_pen = new Pen(Color.Blue, 4);
                gr.DrawRectangle(blue_pen, hareketEdenParca.MevcutNoktasi);
            }
            pbAlan.Visible = true;
            pbAlan.Refresh();
        }

        private void pbAlan_MouseDown(object sender, MouseEventArgs e)
        {
            hareketEdenParca = null;
            foreach(Parca piece in parcalar)
            {
                if(!piece.baslangicNoktasindaMi() && piece.Contains(e.Location))
                    hareketEdenParca = piece;
            }
            if (hareketEdenParca == null) return;

            hareketNoktasi = e.Location;

            parcalar.Remove(hareketEdenParca);
            parcalar.Add(hareketEdenParca);

            arkaPlanYap();
            tahtaOlustur();
        }

        private void pbAlan_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareketEdenParca == null) return;

            int dx = e.X - hareketNoktasi.X;
            int dy = e.Y - hareketNoktasi.Y;
            hareketEdenParca.MevcutNoktasi.X += dx;
            hareketEdenParca.MevcutNoktasi.Y += dy;

            hareketNoktasi = e.Location;

            tahtaOlustur();
        }

        private void pbAlan_MouseUp(object sender, MouseEventArgs e)
        {
            if (hareketEdenParca == null) return;

            if(hareketEdenParca.baslangicNoktasinaYapistir())
            {
                parcalar.Remove(hareketEdenParca);
                parcalar.Reverse();
                parcalar.Add(hareketEdenParca);
                parcalar.Reverse();

                oyunBitisi = true;
                foreach(Parca piece in parcalar)
                {
                    if(!piece.baslangicNoktasindaMi())
                    {
                        oyunBitisi = false;
                        break;
                    }
                }
            }

            hareketEdenParca = null;

            arkaPlanYap();
            tahtaOlustur();
        }
    }

    class Parca
    {
        public Bitmap resim;
        public Rectangle BaslangicNoktasi, MevcutNoktasi;

        public Parca(Bitmap yeniResim, Rectangle baslangicYeri)
        {
            resim = yeniResim;
            BaslangicNoktasi = baslangicYeri;
        }

        public bool baslangicNoktasindaMi()
        {
            return BaslangicNoktasi.Equals(MevcutNoktasi);
        }

        public bool Contains(Point p)
        {
            return MevcutNoktasi.Contains(p);
        }

        public bool baslangicNoktasinaYapistir()
        {
            if((Math.Abs(MevcutNoktasi.X - BaslangicNoktasi.X) < 20) &&
               (Math.Abs(MevcutNoktasi.Y - BaslangicNoktasi.Y) < 20))
            {
                MevcutNoktasi = BaslangicNoktasi;
                return true;
            }
            return false;
        }
    }
}
