using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

using System.Web.Script.Serialization;

namespace mqtt_reader
{
    public partial class MainWindow : Form
    {
        MqttClient client;
        String clientId;
        string BrokerAddress = "geitp-dimer2.insa-toulouse.fr";

        public class AppResponse {
            public string applicationID {get; set;}
            public string applicationName {get; set;}
            public string nodeName {get; set;}
            public string devEUI {get; set;}
            public RXInfo rxInfo {get; set;}
            public TXInfo txInfo {get; set;}
            public string fCnt {get; set;}
            public string fPort {get; set;}
            public string data {get; set;}
            public string decodedData { get; set; }
        };

        public class RXInfo {
            public string mac {get; set;}
            public string time {get; set;}
            public string rssi {get; set;}
            public string loRaSNR {get; set;}
            public string name {get; set;}
            public string latitude {get; set;}
            public string longitude {get; set;}
            public string altitude { get; set; }
        };

        public class TXInfo {
            public string frequency {get; set;}
            public DataRate dataRate {get; set;}
            public string adr {get; set;}
            public string codeRate {get; set;}
        };
        
        public class DataRate {
            public string modulation {get; set;}
            public string bandwidth {get; set;}
            public string spreadFactor {get; set;}
        };

        string SubscribedTopic = "";
        ArrayList MessagesList = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();

            client = new MqttClient(BrokerAddress);

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();

            client.Connect(clientId);
            toolStripStatusConnect.Text = "Connected to " + BrokerAddress;
            Console.WriteLine("Connected to " + BrokerAddress);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.IsConnected) client.Disconnect();

