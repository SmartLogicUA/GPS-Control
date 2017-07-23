using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GpsFlashControl
{

    [Serializable]
    struct DegreeRecord
    {
        string data;

        public DegreeRecord(string input)
        {
            if (input.Length != 8 && input.Length != 9)
                throw new ArgumentException("Couldn't init DegreeRecord instance");
            data = input;
        }

        public override string ToString()
        {
            if (data.Length == 8)
                return data.Substring(0, 2) + "°" + data.Substring(2, 2) + "," + data.Substring(4);
            else
                return data.Substring(1, 2) + "°" + data.Substring(3, 2) + "," + data.Substring(5);
        }
    }

    [Serializable]
    struct FlashRecord : IComparable
    {
        DateTime time;
        DegreeRecord latitude;
        DegreeRecord longitude;

        public FlashRecord(string input, bool fromDevice)
        {
            if (input.Length != 30)
                throw new ArgumentException("Couldn't init FlashRecord instance");
            time = new DateTime(2000 + int.Parse(input.Substring(28, 2)), int.Parse(input.Substring(26, 2)), int.Parse(input.Substring(24, 2)), int.Parse(input.Substring(18, 2)), int.Parse(input.Substring(20, 2)), int.Parse(input.Substring(22, 2)));
            if (fromDevice)
                time = DateTime.SpecifyKind(time, DateTimeKind.Utc).ToLocalTime();
            else
                time = DateTime.SpecifyKind(time, DateTimeKind.Local);
            latitude = new DegreeRecord(input.Substring(0, 8));
            longitude = new DegreeRecord(input.Substring(8, 9));
        }

        public FlashRecord(string input) : this(input, false) { }

        public string[] ToStringArray()
        {
            string[] output = new string[4];
            output[0] = time.ToShortDateString();
            output[1] = time.ToLongTimeString();
            output[2] = latitude.ToString();
            output[3] = longitude.ToString();

            return output;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is FlashRecord)
                return this.time.CompareTo(((FlashRecord)obj).time);
            else
                throw new ArgumentException("The object isn't a FlashRecord instance");
        }

        #endregion
    }

    public delegate void VoidString(string input);
    public delegate void VoidControl(Control input);
    public delegate DataGridView GridVoid();
    //public delegate int IntObjectArray(params object[] input);
    //public delegate void VoidDatagridView(DataGridView input);
}