# Version Geoserver
2.8.2


# Version Java
C:\Program Files\Java\jdk1.7.0_79
C:\Program Files\Java\jre7


# Shenime per Printimin 
1.  Konfigurime nga Geoserver - nderfaqe
	Logohemi ne geoserver: http://localhost:8090/geoserver/web/		user:admin		pass:geoserver
	Ne seksionin
	Settings -> JAI:	Memory Capacity (0-1)	: 1 
						Memory Threshold (0-1)	: 1
						Tile Threads:			: 10
						Tile Threads Priority	: 5
2.	Konfigurime nga Geoserver - file
	C:\Program Files (x86)\GeoServer 2.8.2\wrapper\wrapper.conf
	Ndryshohen parametrat e java ne seksionin: 
		# Java Additional Parameters
		wrapper.java.additional.1=-Djetty.home=.
		wrapper.java.additional.2=-DGEOSERVER_DATA_DIR="%GEOSERVER_DATA_DIR%"
		wrapper.java.additional.3=-XX:MaxPermSize=250m
		wrapper.java.additional.4=-Xmx256M -Xms48m
		wrapper.java.additional.5=-XX:SoftRefLRUPolicyMSPerMB=36000
		wrapper.java.additional.6=-XX:+UseParallelGC

		# Initial Java Heap Size (in MB)
		wrapper.java.initmemory=1000

		# Maximum Java Heap Size (in MB)
		wrapper.java.maxmemory=3200
3.	Nga programi i javes "jre"
	Java -> Configure Java
		Ne tab "Java" -> shtyp "view" -> Runtime parameters : shto
		-Xms2048m -Xmx2048m -XX:+UseParallelOldGC -XX:+UseParallelGC -XX:NewRatio=2 -XX:+AggressiveOpts
		
4.	Be restart service "GeoServer 2.8.2"
5.	Logohemi perseri ne geoserver (si ne piken 1)
	Ne seksionin
	About & Status -> Server Status	(Duhet te kene ndryshuar fushat: "Memory Usage", "JAI Maximum Memory" etj)
	Shtypim Clear per	"Resource Cache"
	Shtypim reload per	"Configuration and catalog"