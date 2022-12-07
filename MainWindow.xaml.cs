using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Vives;

namespace project_1e_semester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    

    public partial class MainWindow : Window
    {
        SerialPort _serialPort;
        byte[] _data;
        const int START_ADDRESS = 160;
        const int NUMBER_OF_DMX_BYTES = 513;
        DispatcherTimer _dispatcherTimer;
        int pan;
        int tilt;

        MovingHead movinghead = new MovingHead();

        public MainWindow()
        {
            InitializeComponent();

            cbxPortName.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxPortName.Items.Add(s);

            _serialPort = new SerialPort();
            _serialPort.BaudRate = 250000;
            _serialPort.StopBits = StopBits.Two;

            _data = new byte[NUMBER_OF_DMX_BYTES];

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(0.1);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

        }

        private void SendDmxData(byte[] data, SerialPort serialPort)
        {
            data[0] = 0;

            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.BreakState = true;
                Thread.Sleep(1);
                serialPort.BreakState = false;
                Thread.Sleep(1);

                serialPort.Write(data, 0, data.Length);
            }
        }

        private void cbxPortName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                if (cbxPortName.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxPortName.SelectedItem.ToString();
                    _serialPort.Open();

                    sldrSpeed.IsEnabled = true;
                    sldrDimmer.IsEnabled = true;
                    sldrStrobe.IsEnabled = true;
                    sldrRed.IsEnabled = true;
                    sldrGreen.IsEnabled = true;
                    sldrBleu.IsEnabled = true;
                    sldrWhite.IsEnabled = true;
                    sldrPanTiltMode.IsEnabled = true;
                    sldrSnelheidPanTilt.IsEnabled = true;
                    sldrColorMode.IsEnabled = true;
                    sldrSnelheidColor.IsEnabled = true;

                }
                else
                {
                    sldrSpeed.IsEnabled = false;
                    sldrDimmer.IsEnabled = false;
                    sldrStrobe.IsEnabled = false;
                    sldrRed.IsEnabled = false;
                    sldrGreen.IsEnabled = false;
                    sldrBleu.IsEnabled = false;
                    sldrWhite.IsEnabled = false;
                    sldrPanTiltMode.IsEnabled = false;
                    sldrSnelheidPanTilt.IsEnabled = false;
                    sldrColorMode.IsEnabled = false;
                    sldrSnelheidColor.IsEnabled = false;
                }
            }
        }

        private void _dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            SendDmxData(_data, _serialPort);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SendDmxData(new byte[NUMBER_OF_DMX_BYTES], _serialPort);
            _serialPort.Dispose();
        }
        
        private void KeyPress_KeyDown(object sender, KeyEventArgs e)
        {
            //toetsen voor op en neer beweging
            if (e.Key == Key.D & pan <= 254)
            {
                pan = movinghead.PanMoveRight();
            }
            if (e.Key == Key.Q & pan >=1)
            {
                pan = movinghead.PanMoveLeft();
            }
            _data[START_ADDRESS + 0] = Convert.ToByte(pan);
            lblpan.Content = pan;
            //toetsen voor links rechts beweging
            if (e.Key == Key.Z & tilt <= 254)
            {
               tilt = movinghead.TiltMoveUp();
            }
            if (e.Key == Key.S & tilt >= 1)
            {
               tilt = movinghead.TiltMoveDown();
            }
            _data[START_ADDRESS + 1] = Convert.ToByte(tilt);
            lbltilt.Content = tilt;
        }


        private void sldr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //_data[START_ADDRESS + 0] = Convert.ToByte(pan);
            //lblpan.Content = pan;
            //_data[START_ADDRESS + 1] = Convert.ToByte(tilt);
            //lbltilt.Content = tilt;
            _data[START_ADDRESS + 2] = Convert.ToByte(sldrSpeed.Value);
            _data[START_ADDRESS + 3] = Convert.ToByte(sldrDimmer.Value);
            _data[START_ADDRESS + 4] = Convert.ToByte(sldrStrobe.Value);
            _data[START_ADDRESS + 5] = Convert.ToByte(sldrRed.Value);
            _data[START_ADDRESS + 6] = Convert.ToByte(sldrGreen.Value);
            _data[START_ADDRESS + 7] = Convert.ToByte(sldrBleu.Value);
            _data[START_ADDRESS + 8] = Convert.ToByte(sldrWhite.Value);
            _data[START_ADDRESS + 9] = Convert.ToByte(sldrPanTiltMode.Value);
            _data[START_ADDRESS + 10] = Convert.ToByte(sldrSnelheidPanTilt.Value);
            _data[START_ADDRESS + 11] = Convert.ToByte(sldrColorMode.Value);
            _data[START_ADDRESS + 12] = Convert.ToByte(sldrSnelheidColor.Value);

            if (sldrColorMode.Value > 0)
            {
                sldrSpeed.IsEnabled = false;
                sldrDimmer.IsEnabled = false;
                sldrStrobe.IsEnabled = false;
                sldrRed.IsEnabled = false;
                sldrGreen.IsEnabled = false;
                sldrBleu.IsEnabled = false;
                sldrWhite.IsEnabled = false;
            }
            if (sldrColorMode.Value == 0)
            {
                sldrSpeed.IsEnabled = true;
                sldrDimmer.IsEnabled = true;
                sldrStrobe.IsEnabled = true;
                sldrRed.IsEnabled = true;
                sldrGreen.IsEnabled = true;
                sldrBleu.IsEnabled = true;
                sldrWhite.IsEnabled = true;
            }
        }
    }
}
