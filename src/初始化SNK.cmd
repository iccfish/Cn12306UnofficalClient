@ECHO OFF

ECHO 将会新建一个本机的、新的签名密钥。
ECHO.

Tools\sn.exe -k 12306.snk

ECHO.

ECHO 操作完成。