using CallNumber.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TestProgram.ViewModel
{
    class DataGridVM
    {
        CallNumberEntry2 call = new CallNumberEntry2();


        private DataGrid grid;

        public DataGridVM(DataGrid grid)
        {
            call.EntryLeft = new CallNumberEntry();
            call.EntryLeft.DeviceName = "设备名称";
            call.EntryLeft.ExamineeId = 32342;
            call.EntryLeft.ExamineeName = "王五";
            call.EntryLeft.ExamRoom = "默认考场";

            List<CallNumberEntry2> list = new List<CallNumberEntry2>();
            list.Add(call);
            grid.ItemsSource = list;
        }

        public DataGrid Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;
            }
        }
    }
    public class CallNumberEntry : INotifyPropertyChanged
    {
        private String examineeName;
        private String deviceName;
        private String subjectItemName;
        private Int32 examProcId;
        private Int32 examineeId;
        private string examRoom;

        public Int32 ExamineeId
        {
            get { return examineeId; }
            set
            {
                examineeId = value;
                OnPropertyChanged(nameof(ExamineeId));
            }
        }

        public Int32 ExamProcId
        {
            get { return examProcId; }
            set
            {
                examProcId = value;
                OnPropertyChanged(nameof(ExamProcId));
            }
        }

        public string ExamineeName
        {
            get { return examineeName; }
            set
            {
                examineeName = value;
                OnPropertyChanged(nameof(ExamineeName));
            }
        }

        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                deviceName = value;
                OnPropertyChanged(nameof(DeviceName));
            }
        }

        public string SubjectItemName
        {
            get { return subjectItemName; }
            set
            {
                subjectItemName = value;
                OnPropertyChanged(nameof(SubjectItemName));
            }
        }

        public string ExamRoom
        {
            get { return examRoom; }
            set
            {
                examRoom = value;
                OnPropertyChanged(nameof(ExamRoom));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class CallNumberEntry2 : INotifyPropertyChanged
    {
        private CallNumberEntry entryLeft;
        private CallNumberEntry entryRight;

        public CallNumberEntry EntryLeft
        {
            get { return entryLeft; }
            set
            {
                entryLeft = value;
                OnPropertyChanged(nameof(EntryLeft));
            }
        }

        public CallNumberEntry EntryRight
        {
            get { return entryRight; }
            set
            {
                entryRight = value;
                OnPropertyChanged(nameof(EntryRight));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
