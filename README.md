Add to
	In C:\xampp\apache\conf\httpd.conf
	
	Enable 2 modules:
		LoadModule proxy_connect_module modules/mod_proxy_connect.so
		LoadModule proxy_http_module modules/mod_proxy_http.so
		
	In C:\xampp\apache\conf\extra\httpd-vhosts.conf
		Add at the end of the files:
		
			<VirtualHost *:80>
				ProxyPreserveHost On

				# Servers to proxy the connection, or;
				# List of application servers:
				# Usage:
				# ProxyPass / http://[IP Addr.]:[port]/
				# ProxyPassReverse / http://[IP Addr.]:[port]/
				# Example: 
				ProxyPass /api/ http://localhost:5000/api/
				ProxyPassReverse /api http://localhost:5000/api/

				ServerName localhost
			</VirtualHost>
	
	