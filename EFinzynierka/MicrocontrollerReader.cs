using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace MicrocontrollerReader
{
    public class MicrocontrollerReader
    {
        // Set the name of the serial port that is connected to the microcontroller
        private string portName = "COM6";

        // Set the baud rate for the serial port
        private int baudRate = 9600;


        // Create a CancellationTokenSource to stop the background task
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        // Create a background task to read data from the microcontroller
        private Task readTask;

        public MicrocontrollerReader()
        {
            // Start the background task
            readTask = Task.Factory.StartNew(ReadData, cancelTokenSource.Token);
        }

        public void ReadData()
        {
            // Create a new SerialPort object
            SerialPort serialPort = new SerialPort(portName, baudRate);

            // Open the serial port
            serialPort.Open();

            // Read data from the serial port until the task is cancelled
            while (!cancelTokenSource.IsCancellationRequested)
            {
                // Read a line of data from the serial port
                string data = serialPort.ReadLine();

                // Process the data that was read from the microcontroller
                ProcessData(data);

                // Sleep for a short time before reading the next line of data
                Thread.Sleep(200);
            }

            // Close the serial port when we're finished
            serialPort.Close();
        }

        public void ProcessData(string data)
        {
            // TODO: Parse the data and extract the values that you are interested in
        }


        
    }
}


