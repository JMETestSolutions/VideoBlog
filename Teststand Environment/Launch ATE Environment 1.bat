@echo off
REM This batch file will launch teststand using the environments file specified
SET environmentDirectory=%~dp0
SET teststandDirectory=C:\Program Files (x86)\National Instruments\TestStand 2020\Bin
START "Teststand" /d "%teststandDirectory%" SeqEdit.exe /ENV "%environmentDirectory%Environment.tsenv"