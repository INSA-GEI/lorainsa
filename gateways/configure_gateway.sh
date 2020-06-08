#!/bin/bash

echo "Enable SPI"
sudo su  -c 'echo "dtparam=spi=on" >> ./mnt_boot/config.txt'

sudo dpkg-reconfigure locales
sudo dpkg-reconfigure tzdata

sudo apt update && sudo apt upgrade && sudo apt install git

sudo adduser lora
sudo adduser lora sudo
