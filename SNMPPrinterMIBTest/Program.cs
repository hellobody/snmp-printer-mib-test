using System;
using System.Collections.Generic;
using System.Net;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;

namespace SNMPPrinterMIBTest
{
    internal class Program
    {
        private const string _printMIB = "1.3.6.1.2.1.43."; // OID for the MIB module for management of printers.

        static void Main(string[] args)
        {
            var result = new List<Variable>();

            try
            {
                var rowCount = Messenger.Walk(
                    VersionCode.V1,
                    new IPEndPoint(IPAddress.Parse("192.168.1.157"), 161),
                    new OctetString("public"),
                    new ObjectIdentifier(_printMIB + "8.2.1.13"),
                    result,
                    6000,
                    WalkMode.WithinSubtree);
            }
            catch(Lextm.SharpSnmpLib.Messaging.TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i].ToString());
            }

            Console.WriteLine("Press any key to close.");

            Console.ReadKey();
        }
    }
}
