#! /bin/bash

cd /tmp
apt-get update
apt-get --assume-yes install wget
apt-get --assume-yes install perl
wget https://mirror.ctan.org/systems/texlive/tlnet/install-tl-unx.tar.gz
zcat install-tl-unx.tar.gz | tar xf -
perl /tmp/install-tl-*/install-tl --no-interaction --no-doc-install --no-src-install --scheme=small

/usr/local/texlive/2023/bin/x86_64-linux/tlmgr init-usertree
/usr/local/texlive/2023/bin/x86_64-linux/tlmgr install titlesec