using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;

namespace EFinzynierka.Controllers
{
    public class Rfid : Controller
    {
        public IActionResult Index()
        {
            SerialPort mySerialPort = new SerialPort("COM3");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            if (!mySerialPort.IsOpen)
                mySerialPort.Open();

            return View();
        }

        private static void DataReceivedHandler(
                     object sender,
                     SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);

            Console.WriteLine(indata);
        }
    }
}