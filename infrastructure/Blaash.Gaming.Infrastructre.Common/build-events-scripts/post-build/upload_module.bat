
echo OFF


set http_base_url=http://103.212.120.203:813
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O tenant_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install

set http_base_url=http://103.212.120.203:810
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O event_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install


set http_base_url=http://103.212.120.203:806
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O gateway_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install


set http_base_url=http://103.212.120.203:807
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O sso_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install


set http_base_url=http://103.212.120.203
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O ui_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install


set http_base_url=http://103.212.120.203:808
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O admin_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install


set http_base_url=http://103.212.120.203:809
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O profile_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install


set http_base_url=http://103.212.120.203:811
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O game_config_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install

set http_base_url=http://103.212.120.203:812
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O game_play_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install



set http_base_url=http://103.212.120.203:815
wget -S -O uninstall_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/uninstall
ZNxtApp.CLI -u   %http_base_url%/api/moduleinstaller/upload  .\..\..\bin\Debug\Blaash.Gaming.Infrastructre.Common.1.0.1-Beta00%1.nupkg
wget -S -O engagement_response.json --post-data "{\"Name\":\"Blaash.Gaming.Infrastructre.Common\",\"Version\":\"1.0.1-Beta00%1\",\"InstallationKey\":\"f5d2ce5ed463e844266feb92abba804244a7b5287484ecd2041395dad4bf18c3\"}" %http_base_url%/api/moduleinstaller/install
