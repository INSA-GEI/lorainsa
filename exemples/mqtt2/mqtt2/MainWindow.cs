using System;
using Gtk;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MainWindow : Gtk.Window
{
    MqttClient client;
    String clientId;
    string BrokerAddress = "test.mosquitto.org";

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        client = new MqttClient(BrokerAddress);

		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

		// use a unique id as client id, each time we start the application
		clientId = Guid.NewGuid().ToString();

		client.Connect(clientId);

        Console.WriteLine("Connected");
	}

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Console.WriteLine("Bye bye");
        client.Disconnect();

        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButtonSubscribeClicked(object sender, EventArgs e)
    {
		if (entrySub.Text != "")
		{
			// whole topic
			string Topic = "/testify/" + entrySub.Text;

			// subscribe to the topic with QoS 2
			client.Subscribe(new string[] { Topic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
            Console.WriteLine("Subscribe: " + Topic);
		}
		else
		{
			Console.WriteLine("You have to enter a topic to subscribe!");
		}
    }

	// this code runs when a message was received
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
	{
		string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

        Console.WriteLine("Received: "+ReceivedMessage);
	}
}
