#!/bin/bash
raspbian_url="https://downloads.raspberrypi.org/raspbian_lite_latest"
raspbian_zip="raspbian_lite_latest"
raspbian_img="2018-06-27-raspbian-stretch-lite.img"

echo "-----------------------------------------------"
echo "Script de configuration de l'image raspbian nue"
echo "-----------------------------------------------"
echo ""
echo ""
echo "!!!!! DOIT ETRE EXECUTE SUR PC !!!!!        "

read -n1 -p "(Q)itter ou (C)ontinuer ? " choix
echo ""

if [[ $choix == "Q" || $choix == "q" ]]; then
  
  exit 1
fi

rm -rf ./mnt_boot
rm -rf ./mount_root
#rm -rf $raspbian_img

mkdir ./mnt_boot
mkdir ./mnt_root
echo "Recuperation de la dernier image raspbian"
#wget $raspbian_url
#unzip $raspbian_zip

echo "Montage de l'image"
sudo losetup -P /dev/loop0 $raspbian_img
sudo mount -v /dev/loop0p1 ./mnt_boot
sudo mount -v /dev/loop0p2 ./mnt_root

echo "Configuration du WiFi"
sudo rm -rf ./mnt_root/etc/wpa_supplicant/wpa_supplicant.conf
sudo touch ./mnt_root/etc/wpa_supplicant/wpa_supplicant.conf
sudo chmod 666 ./mnt_root/etc/wpa_supplicant/wpa_supplicant.conf

cat >./mnt_root/etc/wpa_supplicant/wpa_supplicant.conf <<EOL
ctrl_interface=DIR=/var/run/wpa_supplicant GROUP=netdev
update_config=1
country=FR

network={
	ssid="IOT"
	psk="aBcDeFgHiJINSA18"
}
EOL

sudo chmod 444 ./mnt_root/etc/wpa_supplicant/wpa_supplicant.conf

echo "Enable SPI"
sudo su  -c 'echo "dtparam=spi=on" >> ./mnt_boot/config.txt'

read -n1 -p "Press ENTER " choix
echo "Demontage de l'image"
sudo umount ./mnt_boot
sudo umount ./mnt_root
rm -rf ./mnt_boot
rm -rf ./mnt_root
#rm $raspbian_img
#rm $ raspbian_zip

echo "Fin"