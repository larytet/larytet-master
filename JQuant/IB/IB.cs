/// <summary>
/// Contains wrapper for the InteractiveBrokers API and simulation 
/// This is a rather large file
/// IB supportrs paper trading. I do not need to support paper trading option in the FSM
/// like I did in all algos so far.
/// </summary>


using System;

// file system
using System.IO;

// text stream
using System.Text;

namespace IB
{
	/// <summary>
	/// Message types to be sent to Processor
	/// </summary>
	public enum MessageType
	{
		Connect,
		DataIn
	}

	/// <summary>
	/// Possible states of the Processor
	/// </summary>
	public enum State
	{
		Idle,
		Connecting,
		Connected
	}
	
	/// <summary>
	/// Objects of this type can be sent to the Processot thread
	/// </summary>
	public class Message
	{
		public Message(MessageType id, object data, object data1)
		{
			this.data = data;
			this.data1 = data1;
			this.id = id;
		}
		
		public Message(MessageType id, object data)
		{
			this.data = data;
			this.data1 = null;
			this.id = id;
		}

		public MessageType id;
		public object data;
		public object data1;
	}
	
	public class Processor : JQuant.MailboxThread<Message>
	{
		/// <summary>
		/// Initialize the class. This is a simpleton - only one object of this 
		/// type exists in the system
		/// </summary>
		protected Processor() 
			:base("Processor", 100)
		{
			state = State.Idle;
		
		}
		
		/// <summary>
		/// Create an instance of the processor
		/// </summary>
		/// <returns>
		/// A <see cref="Processor"/>
		/// </returns>
		public Processor CreateProcessor ()
		{
			return null;
		}
		
		/// <summary>
		/// Connect to the server. This method will open a client socket and 
		/// start the FSM which attempts to connect to the specified server. 
		/// The actual connection is goingt to be established in the context of the 
		/// Processor FSM. Notification onConnect can be received asynchronously 
		/// by the application
		/// <param name="host">
		/// </summary>
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="port">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool Connect(string host, int port)
		{
			this.Send(new Message(MessageType.Connect, host, port));
			return true;
		}
		
		/// <summary>
		/// Returns true if connection is established with the server and 
		/// the initial handshake is alright
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool isConnected()
		{
			return (state == State.Connected);
		}
		
		protected override void HandleMessage (Message message)
		{
			switch (state)
			{
			case State.Idle:
				HandleMessage_Idle (message);
				break;
			case State.Connecting:
				HandleMessage_Connecting (message);
				break;
			case State.Connected:
				HandleMessage_Connected (message);
				break;
			}
		}

		protected void HandleMessage_Idle (Message message)
		{
			switch (message.id)
			{
				case MessageType.Connect:
				HandleMessage_IdleConnect (message);
				break;
			}
		
		}
		
		protected void HandleMessage_Connected (Message message)
		{
			
		}

		protected void HandleMessage_Connecting (Message message)
		{
			switch (message.id)
			{
				case MessageType.DataIn:
				HandleMessage_ConnectingDataIn(message);
				break;
			}
			
		}
		
		protected void HandleMessage_IdleConnect (Message message)
		{
			string host = (string)message.data;
			int port = (int)(message.data1);
			clientSocket = null;
			try
			{
				clientSocket = new System.Net.Sockets.TcpClient (host, port);
			}
			catch (Exception e)
			{
				System.Console.WriteLine ("Failed to connect " + host + ", port "+port);
				System.Console.WriteLine (e.ToString ());
			}
			if (clientSocket != null)
			{
				socketReader = new SocketReader(this, clientSocket);
				socketReader.Start();
				bool res = WriteToSocket(clientSocket, CLIENT_VERSION);
				if (res)
				{
					this.state = State.Connecting;
				}
				else
				{
					SocketCleanup();
				}
			}
		
		}
		
		/// <summary>
		/// In the connecting state I am ready only for one message - server version
		/// </summary>
		/// <param name="message">
		/// A <see cref="Message"/>
		/// </param>
		protected void HandleMessage_ConnectingDataIn(Message message)
		{
			
		}
		
		protected void SocketCleanup()
		{
			clientSocket.Close();
			socketReader.Stop();
		}

		protected static bool WriteToSocket(System.Net.Sockets.TcpClient clientSocket, int data)
		{
			string s = data.ToString();
			bool res = WriteToSocket(clientSocket, s);

			return res;
		}
		
		protected static bool WriteToSocket(System.Net.Sockets.TcpClient clientSocket, string s)
		{
			System.Net.Sockets.NetworkStream networkStream;
			networkStream = clientSocket.GetStream();
			
			bool res;
			byte[] data = System.Text.Encoding.ASCII.GetBytes(s);
			try 
			{
				networkStream.Write(data, 0, data.Length);
				res = true;
			}	
			catch (Exception e)
			{
				res = false;
			}

			return res;
		}
		

		protected State state;
		protected System.Net.Sockets.TcpClient clientSocket;
		private static int CLIENT_VERSION = 46;
		private static int SERVER_VERSION = 38;
		SocketReader socketReader;
		
	}
	
	class SocketReader
	{
		public SocketReader(Processor processor, System.Net.Sockets.TcpClient clientSocket)
		{
			this.networkStream = clientSocket.GetStream();
			this.processor = processor;
			exitFlag = false;
			thread = new System.Threading.Thread(this.Run);
			thread.Priority = System.Threading.ThreadPriority.Highest;
		}
		
		public void Start()
		{
			thread.Start();
		}
		
		public void Stop()
		{
			exitFlag = true;
		}
		
		public void Run()
		{
			const int BUFFER_SIZE = 1*1024;
			byte[] buffer = new byte[BUFFER_SIZE];
			while (!exitFlag)
			{
				int bytes = 0;
				
				try
				{
					bytes = networkStream.Read(buffer, 0, buffer.Length);
				}
				catch (Exception e)
				{
				}
				if (bytes != 0)
				{
					HandleData(buffer, bytes);
					buffer = new byte[BUFFER_SIZE];
				}
				else
				{
					System.Threading.Thread.Sleep(200);
				}
			}
		}
		
		protected void HandleData(byte[] data, int size)
		{
			processor.Send(new Message(MessageType.DataIn, data, size));
		}
		
		bool exitFlag;
		System.Net.Sockets.NetworkStream networkStream;
		Processor processor;
		System.Threading.Thread thread;
	}

} // namespace IB