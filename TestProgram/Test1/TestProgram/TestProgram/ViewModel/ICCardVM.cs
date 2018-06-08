using Common;
using GalaSoft.MvvmLight;
using HardWare.CardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using TestProgram.BLL;

namespace TestProgram.ViewModel
{
    class ICCardVM : ViewModelBase
    {
        private IDCard IC = new IDCard();

        private ICCardBLL iCCardBll;

        public ICCardBLL ICCardBLL
        {
            get { return iCCardBll; }
            set
            {
                iCCardBll = value;
                RaisePropertyChanged("ICCardBLL");
            }
        }

        public ICCardVM()
        {
            Func<IDCard, IDCard> fuc = new Func<IDCard, IDCard>(IC.ReadCard);
            fuc.BeginInvoke(IC, new AsyncCallback(CanExam), null);
        }
        private void CanExam(IAsyncResult result)
        {
            LogImpl.Debug("读卡成功");
            AsyncResult ar = (AsyncResult)result;
            Func<IDCard, IDCard> md = (Func<IDCard, IDCard>)ar.AsyncDelegate;
            IDCard IC = md.EndInvoke(result);
            if (IC == null)
            {
                return;
            }
            ICCardBLL = new ICCardBLL
            {
                Name = IC.Name,
                Sex = IC.Sex,
                Nation = IC.Nation,
                Address = IC.Address,
                IDCode = IC.IDCode,
            };
        }
    }


}
