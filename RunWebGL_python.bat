@echo off

REM Open the default browser
start http://localhost:8000/index.html

SET PYTHON_CMD=python

REM Start the server based on the available Python command
%PYTHON_CMD% -m SimpleHTTPServer 8000

REM Wait for user input to close the server
echo Press any key to stop the server...
pause >nul
