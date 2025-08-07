@echo off
cd /d "%~dp0"

:: Stage all changes
git add -A

:: Ask for a commit message
set /p message=Enter commit message: 

:: Commit and push
git commit -m "%message%"
git push origin main

pause