@echo off

	set cexists=0

	:c1
	findstr /C:"Modified Page List Bytes" telegraf.conf
	if "%errorlevel%" == "0" goto c2
	  set c1="Modified Page List Bytes", 
	  set cexists=1

	:c2
	findstr /C:"Standby Cache Reserve Bytes" telegraf.conf
	if "%errorlevel%" == "0" goto c3
		set c2="Standby Cache Reserve Bytes", 
		set cexists=1

	:c3
	findstr /C:"Standby Cache Normal Priority Bytes" telegraf.conf
	if "%errorlevel%"=="0" goto c4
		set c3="Standby Cache Normal Priority Bytes", 
		set cexists=1

	:c4
	findstr /C:"Standby Cache Core Bytes" telegraf.conf
	if "%errorlevel%"=="0" goto check_counter
		set c4="Standby Cache Core Bytes"
		set cexists=1

	:check_counter
	if %cexists% == 0 goto c5
		echo.>>telegraf.conf
		echo.>>telegraf.conf
		echo.  # Additional win_perf_counters from Telegraf v1.18.2>>telegraf.conf
		echo.  [[inputs.win_perf_counters.object]]>>telegraf.conf
		echo.    ObjectName = "Memory">>telegraf.conf
		echo.    Counters = [%c1%%c2%%c3%%c4%]>>telegraf.conf
		echo.    Instances = ["------"]>>telegraf.conf
		echo.    Measurement = "win.mem">>telegraf.conf

	:c5
	findstr /C:"inputs.mem" telegraf.conf
	if NOT "%errorlevel%"=="0" (echo.  >>telegraf.conf
		echo.  [[inputs.mem]]>>telegraf.conf
		echo.    name_prefix = "win.">>telegraf.conf
		echo.  >>telegraf.conf)