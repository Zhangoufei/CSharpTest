using GalaSoft.MvvmLight;
using JAAJ.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace TestProgram.ViewModel
{
    class PhotoPictureVM : ViewModelBase
    {
        private BitmapImage imagePicture;

        public BitmapImage ImagePicture {

            get
            {
                return imagePicture;
            }
            set
            {
                imagePicture = value;
                RaisePropertyChanged("ImagePicture");
            }
        }

        public PhotoPictureVM() {

            StringBuilder buider = new StringBuilder();
            buider.Append("select exam.iexamineeid,exam.nvcName,exam.bsex,exam.nvcNation,exam.nvcIDNum,exam.imgpicture");
            buider.Append(" from JAAJ_Batchs bat ");
            buider.Append("left join JAAJ_BatchExamineeMap map  on bat.iBatchID=map.iBatchID ");
            buider.Append("left join JAAJ_Examinees exam on map.iExamineeID=exam.iExamineeID ");
            buider.Append("left join JAAJ_Exams exams on exams.nvcBatchNO=bat.nvcBatchNO ");
            buider.Append("where exam.iExamineeID = " + 491 + " and bat.iBatchID=" + 102);
            //查询数据库  转变成图片
            DataTable table = SQLHelp.GetDataTable(buider.ToString());
            if (table.Rows.Count > 0)
            {
                byte[] imagebyte =(byte[]) table.Rows[0]["imgpicture"];
                //开始加载图像  
                BitmapImage bim = new BitmapImage();
                bim.BeginInit();
                bim.StreamSource = new MemoryStream(imagebyte);
                bim.EndInit();
                ImagePicture = bim;
            }
        }
    }
}
