using ClientServerLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;


namespace Server
{
    class Program
    {

        static void Main(string[] args)
        {
            int PORT_NUMBER = 8005; // the port to run the server on

            // connect all model, observers, etc
            // setup inet on proper port
            // load all account info from file

            Model model = ReadModelFromFile();
            Console.WriteLine("Model created.");

            Controller controller = new Controller(model);
            Console.WriteLine("Controller created.");

            WebSocketServer wss = new WebSocketServer(PORT_NUMBER);
            ChatSocketBehavior csb = new ChatSocketBehavior();

            OutputConsole view = new OutputConsole(model);
            Console.WriteLine("View created.");

            wss.AddWebSocketService<ChatSocketBehavior>("/chat");

            // Must attach all observer/model delegate interactions
            controller.ModelUpdated += (s, arg) => view.HandleModelUpdate(s, arg);
            ChatSocketBehavior.OnLoginPacketRecieved += ((s, packet) => controller.handleLoginPacket(packet, (IClient)s,  ((ChatSocketBehavior)s).TempUsernameMap));
            ChatSocketBehavior.OnLogoutPacketRecieved += (s, packet) => controller.handleLogoutPacket(packet, (IClient)s, ((ChatSocketBehavior)s).TempUsernameMap, ((ChatSocketBehavior)s).getNextTempUsername());
            ChatSocketBehavior.OnQueryPacketRecieved += ((s, packet) => controller.handleQueryPacket(packet, (IClient)s));
            //ChatSocketBehavior.OnLoginPacketRecieved += (s, e) => Console.WriteLine("hi hi hi hi");

            Console.WriteLine("Observer test..");
            //controller.handleForwardPacket("");

            //Console.WriteLine("Save model test.. (try removing this & next test after 1st run) ");
            //WriteModelToFile(model);

            //Console.WriteLine("Modify model test..");
            //model.GetAccounts().Add("testAccount", new Account("testman", "password"));

            //Console.WriteLine("Save model test.. (try removing this & next test after 1st run) ");
            //WriteModelToFile(model);

            //Console.WriteLine("Load model test..");
            //ReadModelFromFile();

            model.GetChats.Add(0, new Chat(0)); // general chat

            PrintModelInfo(model);

            Console.WriteLine("Going online @ port: " + PORT_NUMBER);

            // start the server
            wss.Start();

            Console.WriteLine();
            Console.WriteLine();

            bool running = true;
            while (running)
            {
                Console.WriteLine("Running server..");
                Console.WriteLine("Type SAVE_QUIT to Save and Exit.");
                Console.WriteLine("Type EXIT to Exit without saving.");
                Console.WriteLine("Type INFO to List all Accounts.");
                Console.WriteLine("Type DELETE to DELETE ALL SAVED ACCOUNTS and Exit.");
                Console.WriteLine("-------------------------------------------");

                string input = Console.ReadLine();

                if (input.ToUpper().Equals("SAVE_QUIT"))
                {
                    WriteModelToFile(model);
                    running = false;
                    Console.WriteLine("Model saved, will exit.");
                }
                else if (input.ToUpper().Equals("EXIT"))
                {
                    running = false;
                    Console.WriteLine("Will now exit without saving.");
                }
                else if (input.ToUpper().Equals("INFO"))
                {
                    PrintModelInfo(model);
                }
                else if (input.ToUpper().Equals("DELETE"))
                {
                    model.GetAccounts.Clear();
                    running = false;
                    Console.WriteLine("DELETED ALL ACCOUNTS, exiting.");
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                }
            }

            // Stop the server
            wss.Stop();

            WriteModelToFile(model);
        }


        private static void PrintModelInfo(Model model)
        {
            Console.WriteLine("Num accounts in model: " + model.GetAccounts.Count);
            int i = 1;
            foreach (KeyValuePair<string, Account> pair in model.GetAccounts.ToList())
            {
                Account account = pair.Value;
                Console.WriteLine("  Account #" + i++ + " - " + account.Username);
                if (account.GetContacts.Count != 0)
                {
                    Console.WriteLine("      Contacts:");
                    int j = 1;
                    foreach (Account contact in account.GetContacts)
                    {
                        Console.WriteLine("      (" + j++ + ") " + contact.Username);
                    }
                }
                else
                {
                    Console.WriteLine("      (This user has no contacts)");
                }
                Console.WriteLine();
            }
        }

        /*
            SERIALIZATION HANDLING (SAVING, NOT NETWORKING)
        */
        //private static readonly string SERIALIZE_FOLDER = @"Data\";
        private static readonly string FILE = @"model.ser";

        private static Model ReadModelFromFile()
        {
            return ReadFromBinaryFile<Model>(Path.Combine(Environment.CurrentDirectory, FILE)) ?? new Model();
        }

        public static void WriteModelToFile(Model model)
        {
            File.Delete(Path.Combine(Environment.CurrentDirectory, FILE)); // this is unneeded (next line has 3rd arg default false to override)
            WriteToBinaryFile<Model>(Path.Combine(Environment.CurrentDirectory, FILE), model);
        }

        /// <summary>
        /// METHOD FROM:
        /// http://blog.danskingdom.com/saving-and-loading-a-c-objects-data-to-an-xml-json-or-binary-file/
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// METHOD FROM:
        /// http://blog.danskingdom.com/saving-and-loading-a-c-objects-data-to-an-xml-json-or-binary-file/
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        private static T ReadFromBinaryFile<T>(string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
