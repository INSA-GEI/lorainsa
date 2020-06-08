#!/bin/bash

# ---------------------------------------
# EUI and packet forwarder configuration
#
# Author: S DI MERCURIO
# Date: 26/09/2018
# ---------------------------------------

# Gather ETH mac address (will be the EUI of the gateway)

GATEWAY_EUI_NIC=$(ip -oneline link show up 2>&1 | grep -v LOOPBACK | sed -E 's/^[0-9]+: ([0-9a-z]+): .*/\1/' | head -1)
if [[ -z $GATEWAY_EUI_NIC ]]; then
  echo "ERROR: No network interface found. Cannot set gateway ID."
  exit 1
fi

# Then get EUI based on the MAC address of that device
GATEWAY_EUI=$(cat /sys/class/net/$GATEWAY_EUI_NIC/address | awk -F\: '{print $1$2$3"FFFE"$4$5$6}')
GATEWAY_EUI=${GATEWAY_EUI^^} # toupper

echo "Gateway EUI: $GATEWAY_EUI"
address=$(ifconfig | grep -A1 $GATEWAY_EUI_NIC | grep 'inet' | awk -F' ' '{ print $2 }')

if [[ -z $address ]]; then
  echo "ERROR: Primary interface ($GATEWAY_EUI_NIC) not connected."
  echo "ERROR: Cannot retrieve ip address."
  exit 2
fi

echo "Gateway IP: $address"
name=$(host $address| awk -F' ' '{ print $5 }' | awk -F'.' '{ print $1 }')

if [[ -z $name ]]; then
  echo "ERROR: DNS entry not found for ip $(address)."
  
  read -p "Network name of the gateway or [ENTER] to exit: " name

  if [[ $name == "" ]]; then
    exit 2
  fi
elif [[ $name =~ NXDOMAIN$ ]]; then
  echo "ERROR: DNS entry not found for ip $(address)."
  
  read -p "Network name of the gateway or [ENTER] to exit: " name

  if [[ $name == "" ]]; then
    exit 2
  fi
fi
echo "Gateway name: $name"

# Generating configuration files 
echo ""
echo -e "{\n\t\"gateway_conf\": {\n\t\t\"gateway_ID\": \"$GATEWAY_EUI\",\n\t\t\"servers\": [ \n\t\t\t { \"server_address\": \"127.0.0.1\", \"serv_port_up\": 1700, \"serv_port_down\": 1700, \"serv_enabled\": true },\n\t\t\t { \"server_address\": \"router.eu.thethings.network\", \"serv_port_up\": 1700, \"serv_port_down\": 1700, \"serv_enabled\": false } \n\t\t],\n\t\t\"ref_latitude\": 43.571188,\n\t\t\"ref_longitude\": 1.467054,\n\t\t\"ref_altitude\": 150,\n\t\t\"contact_email\": \"dimercur@insa-toulouse.fr\",\n\t\t\"description\": \"$name\" \n\t}\n}" >local_conf.json
echo -e "$name" >hostname

