using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JaaJ.DAL
{
    /// <summary>
    /// 实际操作试题
    /// </summary>
    public class RealityScores
    {

        private int step;   //操作步骤
        private string content; //内容

        private ScreenValue[] screenValue; //图像识别的 步骤 和是否启用

        private ReadCardValue readCardValue;  //读卡器 对象  

        private bool submit;  //是否提交考试

        private double score;  //分数

        private ComboxValue comBoxValue;  //是否使用输入框


        public int Step
        {
            get
            {
                return step;
            }

            set
            {
                step = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }


        public ReadCardValue ReadCardValue
        {
            get
            {
                return readCardValue;
            }

            set
            {
                readCardValue = value;
            }
        }

        public bool Submit
        {
            get
            {
                return submit;
            }

            set
            {
                submit = value;
            }
        }

        public double Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public ScreenValue[] ScreenValue
        {
            get
            {
                return screenValue;
            }

            set
            {
                screenValue = value;
            }
        }

        public ComboxValue ComBoxValue
        {
            get
            {
                return comBoxValue;
            }

            set
            {
                comBoxValue = value;
            }
        }
    }
    public class ComboxValue
    {

        private string[] comBoxVlaueList;   //保存计量单位

        private decimal minValue;     //最小值

        private decimal maxValue;      //最大值

        private bool enable;        //是否启用

        public string[] ComBoxVlaueList
        {
            get
            {
                return comBoxVlaueList;
            }

            set
            {
                comBoxVlaueList = value;
            }
        }

        public decimal MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                minValue = value;
            }
        }

        public decimal MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                maxValue = value;
            }
        }

        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }
    }
    public class ScreenValue
    {

        private int screenNum;  //摄像头初始号 0-n
        private bool enable;   //是否启用
        private int step;      //图像识别的步骤

        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        public int Step
        {
            get
            {
                return step;
            }

            set
            {
                step = value;
            }
        }

        public int ScreenNum
        {
            get
            {
                return screenNum;
            }

            set
            {
                screenNum = value;
            }
        }
    }

    public class ReadCardValue
    {
        private bool enable;  //是否启用 读卡器
        private string epc;     //epc号
        private string address; //epc的地址
        private bool epcEanble;   //epc是否启用
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        public string Epc
        {
            get
            {
                return epc;
            }

            set
            {
                epc = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public bool EpcEanble
        {
            get
            {
                return epcEanble;
            }

            set
            {
                epcEanble = value;
            }
        }
    }
}
