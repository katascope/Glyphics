@REM Because all the other ways don't seem to work very well
@REM So, just recursively delete the bin and obj folders
@echo off
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"