cat >global_conf.json <<EOL
{
    "SX1301_conf": {
        "lorawan_public": true,
        "clksrc": 1, /* radio_1 provides clock to concentrator */
        "radio_0": {
            "enable": true,
            "type": "SX1257",
            "freq": 867500000,
            "rssi_offset": -166.0,
            "tx_enable": true
        },
        "radio_1": {
            "enable": true,
            "type": "SX1257",
            "freq": 868500000,
            "rssi_offset": -166.0,
            "tx_enable": false
        },
        "chan_multiSF_0": {
            /* Lora MAC channel, 125kHz, all SF, 868.1 MHz */
            "enable": true,
            "radio": 1,
            "if": -400000
        },
        "chan_multiSF_1": {
            /* Lora MAC channel, 125kHz, all SF, 868.3 MHz */
            "enable": true,
            "radio": 1,
            "if": -200000
        },
        "chan_multiSF_2": {
            /* Lora MAC channel, 125kHz, all SF, 868.5 MHz */
            "enable": true,
            "radio": 1,
            "if": 0
        },
        "chan_multiSF_3": {
            /* Lora MAC channel, 125kHz, all SF, 867.1 MHz */
            "enable": true,
            "radio": 0,
            "if": -400000
        },
        "chan_multiSF_4": {
            /* Lora MAC channel, 125kHz, all SF, 867.3 MHz */
            "enable": true,
            "radio": 0,
            "if": -200000
        },
        "chan_multiSF_5": {
            /* Lora MAC channel, 125kHz, all SF, 867.5 MHz */
            "enable": true,
            "radio": 0,
            "if": 0
        },
        "chan_multiSF_6": {
            /* Lora MAC channel, 125kHz, all SF, 867.7 MHz */
            "enable": true,
            "radio": 0,
            "if": 200000
        },
        "chan_multiSF_7": {
            /* Lora MAC channel, 125kHz, all SF, 867.9 MHz */
            "enable": true,
            "radio": 0,
            "if": 400000
        },
        "chan_Lora_std": {
            /* Lora MAC channel, 250kHz, SF7, 868.3 MHz */
            "enable": true,
            "radio": 1,
            "if": -200000,
            "bandwidth": 250000,
            "spread_factor": 7
        },
        "chan_FSK": {
            /* FSK 50kbps channel, 868.8 MHz */
            "enable": true,
            "radio": 1,
            "if": 300000,
            "bandwidth": 125000,
            "datarate": 50000
        },
        "tx_lut_0": {
            /* TX gain table, index 0 */
            "pa_gain": 0,
            "mix_gain": 8,
            "rf_power": -6,
            "dig_gain": 0
        },
        "tx_lut_1": {
            /* TX gain table, index 1 */
            "pa_gain": 0,
            "mix_gain": 10,
            "rf_power": -3,
            "dig_gain": 0
        },
        "tx_lut_2": {
            /* TX gain table, index 2 */
            "pa_gain": 0,
            "mix_gain": 12,
            "rf_power": 0,
            "dig_gain": 0
        },
        "tx_lut_3": {
            /* TX gain table, index 3 */
            "pa_gain": 1,
            "mix_gain": 8,
            "rf_power": 3,
            "dig_gain": 0
        },
        "tx_lut_4": {
            /* TX gain table, index 4 */
            "pa_gain": 1,
            "mix_gain": 10,
            "rf_power": 6,
            "dig_gain": 0
        },
        "tx_lut_5": {
            /* TX gain table, index 5 */
            "pa_gain": 1,
            "mix_gain": 12,
            "rf_power": 10,
            "dig_gain": 0
        },
        "tx_lut_6": {
            /* TX gain table, index 6 */
            "pa_gain": 1,
            "mix_gain": 13,
            "rf_power": 11,
            "dig_gain": 0
        },
        "tx_lut_7": {
            /* TX gain table, index 7 */
            "pa_gain": 2,
            "mix_gain": 9,
            "rf_power": 12,
            "dig_gain": 0
        },
        "tx_lut_8": {
            /* TX gain table, index 8 */
            "pa_gain": 1,
            "mix_gain": 15,
            "rf_power": 13,
            "dig_gain": 0
        },
        "tx_lut_9": {
            /* TX gain table, index 9 */
            "pa_gain": 2,
            "mix_gain": 10,
            "rf_power": 14,
            "dig_gain": 0
        },
        "tx_lut_10": {
            /* TX gain table, index 10 */
            "pa_gain": 2,
            "mix_gain": 11,
            "rf_power": 16,
            "dig_gain": 0
        },
        "tx_lut_11": {
            /* TX gain table, index 11 */
            "pa_gain": 3,
            "mix_gain": 9,
            "rf_power": 20,
            "dig_gain": 0
        },
        "tx_lut_12": {
            /* TX gain table, index 12 */
            "pa_gain": 3,
            "mix_gain": 10,
            "rf_power": 23,
            "dig_gain": 0
        },
        "tx_lut_13": {
            /* TX gain table, index 13 */
            "pa_gain": 3,
            "mix_gain": 11,
            "rf_power": 25,
            "dig_gain": 0
        },
        "tx_lut_14": {
            /* TX gain table, index 14 */
            "pa_gain": 3,
            "mix_gain": 12,
            "rf_power": 26,
            "dig_gain": 0
        },
        "tx_lut_15": {
            /* TX gain table, index 15 */
            "pa_gain": 3,
            "mix_gain": 14,
            "rf_power": 27,
            "dig_gain": 0
        }
    },

    "gateway_conf": {
        /* change with default server address/ports, or overwrite in local_conf.json */
        "gateway_ID": "AA555A0000000000",
        /* Devices */
        "gps": false,
        "beacon": false,
        "monitor": false,
        "upstream": true,
        "downstream": true,
        "ghoststream": false,
        "radiostream": true,
        "statusstream": true,
        /* node server */
        "server_address": "127.0.0.1",
        "serv_port_up": 1680,
        "serv_port_down": 1681,
      GC-GW  /* node servers for poly packet server (max 4) */
        "servers":
        [ /*{ "server_address": "127.0.0.1",
            "serv_port_up": 1680,
            "serv_port_down": 1681,
            "serv_enabled": false },
          { "server_address": "iot.semtech.com",
            "serv_port_up": 1680,
            "serv_port_down": 1680,
            "serv_enabled": false }*/ ],
        /* adjust the following parameters for your network */
        "keepalive_interval": 10,
        "stat_interval": 30,
        "push_timeout_ms": 100,
        /* forward only valid packets */
        "forward_crc_valid": true,
        "forward_crc_error": false,
        "forward_crc_disabled": false,
        /* GPS configuration */
        "gps_tty_path": "/dev/ttyAMA0",
        "fake_gps": true,
        "ref_latitude": 10,
        "ref_longitude": 20,
        "ref_altitude": -1,
        /* Ghost configuration */
        "ghost_address": "127.0.0.1",
        "ghost_port": 1918,
        /* Monitor configuration */
        "monitor_address": "127.0.0.1",
        "monitor_port": 2008,
        "ssh_path": "/usr/bin/ssh",
        "ssh_port": 22,
        "http_port": 80,
        "ngrok_path": "/usr/bin/ngrok",
        "system_calls": ["df -m","free -h","uptime","who -a","uname -a"],
        /* Platform definition, put a asterix here for the system value, max 24 chars. */
        "platform": "*", 
        /* Email of gateway operator, max 40 chars*/
        "contact_email": "dimercur@insa-toulouse.fr", 
        /* Public description of this device, max 64 chars */
        "description": "Global Configuration"         
    }
}
EOL

sudo cp global_conf.json /opt/ttn-gateway/bin
sudo cp local_conf.json /opt/ttn-gateway/bin
sudo cp hostname /etc
echo "Configuration files copied under '/opt/ttn-gateway/bin'"

read -p "Reboot [y/N] " choix
echo ""

choix=${choix,,} # tolower

if [[ $choix =~ ^(yes|y) ]]; then
  echo "Reboot ..."
  sudo reboot
fi