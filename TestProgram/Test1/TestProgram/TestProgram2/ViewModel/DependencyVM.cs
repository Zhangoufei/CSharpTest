using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace TestProgram2.ViewModel
{
    class DependencyVM : ViewModelBase
    {
        private ObservableCollection<Device> devices =new ObservableCollection<Device>();

        public ObservableCollection<Device> Devices
        {
            set
            {
                devices = value;
            }
            get { return devices; }
        }

        public DependencyVM()
        {
           
            devices.Add(new Device()
            {
                Id="123",Name = "王五",Email = "23@dddd"
            });
            devices.Add(new Device()
            {
                Id = "4444",
                Name = "王五222",
                Email = "23@dddd"
            });
            devices.Add(new Device()
            {
                Id = "12443",
                Name = "王五4444222",
                Email = "23@dddd"
            });

            ThreadPool.QueueUserWorkItem(DeviceQueue, devices);  //模拟5个设备
        }

        private string lableValue;

        public string LableValue
        {
            get
            {
                return lableValue;
            }

            set
            {
                lableValue = value;
                RaisePropertyChanged("LableValue");
            }
        }



        private void DeviceQueue(object obj)
        {
            while (true)
            {

                ObservableCollection<Device> temp = (ObservableCollection<Device>) obj ;



                Thread.Sleep(2000);
            }
        }

        private RelayCommand<object> addDevice;

        public ICommand AddDevice
        {
            get
            {
                if (addDevice == null)
                {
                    addDevice = new RelayCommand<object>(x => Sub(x));
                }
                return addDevice;
            }
        }
        public void Sub(object obj)
        {
            Devices.Add(new Device()
            {
                Id = "12443455555",
                Name = "55555",
                Email = "23@3333"
            });

            sumLable++;
            LableValue = sumLable.ToString();
        }

        private RelayCommand updateDevice;

        private int sumLable = 0;
        public ICommand UpdateDevice
        {
            get
            {
                if (updateDevice == null)
                {
                    

                    updateDevice =new RelayCommand(() =>
                    {
                        devices.RemoveAt(0);
                        devices[1].Name = "王五";
                    });
                   
                }
                return updateDevice;
            }

        }
    }

    class  Device : ViewModelBase
    {
        private string id;
        private string name;
        private string email;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
    }
}
