@echo off
cd /d "%~dp0" 

echo Pulling latest changes from remote repository...
git pull origin main

echo Pull complete!
pause