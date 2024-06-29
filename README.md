# Me.Cv

## 1 - Deploy on Debian 12

    1.1 - Install .NET SDK / .NET Runtime

        1.1.1 - wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        1.1.2 - sudo dpkg -i packages-microsoft-prod.deb
        1.1.3 - sudo rm packages-microsoft-prod.deb
        1.1.4 - Install the SDK
            1.1.4.1 - sudo apt-get update
            1.1.4.2 - sudo apt-get install -y dotnet-sdk-8.0
        1.1.5 - Install the Runtime
            1.1.5.1 - sudo apt-get update
            1.1.5.2 - sudo apt-get install -y aspnetcore-runtime-8.0
            1.1.5.3 - sudo apt-get install -y dotnet-runtime-8.0
        1.1.6 - Check installed SDKs and Runtimes
            1.1.6.1 - Check installed SDKs: dotnet --list-sdks
            1.1.6.2 - Check installed Runtimes: dotnet --list-runtimes
            1.1.6.3 - Check more info: dotnet --info

    1.2 - Build Me.Cv.Api project

        1.2.1 - Right click on Me.Cv.Api project the Visual Studio IDE
        1.2.2 - Select Publish
        1.2.3 - Select Folder profile
        1.2.4 - Click on Publish button

    1.3 - Transfer Me.Cv.Api builded project to Debian 12 server as Zip file

        1.3.1 - You can use services like https://www.file.io (upload using it and download on your server using wget). Also you can use FTP or SSH.
        1.3.2 - I recommend unzip project on /var/www/me-cv/me-cv-api/ directory

    1.4 - Verify that Me.Cv.Api project work correctly

        1.4.1 - Run "dotnet Me.Cv.Api.dll" command on /var/www/me-cv/me-cv-api/ directory. If the output is "Now listening on: http://localhost:5000" then the project works fine.

    1.5 - Create and configure Me.Cv.Api service as startup OS service

        1.5.1 - Create me-cv-api service: sudo nano /etc/systemd/system/me-cv-api.service
        1.5.2 - Add the next code to file created:
            ```bash
    			[Unit]
    			Description=Me.Cv.Api Service

    			[Service]
    			WorkingDirectory=/var/www/me-cv/me-cv-api
    			ExecStart=/usr/bin/dotnet Me.Cv.Api.dll
    			Restart=always
    			RestartSec=10
    			SyslogIdentifier=me-cv-api
    			User=root
    			Environment=ASPNETCORE_ENVIRONMENT=Production

    			[Install]
    			WantedBy=multi-user.target
    		```
        1.5.3 - Enable service: sudo systemctl enable me-cv-api.service
        1.5.4 - Other commands:
            1.5.4.1 - Start service: sudo systemctl start me-cv-api.service
            1.5.4.2 - Stop service: sudo systemctl stop me-cv-api.service
            1.5.4.3 - Restart service: sudo systemctl restart me-cv-api.service
            1.5.4.4 - Check service status: sudo systemctl status me-cv-api.service
        1.5.5 - Reboot Debian 12 OS and check service status

    1.6 - Install and configure Nginx

        1.6.1 - Update packages: sudo apt-get update
        1.6.2 - Install Nginx: sudo apt-get install nginx
        1.6.3 - Start service: sudo service nginx start
        1.6.4 - Stop service: sudo service nginx stop
        1.6.5 - Restart service: sudo service nginx restart
        1.6.6 - Check service status: sudo service nginx status
        1.6.7 - Navigate to http://your-server-ip and check that if "Welcome to nginx!" is showed

    1.7 - Build Me.Cv.Web project

        1.7.1 - Run command: ng build --configuration production

    1.8 - Transfer Me.Cv.Web builded project to Debian 12 server as Zip file

        1.8.1 - You can use services like https://www.file.io (upload using it and download on your server using wget). Also you can use FTP or SSH.
        1.8.2 - I recommend unzip project on /var/www/me-cv/me-cv-web/ directory

    1.9 - Configure Nginx site

        1.9.1 - Edit default Nginx file: sudo nano /etc/nginx/sites-available/default
        1.9.2 - Add the next code:
            ```
                server {
                        listen 80 default_server;
                        listen [::]:80 default_server;
                        server_name edujjalvarez.ar;
                        return 301 https://$host$request_uri;
                }

                server {
                        listen 443 ssl;
                        listen [::]:443 ssl;
                #        listen 80 default_server;
                #        listen [::]:80 default_server;

                        root /var/www/me-cv;

                        index index.html index.htm index.nginx-debian.html;

                        server_name edujjalvarez.ar;
                #         server_name _;

                        ssl_certificate /etc/letsencrypt/live/edujjalvarez.ar/fullchain.pem; # managed by Certbot
                        ssl_certificate_key /etc/letsencrypt/live/edujjalvarez.ar/privkey.pem; # managed by Certbot
                        include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
                        ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot

                        location / {
                                return 301 https://$host/me-cv-web$request_uri;
                        }

                        location /me-cv-web/ {
                                index index.html index.htm index.nginx-debian.html;
                                try_files $uri $uri/ =404;
                        }

                        location /me-cv-api/ {
                                proxy_pass http://localhost:5000/;
                                proxy_http_version 1.1;
                                proxy_set_header Upgrade $http_upgrade;
                                proxy_set_header Connection keep-alive;
                                proxy_set_header Host $host;
                                proxy_cache_bypass $http_upgrade;
                                proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
                                proxy_set_header X-Forwarded-Proto $scheme;
                        }
                }
            ```
        1.9.3 - Restart Nginx service: sudo service nginx restart
        1.9.4 - Navigate to: http://your-server-ip/me-cv-web/

    1.10 - Install and configure Let's Encrypt SSL Certificate

        1.10.1 - Update packages: sudo apt update
        1.10.2 - Install Certbot: sudo apt install certbot python3-certbot-nginx
        1.10.3 - Get SSL Certificate: sudo certbot --nginx -d edujjalvarez.ar
        1.10.4 - Certbot install a cron job to renew certificates automatically
        1.10.5 - Simulate certificate renew: sudo certbot renew --dry-run
        1.10.6 - Restart Nginx: sudo service nginx restart
        1.10.7 - Nagigate to https://your-server-ip