            Console.WriteLine("Bye Bye");
        }

        private void butSubscribeUnsubscribe_Click(object sender, EventArgs e)
        {
            if (butSubscribeUnsubscribe.Text.Contains("Unsubscribe"))
            {
                client.Unsubscribe(new string[] { SubscribedTopic });
                butSubscribeUnsubscribe.Text = "Subscribe";
                Console.WriteLine("Unsubscribe: " + SubscribedTopic);
            }
            else
            {
                if (TopicBox.Text != "")
                {
                    // whole topic
                    SubscribedTopic = TopicBox.Text;

                    // subscribe to the topic with QoS 2
                    client.Subscribe(new string[] { SubscribedTopic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
                    Console.WriteLine("Subscribe: " + SubscribedTopic);

                    butSubscribeUnsubscribe.Text = "Unsubscribe";
                }
                else
                {
                    MessageBox.Show("You must enter a topic before subscribing", "Invalid topic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            string ReceivedTopic = e.Topic;

            if (ReceivedTopic.Contains("application/"))
            {
                Console.WriteLine("Topic: "+ ReceivedTopic+ "\nMsg: " + ReceivedMessage);

                var json = new JavaScriptSerializer();
                //AppResponse results = json.Deserialize<AppResponse>(ReceivedMessage);

                dynamic result = json.DeserializeObject(ReceivedMessage);
                AppResponse appResponse = new AppResponse();
                appResponse.rxInfo = new RXInfo();
                appResponse.txInfo = new TXInfo();
                appResponse.txInfo.dataRate = new DataRate();

                appResponse.applicationID = result["applicationID"];
                appResponse.applicationName = result["applicationName"];
                appResponse.nodeName = result["nodeName"];
                appResponse.devEUI = result["devEUI"];

                appResponse.rxInfo.mac = result["rxInfo"][0]["mac"];
                appResponse.rxInfo.time = result["rxInfo"][0]["time"];
                appResponse.rxInfo.rssi = result["rxInfo"][0]["rssi"].ToString();
                appResponse.rxInfo.loRaSNR = result["rxInfo"][0]["loRaSNR"].ToString();
                appResponse.rxInfo.name = result["rxInfo"][0]["name"];
                appResponse.rxInfo.latitude = result["rxInfo"][0]["latitude"].ToString();
                appResponse.rxInfo.longitude = result["rxInfo"][0]["longitude"].ToString();
                appResponse.rxInfo.altitude = result["rxInfo"][0]["altitude"].ToString();

                appResponse.txInfo.frequency = result["txInfo"]["frequency"].ToString();
                appResponse.txInfo.dataRate.modulation = result["txInfo"]["dataRate"]["modulation"];
                appResponse.txInfo.dataRate.bandwidth = result["txInfo"]["dataRate"]["bandwidth"].ToString();
                appResponse.txInfo.dataRate.spreadFactor = result["txInfo"]["dataRate"]["spreadFactor"].ToString();

                appResponse.txInfo.adr = result["txInfo"]["adr"].ToString();
                appResponse.txInfo.codeRate = result["txInfo"]["codeRate"];

                appResponse.fCnt = result["fCnt"].ToString();
                appResponse.fPort = result["fPort"].ToString();
                appResponse.data = result["data"];
                byte[] data = Convert.FromBase64String(appResponse.data);
                appResponse.decodedData = Encoding.UTF8.GetString(data);

                MessagesList.Add(appResponse);
                Console.WriteLine(appResponse.ToString());

                toolStripStatusMessages.Text = MessagesList.Count.ToString() + " message(s) received";

                MessagesView.Invoke((MethodInvoker) delegate
                {
                    System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[] {
                    appResponse.rxInfo.time, appResponse.decodedData, ReceivedMessage}, -1);

                    MessagesView.Items.Add(listViewItem);
                });
            }
        }

        private void MessagesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MessagesView.SelectedIndices.Count != 0)
            {
                int index = MessagesView.SelectedIndices[0];
                Console.WriteLine("selected Index : " + index);

                AppResponse response = (AppResponse)MessagesList[index];

                TreeNode applicationIDNode = new TreeNode("applicationID: "+response.applicationID);
                TreeNode applicationNameNode = new TreeNode("applicationName: " + response.applicationName);
                TreeNode nodeNameNode = new TreeNode("nodeName: " + response.nodeName);
                TreeNode devEUINode = new TreeNode("devEUI: " + response.devEUI);

                TreeNode macNode = new TreeNode("mac: " + response.rxInfo.mac);
                TreeNode timeNode = new TreeNode("time: " + response.rxInfo.time);
                TreeNode rssiNode = new TreeNode("rssi: " + response.rxInfo.rssi);
                TreeNode loRaSNRNode = new TreeNode("loRaSNR: " + response.rxInfo.loRaSNR);
                TreeNode nameNode = new TreeNode("name: " + response.rxInfo.name);
                TreeNode latitudeNode = new TreeNode("latitude: " + response.rxInfo.latitude);
                TreeNode longitudeNode = new TreeNode("longitude: " + response.rxInfo.longitude);
                TreeNode altitudeNode = new TreeNode("altitude: " + response.rxInfo.altitude);

                TreeNode frequencyNode = new TreeNode("frequency: " + response.txInfo.frequency);
                TreeNode modulationNode = new TreeNode("modulation: " + response.txInfo.dataRate.modulation);
                TreeNode bandwidthNode = new TreeNode("bandwidth: " + response.txInfo.dataRate.bandwidth);
                TreeNode spreadFactorNode = new TreeNode("spreadFactor: " + response.txInfo.dataRate.spreadFactor);

                TreeNode adrNode = new TreeNode("adr: " + response.txInfo.adr);
                TreeNode codeRateNode = new TreeNode("codeRate: " + response.txInfo.codeRate);

                TreeNode fCntNode = new TreeNode("fCnt: " + response.fCnt);
                TreeNode fPortNode = new TreeNode("fPort: " + response.fPort);
                TreeNode dataNode = new TreeNode("data: " + response.data);
                TreeNode decodedDataNode = new TreeNode("decoded Data: " + response.decodedData);

                TreeNode rxInfo = new TreeNode("rxInfo", new System.Windows.Forms.TreeNode[] {
            macNode,timeNode ,rssiNode ,loRaSNRNode,nameNode,latitudeNode,longitudeNode,altitudeNode});
                TreeNode dataRate = new TreeNode("dataRate", new System.Windows.Forms.TreeNode[] {
            modulationNode,bandwidthNode ,spreadFactorNode});
                TreeNode txInfo = new TreeNode("txInfo", new System.Windows.Forms.TreeNode[] {
            frequencyNode,dataRate ,adrNode ,codeRateNode});

                TreeNode MainNode = new TreeNode(response.decodedData, new System.Windows.Forms.TreeNode[] {
             applicationIDNode,applicationNameNode ,nodeNameNode,devEUINode,rxInfo, txInfo, fCntNode,fPortNode,dataNode,decodedDataNode});

                treeDetail.Nodes.Clear();
                treeDetail.Nodes.Add(MainNode);
            }
        }
    }
}
